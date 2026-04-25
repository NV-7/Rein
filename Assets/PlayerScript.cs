using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject bullet;
    public float fireRate = 0.3f;
    public float bulletSpeed = 20f;
    public Transform firePoint;

    private float time = 0f;
    void Start()
    {
        firePoint = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetMouseButton(0) && Time.time >= time)
        {
            FireBullet();
            time = Time.time + fireRate;
        }

        
    }

    private void FixedUpdate()
    {
       
    }

    void FireBullet()
    {
        Vector3 direction = firePoint.forward;
        Quaternion rotation = Quaternion.LookRotation(direction);
       // rotation.y -= 90f;
        GameObject b = Instantiate(bullet, firePoint.position, rotation);
        b.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
    }

    void OnClick(InputAction input)
    {
        if (input.IsPressed())
        {
            FireBullet();
        }
    }
}
