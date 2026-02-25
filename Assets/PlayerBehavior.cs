using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 10;
    public Vector3 input_direction;
    public Rigidbody rb;
    public float rotationSpeed = 720f;
    public GameObject bullet;
    public GameObject camera;
    
    public Transform cameraTransform;
    public Transform point;
    public float bulletSpeed = 15f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

           
            cameraTransform = Camera.main.transform;
        

    }

    void Update()
    {
        float x_movement = 0;
        float z_movement = 0;



        if (Keyboard.current.wKey.isPressed)
        {
            z_movement += 1;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            x_movement += -1;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            z_movement += -1;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            x_movement += 1;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet, point.position, transform.rotation);

            Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();

            if(bulletRb != null)
            {
                bulletRb.velocity = transform.forward * bulletSpeed;
            }

            Destroy(newBullet, 5f); 


        }

        // --- CAMERA-RELATIVE MOVEMENT LOGIC ---

        // 1. Get the camera's forward and right vectors
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        // 2. Flatten the vectors on the Y axis so the player doesn't fly up or dig into the ground
        camForward.y = 0f;
        camRight.y = 0f;

        // 3. Normalize them to ensure consistent speed regardless of camera angle
        camForward.Normalize();
        camRight.Normalize();

        // 4. Multiply inputs by the camera's directional vectors to get the final direction
        input_direction = (camForward * z_movement + camRight * x_movement).normalized;


    }

    private void FixedUpdate()
    {
        if (input_direction != Vector3.zero)
        {
            // Apply velocity. input_direction is already in world-space relative to the camera.
            rb.velocity = new Vector3(input_direction.x * speed, rb.velocity.y, input_direction.z * speed);

            // Rotate the player to face the new movement direction
            Quaternion targetRotation = Quaternion.LookRotation(input_direction);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
}