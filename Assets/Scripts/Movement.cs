using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 aimInput;
    private Vector2 movementInput;
    private Rigidbody playerRb;
    private Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        mainCam = Camera.main;
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
        playerRb.MovePosition(playerRb.position + movement * moveSpeed * Time.fixedDeltaTime);
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
            transform.rotation = rotation;

        }
        
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
