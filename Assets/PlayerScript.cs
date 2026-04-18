using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject bullet;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        Vector3 direction = transform.forward;
        Quaternion rotation = Quaternion.LookRotation(direction);
       // rotation.y -= 90f;
        GameObject b = Instantiate(bullet, transform.position, rotation);
        b.GetComponent<Rigidbody>().velocity = direction * 10;
    }
}
