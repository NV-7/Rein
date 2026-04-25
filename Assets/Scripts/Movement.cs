using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask collisionLayers; // Assign in Inspector to only check specific layers (e.g., Enemy, Walls)
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
        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y);
        Vector3 targetPos = playerRb.position + movement * moveSpeed * Time.fixedDeltaTime;

        if(!detectCollison(targetPos))
        {
            playerRb.MovePosition(targetPos);
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
        
        // If collisionLayers is set, only check those layers
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
            // Ignore the player's own collider and trigger colliders
            if(hit != playerCollider && !hit.isTrigger)
            {
                return true;
            }
        }
        return false;
    }

    

    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void OnAim(InputValue value)
    {
        aimInput = value.Get<Vector2>();
    }
}
