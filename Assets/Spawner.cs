using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject stationary_enemy;
    public GameObject player;
    public float spawnRadius = 10f;
    public float time = 0f;
    public float spawnRate = 1f;
    public GameObject cube;
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
        if(time >= spawnRate)
        {
            SpawnEnemy();
            time = 0f;
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(min_X, max_X);
        float randomZ = Random.Range(min_Z, max_Z);
        Transform enemyTransform = stationary_enemy.transform;
        float yOffset = Mathf.Abs(enemyTransform.position.y - top);
        Vector3 spawnPoint = new Vector3(randomX, top + yOffset, randomZ);

        GameObject enemy = Instantiate(stationary_enemy, spawnPoint, Quaternion.identity);

    }
}
