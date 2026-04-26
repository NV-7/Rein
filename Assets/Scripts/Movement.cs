using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask collisionLayers;
    private Vector2 aimInput;
    private Vector2 movementInput;
    private Rigidbody playerRb;
    private Camera mainCam;
    private BoxCollider playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        mainCam = Camera.main;
        playerCollider = GetComponent<BoxCollider>();
        
        // Validate setup
        if (playerRb == null)
        {
            Debug.LogError("Rigidbody missing!");
        }
        else
        {
            Debug.Log($"Rigidbody - IsKinematic: {playerRb.isKinematic}, Constraints: {playerRb.constraints}");
        }
        
        if (playerCollider == null)
        {
            Debug.LogError("BoxCollider missing!");
        }
        
        Debug.Log($"MoveSpeed: {moveSpeed}");
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        lookAtMouse();
    }

    void PlayerMovement()
    {
        if (movementInput == Vector2.zero) return;
        
        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y);
        Vector3 targetPos = playerRb.position + movement * moveSpeed * Time.fixedDeltaTime;

        Debug.Log($"Current Pos: {playerRb.position}, Target Pos: {targetPos}");
        
        bool collision = detectCollison(targetPos);
        Debug.Log($"Collision Detected: {collision}");
        
        if(!collision)
        {
            playerRb.MovePosition(targetPos);
            Debug.Log($"Moving to: {targetPos}");
        }
        else
        {
            Debug.LogWarning("Movement blocked by collision!");
        }
    }

    public void lookAtMouse()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);

        if(plane.Raycast(ray, out float distance))
        {
            Vector3 intersectPoint = ray.GetPoint(distance);
            Vector3 playerToMouseDirection = intersectPoint - transform.position;
            playerToMouseDirection.y = 0;
            Quaternion rotation = Quaternion.LookRotation(playerToMouseDirection);
            playerRb.MoveRotation(rotation);
        }
    }

    bool detectCollison(Vector3 targetPos)
    {
        Vector3 boxSize = playerCollider.size;
        Vector3 boxCenter = targetPos + playerCollider.center;
        Collider[] hits;
        
        if(collisionLayers.value != 0)
        {
            hits = Physics.OverlapBox(boxCenter, boxSize / 2f, transform.rotation, collisionLayers);
        }
        else
        {
            hits = Physics.OverlapBox(boxCenter, boxSize / 2f, transform.rotation);
        }

        foreach(Collider hit in hits)
        {
            if(hit != playerCollider && !hit.isTrigger)
            {
                Debug.Log($"Collision with: {hit.gameObject.name}");
                return true;
            }
        }
        return false;
    }

    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
        Debug.Log($"OnMove called: {movementInput}");
    }

    void OnAim(InputValue value)
    {
        aimInput = value.Get<Vector2>();
    }
}
