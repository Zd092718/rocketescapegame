using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPostion;
    [SerializeField] Vector3 movementVector;
    //Range(0,1) creates a slider in the unity inspector which allows for oscillation simulation
    float movementFactor;
    [SerializeField] float period;
    // Start is called before the first frame update
    void Start()
    {
        startingPostion = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Mathf.Epsilon is safest bet when zero checking floats
        if(period <= Mathf.Epsilon){return;}
        // continually growing over time
        float cycles = Time.time / period;

        // constant value of 6.283
        const float tau = Mathf.PI * 2;
        // going from -1 to 1
        float rawSinWave = Mathf.Sin(cycles * tau);

        // keeps movement factor between 1 and 2 for cleaner calculation
        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPostion + offset;
    }
}
