using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Tilemaps;

public class enemyStationary_behavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public float fireRate = 1f;
    public float health = 100f;
    private GameObject player;
    private float time = 0f;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
         time += Time.deltaTime;
        if(time  >= fireRate)
        {
            FireBullet();
            time = 0f;
        }

        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void FireBullet()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
        b.GetComponent<Rigidbody>().velocity = direction * 10f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            health -= 25f;
            Destroy(other.gameObject);
        }
        
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
