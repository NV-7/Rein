using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour

{
    public float speed = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 pos = transform.position;

        if (Keyboard.current.upArrowKey.isPressed == true)
        {
            {
                pos.y = pos.y + speed * Time.deltaTime;
            }
        }
        if (Keyboard.current.leftArrowKey.isPressed == true)
        {
            {
                pos.x = pos.x - speed * Time.deltaTime;
            }
        }
        if (Keyboard.current.downArrowKey.isPressed == true)
        {
            {
                pos.y = pos.y - speed * Time.deltaTime;
            }
        }
        if (Keyboard.current.rightArrowKey.isPressed == true)
        {
            {
                pos.x = pos.x + speed * Time.deltaTime;
            }
        }
    }
}
