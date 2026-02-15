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
            
                Vector3 newPos = transform.position;
                newPos.y = newPos.y + speed * Time.deltaTime;
                transform.position = newPos;
            
        }
        if (Keyboard.current.leftArrowKey.isPressed == true)
        {
            
                Vector3 newPos = transform.position;
                newPos.x = newPos.x - speed * Time.deltaTime;
                transform.position = newPos;
              
            
        }
        if (Keyboard.current.downArrowKey.isPressed == true)
        {
            Vector3 newPos = transform.position;
            newPos.y = newPos.y - speed * Time.deltaTime;
            transform.position = newPos;
            

        }
        if (Keyboard.current.rightArrowKey.isPressed == true)
        {
            Vector3 newPos = transform.position;
            newPos.x = newPos.x + speed * Time.deltaTime;
            transform.position = newPos;
           
        }
    }
    
    
}
