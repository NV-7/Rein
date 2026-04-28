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
    public int bulletCount = 8; // Number of bullets to shoot in a circle
    public int fireMode = 0;
    private GameObject player;
    private float time = 0f;
    private GameObject spawner;
    private GameObject firePoint;
    
    void Start()
    {
        Debug.Log($"[START] {gameObject.name} - bullet: {(bullet != null ? bullet.name : "NULL")}");
    }

    private void Awake()
    {
        spawner = GameObject.FindGameObjectWithTag("Respawn");
        player = GameObject.FindGameObjectWithTag("Player");
        if(transform.childCount > 0)
        {
            firePoint = transform.GetChild(0).gameObject;
            Vector3 firePointPosition = firePoint.transform.position;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
         time += Time.deltaTime;
        if(time  >= fireRate)
        {
            FireBulletSporadic();
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
        if (bullet == null)
        {
            Debug.LogError($"[FireBullet] {gameObject.name} - bullet is NULL!");
            return;
        }
        
        Vector3 direction = (player.transform.position - transform.position).normalized;
        GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
        b.GetComponent<Rigidbody>().velocity = direction * 10f;
    }

    private void FireBulletSpin()
    {
        if(firePoint == null)
        {
            Debug.Log("Fire point not found!");
            return;
        }
        if (bullet == null)
        {
            Debug.LogError($"[FireBulletSpin] {gameObject.name} - bullet is NULL!");
            return;
        }
        
        Vector3 direction = firePoint.transform.position - transform.position;
        GameObject a = Instantiate(bullet, transform.position, Quaternion.identity);
        GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
        a.GetComponent<Rigidbody>().velocity = -direction * 10f;
        b.GetComponent<Rigidbody>().velocity = direction * 10f;
    }

    private void FireBulletCircle()
    {
        Debug.Log($"[FireBulletCircle ENTRY] {gameObject.name} - bullet: {(bullet != null ? bullet.name : "NULL")}");
        
        if (bullet == null)
        {
            Debug.LogError($"[FireBulletCircle] {gameObject.name} - bullet is NULL!");
            return;
        }

        Debug.Log($"[FireBulletCircle] {gameObject.name} - Starting to fire {bulletCount} bullets");

        float angleStep = 360f / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));

            GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
            if (bulletInstance != null)
            {
                Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = direction * 10f;
                }
                else
                {
                    Debug.LogError("Bullet prefab is missing a Rigidbody component!");
                }
            }
            else
            {
                Debug.LogError("Failed to instantiate bullet!");
            }
        }
        
        Debug.Log($"[FireBulletCircle] {gameObject.name} - Finished firing");
    }

    private void FireBulletCross()
    {
        float angleStep = 365f / 4f;
        for (int i = 0; i < 4; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));

            GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
            if (bulletInstance != null)
            {
                Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = direction * 10f;
                }
                else
                {
                    Debug.LogError("Bullet prefab is missing a Rigidbody component!");
                }
            }
            else
            {
                Debug.LogError("Failed to instantiate bullet!");
            }
        }

    }

    private void FireBulletSporadic()
    {
        int randomStep = Random.Range(5, 9);
        float angleStep = 365f / randomStep;
        for (int i = 0; i < randomStep; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            float angleShift = Random.Range(0f, 15f);
            Vector3 direction = new Vector3(Mathf.Cos(angle + angleShift), 0f, Mathf.Sin(angle + angleShift));

            GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
            if (bulletInstance != null)
            {
                Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = direction * 10f;
                }
                else
                {
                    Debug.LogError("Bullet prefab is missing a Rigidbody component!");
                }
            }
            else
            {
                Debug.LogError("Failed to instantiate bullet!");
            }
        }
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
