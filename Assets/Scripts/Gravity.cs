using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float accelerationDueToGravity = 9.8f;
    private Vector3 _velocity = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // s is position, is velocity at t=0, t is time and a is acceleration.
        // s = ut + (1/2)a t^2

        Vector3 _gravityVector = Vector3.down * accelerationDueToGravity;

        _velocity += _gravityVector * Time.fixedDeltaTime;
        this.transform.position += _velocity * Time.fixedDeltaTime + (1f / 2f) * _gravityVector * Time.fixedDeltaTime * Time.fixedDeltaTime;

    }

}
