using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    private NavMeshAgent nav;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            nav.SetDestination(player.position);
        }

    }
}
