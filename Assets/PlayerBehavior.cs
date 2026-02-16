using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour

{
    public float speed = 10;
    public Vector3 input_direction;
    public Rigidbody rb;
    public float rotationSpeed = 720f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    // Update is called once per frame
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

        input_direction = new Vector3(x_movement, 0, z_movement).normalized;

      
    }
    private void FixedUpdate()
    {
        if (input_direction != Vector3.zero)
        {
            rb.velocity = new Vector3(input_direction.x * speed, rb.velocity.y, input_direction.z * speed);
            Quaternion targetRotation = Quaternion.LookRotation(input_direction);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }


}
