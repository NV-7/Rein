using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    AudioSource bulletSound;
    ParticleSystem bulletParticle;
    // Start is called before the first frame update
    void Start()
    {
        bulletSound = this.GetComponent<AudioSource>();
        bulletSound.Play();
        bulletParticle = this.GetComponent<ParticleSystem>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
