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
    private GameObject spawner;
    private GameObject firePoint;
    
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Respawn");
        player = GameObject.FindGameObjectWithTag("Player");
        firePoint = transform.GetChild(0).gameObject;
        Vector3 firePointPosition = firePoint.transform.position;
        firePointPosition *= 1.1f;
        firePoint.transform.position = firePointPosition;
        
    }

    // Update is called once per frame
    void Update()
    {
         time += Time.deltaTime;
        if(time  >= fireRate)
        {
            FireBulletSpin();
            time = 0f;
        }

        if (health <= 0f)
        {
            Destroy(gameObject);
        }

        if(player == null)
        {
            GameObject.FindGameObjectsWithTag("Player");
        }
    }

    private void FireBullet()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
        b.GetComponent<Rigidbody>().velocity = direction * 10f;
    }

    private void FireBulletSpin()
    {

        Vector3 direction = transform.position - firePoint.transform.position;
        GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
        b.GetComponent<Rigidbody>().velocity = direction * 10f;
        

    }

    private void rotateFirePoint()
    {

    }

    private void FireBulletSpiral()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            health -= 25f;
            Destroy(other.gameObject);
        }
    }
}
