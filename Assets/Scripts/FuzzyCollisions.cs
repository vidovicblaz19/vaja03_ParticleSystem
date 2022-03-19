using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyCollisions : MonoBehaviour
{

    public Vector3 gravity = new Vector3(0, -9.81f, 0);

    private Transform[] Particles;
    private Transform Bounds;

    private Vector3[] _velocities;
    public float alpha_multiplier = 1f;
    public float beta = 0.95f;

    // Start is called before the first frame update
    void Start()
    {
        Bounds = this.transform.GetChild(0).gameObject.transform;
        GameObject _GO_Particles = this.transform.GetChild(1).gameObject;

        Particles = new Transform[_GO_Particles.transform.childCount];
        _velocities = new Vector3[_GO_Particles.transform.childCount];

        for (int i = 0; i < _GO_Particles.transform.childCount; i++){
            Particles[i] = (_GO_Particles.transform.GetChild(i).transform);
        }
    }


    private Vector3 dComputation(Vector3 ParticleA, Vector3 ParticleB) { 
        Vector3 force = ParticleA - ParticleB;
        float alpha = alpha_multiplier * (force.magnitude * force.magnitude);

        float imen = Mathf.Sqrt(force.x * force.x + force.y * force.y + force.z * force.z);
        force = force / (Mathf.Pow(imen, alpha));

        return force;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        
        //collision
        for (int i = 0; i < Particles.Length; i++)
        {
            //===Gravity===
            //_velocities[i] += gravity * Time.fixedDeltaTime;
        }

        for (int i = 0; i < Particles.Length; i++)
        {
            Vector3 d = Vector3.zero;

            //check for collision with another sphere
            for (int j = 0; j < Particles.Length; j++)
            {
                if (j == i) continue;
                d += dComputation(Particles[i].position, Particles[j].position);
            }

            //check for collision with bounds
            for (int j = 0; j < 6; j++) {
                Vector3 bounds = Vector3.zero;
                switch (j)
                {
                    case 0:
                        bounds = new Vector3(-(Bounds.localScale.x / 2), Particles[i].position.y, Particles[i].position.z);
                        break;
                    case 1:
                        bounds = new Vector3((Bounds.localScale.x / 2), Particles[i].position.y, Particles[i].position.z);
                        break;
                    case 2:
                        bounds = new Vector3(Particles[i].position.x, -(Bounds.localScale.y / 2), Particles[i].position.z);
                        break;
                    case 3:
                        break;
                    case 4:
                        bounds = new Vector3(Particles[i].position.x, Particles[i].position.y, -(Bounds.localScale.z / 2));
                        break;
                    case 5:
                        bounds = new Vector3(Particles[i].position.x, Particles[i].position.y, (Bounds.localScale.z / 2));
                        break;
                    default:
                        break;
                }

                d += dComputation(Particles[i].position, bounds);
                
            }


            //_velocities[i] = (1 - beta) * _velocities[i] + beta * d;
            _velocities[i] = _velocities[i] + beta * d;

            //Gravity
            _velocities[i] += gravity * Time.fixedDeltaTime;
            //compute new positions for particles
            Particles[i].position += _velocities[i] * Time.fixedDeltaTime;

        }
    }
}
