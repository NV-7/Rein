using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Vector3 offset;
    public float smoothing = 10f;
    public bool isSmooth = true;
    public Rigidbody rb;
    void Start()
    {
        if(player == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player = player.transform;
            }
        }

        offset = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (obj != null)
            {
                player = obj.transform;
            }
        }
    }

    private void FixedUpdate()
    {
        //if (player == null)
        //{
        //    return;
        //}

        //Vector3 position = player.position + offset;

        //if (isSmooth)
        //{
        //    Vector3 p = Vector3.Lerp(transform.position, position, smoothing * Time.deltaTime);
        //    rb.MovePosition(p);
        //}
    }

    private void LateUpdate()
    {

        if (player == null)
        {
            return;
        }

        Vector3 position = player.position + offset;

        if (isSmooth)
        {
            Vector3 p = Vector3.Lerp(transform.position, position, smoothing * Time.deltaTime);
            //rb.MovePosition(p);
            transform.position = p;
        }
    }
}
