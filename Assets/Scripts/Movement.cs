using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource boostSource;
    public float thrustSpeed = 500f;
    public float rotationSpeed = 200f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boostSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
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
                boostSource.Play();
            } 
        }
        else 
        {
            boostSource.Stop();
        }
    }
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so physics system can take over
    }
}
