using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowSpray : MonoBehaviour
{

    public ParticleSystem SnowParticles;
    public ParticleSystem SnowParticles2;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = rb.velocity;
   
        if (vel.magnitude > 1)
        {
            CreateDust();
        }
        else
        {
            StopDust();
        }
    }

    void CreateDust()
    {
        SnowParticles.Play();
        SnowParticles2.Play();
    }

    void StopDust()
    {
        SnowParticles.Stop();
        SnowParticles2.Stop();
    }
}
