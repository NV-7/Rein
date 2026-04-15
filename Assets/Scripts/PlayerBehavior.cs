using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 10;
    public Vector3 input_direction;
    public Rigidbody rb;
    public float rotationSpeed = 720f;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform point;
    public float bulletSpeed = 15f;

    [Header("Camera")]
    public Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Automatically grab the main camera if not assigned
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        float x_movement = 0;
        float z_movement = 0;

        if (Keyboard.current.wKey.isPressed) z_movement += 1;
        if (Keyboard.current.aKey.isPressed) x_movement -= 1;
        if (Keyboard.current.sKey.isPressed) z_movement -= 1;
        if (Keyboard.current.dKey.isPressed) x_movement += 1;

        // Shooting logic
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet, point.position, transform.rotation);
            Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();

            if (bulletRb != null)
            {
                bulletRb.velocity = transform.forward * bulletSpeed;
            }
            Destroy(newBullet, 5f);
        }

        // --- CAMERA-RELATIVE MOVEMENT LOGIC ---
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        input_direction = (camForward * z_movement + camRight * x_movement).normalized;
    }

    private void FixedUpdate()
    {
        // Apply Movement
        if (input_direction != Vector3.zero)
        {
            rb.velocity = new Vector3(input_direction.x * speed, rb.velocity.y, input_direction.z * speed);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        // --- THIRD PERSON ROTATION ---
        // Make the player character always face the direction the camera is looking
        Vector3 aimDirection = cameraTransform.forward;
        aimDirection.y = 0f; // Keep rotation strictly horizontal

        if (aimDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(aimDirection);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }
}