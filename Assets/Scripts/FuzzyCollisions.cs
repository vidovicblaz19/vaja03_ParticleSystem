using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyCollisions : MonoBehaviour
{
    public Vector3 gravity = new Vector3(0, -9.81f, 0);
    public float accelerationDueToGravity = 9.81f;
    //public float dampeningFactor = 1f;

    private Transform[] Particles;
    private Transform Bounds;

    private Vector3[] _velocities;

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

    private float repellingForce = 35f;
    private Vector3 isBoundsCollision(Transform Particle, Vector3 bounds) {

        Vector3 force = Particle.position - bounds;
        float dist = force.magnitude;
        //force = force / (Mathf.Sqrt(force.x * force.x + force.y * force.y + force.z * force.z));
        force = force / (Mathf.Sqrt(force.x * force.x + force.y * force.y + force.z * force.z)+1);


        float strength = repellingForce / (dist * dist);
        return force * strength;
    }

    private Vector3 isSphereCollision(Transform ParticleA, Transform ParticleB)
    {
        Vector3 force = ParticleA.position - ParticleB.position;
        float dist = force.magnitude;
        //force = force / (Mathf.Sqrt(force.x * force.x + force.y * force.y + force.z * force.z));
        force = force / (Mathf.Sqrt(force.x * force.x + force.y * force.y + force.z * force.z)+1);

        float strength = repellingForce / (dist * dist);
        return force * strength;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3[] tmpVelocities = _velocities;
        //collision
        for (int i = 0; i < Particles.Length; i++)
        {
            //===Gravity===
            _velocities[i] += gravity * Time.fixedDeltaTime;
        }

        for (int i = 0; i < Particles.Length; i++)
        {        

            //check for collision with another sphere
            for (int j = 0; j < Particles.Length; j++)
            {
                if (j == i) continue;

                _velocities[i] += isSphereCollision(Particles[i], Particles[j]) * Time.fixedDeltaTime;
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

                _velocities[i] += isBoundsCollision(Particles[i], bounds) * Time.fixedDeltaTime;
            }

            //_velocities[i] -= (1 + dampeningFactor) * Vector3.Dot(_velocities[i], n) * n;
            //compute new positions for particles
            Particles[i].position += (_velocities[i] + tmpVelocities[i]) / 2 * Time.deltaTime;
        }
    }
}
