using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustSpeed = 500f;
    [SerializeField] float rotationSpeed = 200f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    Rigidbody rb;
    AudioSource boostSource;

    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boostSource = GetComponent<AudioSource>();
        
    }


    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);

            if(!boostSource.isPlaying)
            {
                boostSource.PlayOneShot(mainEngine);
            } 
            if(!mainEngineParticles.isPlaying)
            {
                mainEngineParticles.Play();
            }
        }
        else 
        {
            boostSource.Stop();
            mainEngineParticles.Stop();
        }
    }
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
            if(!rightThrusterParticles.isPlaying)
            {
                rightThrusterParticles.Play();
            }
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
            if(!leftThrusterParticles.isPlaying)
            {
                leftThrusterParticles.Play();
            }
        } 
        else 
        {
            rightThrusterParticles.Stop();
            leftThrusterParticles.Stop();  
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so physics system can take over
    }
}
