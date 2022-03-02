using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidCollisions : MonoBehaviour
{
    public float accelerationDueToGravity = 1f;
    public float dampeningFactor = 1f;

    private Transform[] Particles;
    private Transform Bounds;

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

    private bool isSphereCollision(Transform ParticleA,Transform ParticleB) {
        //if distance between objects is smaller than sum of their radiai
        if (Vector3.Distance(ParticleA.position, ParticleB.position) <= (ParticleA.localScale.x / 2 + ParticleB.localScale.x / 2))
            return true;
        return false;
    }

    private Vector3 isBoundsCollision(Transform Particle, Transform Box, Vector3 ParticleVelocity)
    {
        var r = Particle.localScale.x / 2;
        //x axis
        if ((Particle.position.x - r) <= -(Box.localScale.x / 2)) {
            var d = Particle.position - new Vector3(-(Box.localScale.x / 2) - r, Particle.position.y, Particle.position.z);
            var v = ParticleVelocity - Vector3.zero;
            if (Vector3.Dot(d, v) < 0) return Vector3.right;
        }
        if ((Particle.position.x + r) >= (Box.localScale.x / 2)) {
            var d = Particle.position - new Vector3((Box.localScale.x / 2) + r, Particle.position.y, Particle.position.z);
            var v = ParticleVelocity - Vector3.zero;
            if (Vector3.Dot(d, v) < 0) return Vector3.left;
        }
        //y axis
        if ((Particle.position.y - r) <= -(Box.localScale.y / 2)) {
            var d = Particle.position - new Vector3(Particle.position.x, -(Box.localScale.y / 2) - r, Particle.position.z);
            var v = ParticleVelocity - Vector3.zero;
            if (Vector3.Dot(v, d) < 0) return Vector3.up;
        }
        //z axis
        if ((Particle.position.z - r) <= -(Box.localScale.z / 2)) {
            var d = Particle.position - new Vector3(Particle.position.x, Particle.position.y, -(Box.localScale.z / 2) - r);
            var v = ParticleVelocity - Vector3.zero;
            if (Vector3.Dot(d, v) < 0) return Vector3.forward;
        }
        if ((Particle.position.z + r) >= (Box.localScale.z / 2)) {
            var d = Particle.position - new Vector3(Particle.position.x, Particle.position.y, (Box.localScale.z / 2) + r);
            var v = ParticleVelocity - Vector3.zero;
            if (Vector3.Dot(d, v) < 0) return Vector3.back;
        }
        return Vector3.zero;
    }

    private Vector3[] _velocities;
    private void Gravity(int particleIndex, Vector3 _tmpVel) {
        //velocity
        _velocities[particleIndex] += Vector3.down * accelerationDueToGravity * Time.fixedDeltaTime;
        //position
        Particles[particleIndex].position += (_velocities[particleIndex] + _tmpVel) / 2 * Time.fixedDeltaTime;
    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3[] tmpVelocities = _velocities;
        //collision
        for (int i = 0; i < Particles.Length; i++)
        {
            //===Gravity===
            _velocities[i] += Vector3.down * accelerationDueToGravity * Time.deltaTime;

            //check for collision with another sphere
            for (int j = 0; j < Particles.Length; j++)
            {
                if (j == i) continue;

                if (isSphereCollision(Particles[i], Particles[j])){
                    //check if velocities were already adjusted so that the collision can't happen anymore, if not, then compute new velocities
                    var d = Particles[i].position - Particles[j].position;
                    var v = _velocities[i] - _velocities[j];
                    if (Vector3.Dot(d, v) < 0)
                    {
                        //adjust velocities of both spheres
                        Vector3 a = (Particles[i].position - Particles[j].position);
                        Vector3 n2 = a / Mathf.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
                        _velocities[i] -= (1 + dampeningFactor) * Vector3.Dot(_velocities[i], n2) * n2;

                        a = (Particles[j].position - Particles[i].position);
                        n2 = a / Mathf.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
                        _velocities[j] -= (1 + dampeningFactor) * Vector3.Dot(_velocities[j], n2) * n2;
                    }
                    break;
                }   
            }
            //check for collision with bounds
            Vector3 n = isBoundsCollision(Particles[i], Bounds, _velocities[i]);

            _velocities[i] -= (1 + dampeningFactor) * Vector3.Dot(_velocities[i], n) * n;
            //compute new positions for particles
            Particles[i].position += (_velocities[i] + tmpVelocities[i]) / 2 * Time.deltaTime;
        }
    }
}
