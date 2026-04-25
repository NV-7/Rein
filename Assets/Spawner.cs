using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject stationary_enemy;
    public GameObject player;
    public float spawnRadius = 10f;
    public float time = 0f;
    public float spawnRate = 1f;
    public GameObject cube;
    public int maxEnemey = 5;
    private int counter = 0;
    private float max_X, max_Z, min_X, min_Z, top;

    private Renderer cubeRenderer;
    void Start()
    {
        cube = transform.parent.gameObject;
        cubeRenderer = cube.GetComponent<Renderer>();
        max_X = cubeRenderer.bounds.max.x;
        max_Z = cubeRenderer.bounds.max.z;
        min_X = cubeRenderer.bounds.min.x;
        min_Z = cubeRenderer.bounds.min.z;
        top = cubeRenderer.bounds.max.y;

        Debug.Log($"Spawn bounds — X: [{min_X}, {max_X}] Z: [{min_Z}, {max_Z}] Top: {top}");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= spawnRate && counter < maxEnemey)
        {
            SpawnEnemy();
            time = 0f;
            
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(min_X, max_X);
        float randomZ = Random.Range(min_Z, max_Z);
        float enemyRadius =  1.5f;
        Transform enemyTransform = stationary_enemy.transform;
        float yOffset = Mathf.Abs(enemyTransform.position.y - top);
        Vector3 spawnPoint = new Vector3(randomX, top + yOffset, randomZ);

        //Check if enemy is spawned on another
        SphereCollider enemyCollider = stationary_enemy.GetComponent<SphereCollider>();
        if(enemyCollider != null)
        {
            enemyRadius = enemyCollider.radius;
        }


        Collider[] hit = Physics.OverlapSphere(spawnPoint, enemyRadius);

        if(hit.Length == 0)
        {
            GameObject enemy = Instantiate(stationary_enemy, spawnPoint, Quaternion.identity);
            counter++;
        }
    }
}
