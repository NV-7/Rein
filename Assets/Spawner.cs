using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public GameObject stationary_enemy;
    public GameObject moving_enemy;
    public GameObject player;
    public GameObject transition;
    public float spawnRadius = 10f;
    public float time = 0f;
    public float spawnRate = 1f;
    public GameObject cube;
    public int maxEnemey = 5;
    public int mode = 0; // 0 for stationary, 1 for moving
    
    private int counter = 0;
    private float max_X, max_Z, min_X, min_Z, top;
    private enemyStationary_behavior enemyBehavior;


    private Renderer cubeRenderer;
    // Start is called before the first frame update  
    void Start()
    {
        cube = transform.parent.gameObject;
        transition = transform.GetChild(0).gameObject;
        cubeRenderer = cube.GetComponent<Renderer>();
        max_X = cubeRenderer.bounds.max.x;
        max_Z = cubeRenderer.bounds.max.z;
        min_X = cubeRenderer.bounds.min.x;
        min_Z = cubeRenderer.bounds.min.z;
        top = cubeRenderer.bounds.max.y;

        

        Debug.Log($"Spawn bounds — X: [{min_X}, {max_X}] Z: [{min_Z}, {max_Z}] Top: {top}");
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(counter < maxEnemey)
        {
            SpawnEnemy();
            time = 0f;
            
        }
        if(maxEnemey <= 0)
        {
            Debug.Log("No enemies left!");
            StartCoroutine(transitionScene());
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(min_X, max_X);
        float randomZ = Random.Range(min_Z, max_Z);
        float enemyRadius =  1.5f;
        Renderer enemyRenderer = stationary_enemy.gameObject.GetComponent<Renderer>();
        Transform enemyTransform = stationary_enemy.transform;
        float yOffset = Mathf.Abs(enemyTransform.position.y - enemyRenderer.bounds.min.y);
        Vector3 spawnPoint = new Vector3(randomX, top + yOffset, randomZ);

        //Check if enemy is spawned on another
        SphereCollider enemyCollider = stationary_enemy.GetComponent<SphereCollider>();
        if (enemyCollider != null)
        {
            enemyRadius = enemyCollider.radius;
        }


        Collider[] hit = Physics.OverlapSphere(spawnPoint, enemyRadius);

        if(hit.Length == 0)
        {
            GameObject enemy;
            if (mode == 0)
            {
                enemy = Instantiate(stationary_enemy, spawnPoint, Quaternion.identity);
                enemy.GetComponent<enemyStationary_behavior>().fireMode = FireMode.FireBullet;
            }
            else if (mode == 1)
            {
                enemy = Instantiate(moving_enemy, spawnPoint, Quaternion.identity);
                enemy.GetComponent<enemyStationary_behavior>().fireMode = FireMode.Circle;
            }
            else
            {
                if(Random.Range(1,3) % 2 == 0)
                {
                    enemy = Instantiate(stationary_enemy, spawnPoint, Quaternion.identity);
                    enemy.GetComponent<enemyStationary_behavior>().fireMode = (FireMode)Random.Range(0, 6);
                }
                else
                {
                    enemy = Instantiate(moving_enemy, spawnPoint, Quaternion.identity);
                }
                   
                enemy.GetComponent<enemyStationary_behavior>().fireMode = (FireMode)Random.Range(0, 6);
            }


                counter++;
        }
    }

    private void loadScene()
    {
        if(mode == 0)
        {
            SceneManager.LoadScene(2);
        }else if(mode == 1)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator transitionScene()
    {
        transition.SetActive(true);
        transition.GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        loadScene();
    }

 

    void SpawnPlayer()
    {
       
        Vector3 spawnPoint = new Vector3((max_X + min_X) / 2 , top + 3.5f, (max_Z + min_Z)/2);
        Instantiate(player, spawnPoint, Quaternion.identity);



    }
}
