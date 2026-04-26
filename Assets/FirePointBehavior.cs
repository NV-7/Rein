using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform center;
    public float rotationSpeed = 50f;
    void Start()
    {
        center = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(center.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
      
    }

    

    
}
