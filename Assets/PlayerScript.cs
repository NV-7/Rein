using MCPForUnity.Editor.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerScript : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject bullet;
    public float fireRate = 0.3f;
    public float bulletSpeed = 20f;
    public Transform firePoint;
    public float immuneTime = 1.5f;
    public AudioSource shootSound;
    public GameObject transition;
    public bool isAlive = true;
    private float immuneTimer = 0f;
    private int hits = -1;
    private MeshRenderer left;
    private MeshRenderer main;
    private MeshRenderer right;
    private List<MeshRenderer> parts;
    private Boolean immune = false;
    private bool deathSequenceStarted = false; // NEW: Prevent multiple death calls

    private float time = 0f;
    void Start()
    {
        
    }



    private void Awake()
    {
        firePoint = transform.GetChild(1);
        shootSound = GetComponent<AudioSource>();
        transition = transform.GetChild(2).gameObject;
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
        
        if(!isAlive) return;
        
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
        // FIXED: Don't process hits if already dead
        if(!isAlive) return;
        
        if (other.CompareTag("BulletIndestructable") || other.CompareTag("BulletDestructable"))
        {
            Debug.Log("Tagged");
            if(immune == false)
            {
                immune = true;
                hits++;
                Destroy(other.gameObject);
                
                // FIXED: Disable part first, then check for death
                if(hits < parts.Count)
                {
                    MeshRenderer partToDisable = parts[hits];
                    if (partToDisable != null)
                    {
                        partToDisable.enabled = false;
                    }
                }
                
                if (hits == 2)
                {
                    Die(); // FIXED: Call Die() method instead of destroying immediately
                }
                
            }

        }
    }

    private void FixedUpdate()
    {
       
    }

    void FireBullet()
    {
        shootSound.Play();
        Vector3 direction = firePoint.forward;
        Quaternion rotation = Quaternion.LookRotation(direction);
       // rotation.y -= 90f;
        GameObject b = Instantiate(bullet, firePoint.position, rotation);
        b.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
    }

   
    void Die()
    {
        if(deathSequenceStarted) return; // Prevent multiple calls
        
        deathSequenceStarted = true;
        isAlive = false;
        
       
        Collider col = GetComponent<Collider>();
        if(col != null)
        {
            col.enabled = false;
        }
        
        Movement movement = GetComponent<Movement>();
        if(movement != null)
        {
            movement.enabled = false;
        }
        
        // Start death sequence
        StartCoroutine(handleDeath());
    }

    IEnumerator handleDeath()
    {
        transition.SetActive(true);
        transition.GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
       
    }

    void OnClick(InputAction input)
    {
        if (input.IsPressed() && isAlive) // FIXED: Check if alive
        {
            FireBullet();
        }
    }
}
