using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    AudioSource bulletSound;
    ParticleSystem bulletParticle;
    private float time = 0;
    public float despawn = 10f;
    // Start is called before the first frame update
    void Start()
    {

        //bulletSound = this.GetComponent<AudioSource>();
        //bulletSound.Play();
        //bulletParticle = this.GetComponent<ParticleSystem>();

     
    }

    

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= despawn)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        String objectTag = gameObject.tag;
        String other_tag = other.gameObject.tag;
        Debug.Log("Bullet Triggered with " + other_tag);
        if (other_tag.Equals("BulletDestructable") && objectTag.Equals("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }

    


}
