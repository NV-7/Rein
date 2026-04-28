using MCPForUnity.Editor.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject bullet;
    public float fireRate = 0.3f;
    public float bulletSpeed = 20f;
    public Transform firePoint;
    public float immuneTime = 1.5f;
    private float immuneTimer = 0f;
    private int hits = -1;
    private MeshRenderer left;
    private MeshRenderer main;
    private MeshRenderer right;
    private List<MeshRenderer> parts;
    private Boolean immune = false;

    private float time = 0f;
    void Start()
    {
        
    }



    private void Awake()
    {
        firePoint = transform.GetChild(1);
        GameObject model = transform.GetChild(0).gameObject;
        left = model.transform.GetChild(0).GetComponent<MeshRenderer>();
        main = model.transform.GetChild(1).GetComponent<MeshRenderer>();
        right = model.transform.GetChild(2).GetComponent<MeshRenderer>();
        parts = new List<MeshRenderer>();
        parts.Add(right);
        parts.Add(left);
        parts.Add(main);
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetMouseButton(0) && Time.time >= time)
        {
            FireBullet();
            time = Time.time + fireRate;
        }

       if(immune == true)
        {
            immuneTimer += Time.deltaTime;
            if(immuneTimer >= immuneTime)
            {
                immuneTimer = 0f;
                immune = false;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletIndestructable") || other.CompareTag("BulletDestructable"))
        {
            Debug.Log("Tagged");
            if(immune == false)
            {
                immune = true;
                hits++;
                Destroy(other.gameObject);
                if (hits == 2)
                {
                    Destroy(gameObject);
                }
                MeshRenderer partToDisable = parts[hits];
                if (partToDisable != null)
                {
                    partToDisable.enabled = false;
                }
                
            }

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
