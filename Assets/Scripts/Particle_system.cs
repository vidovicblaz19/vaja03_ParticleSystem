using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_system : MonoBehaviour
{
    public float StartSpeedMin;
    public float StartSpeedMax;

    public Material particleMaterial;

    public float Lifetime;
    public float DropletSize;
    public Color Color;

    public float Duration;


    public int sphereCount;
    //public float sphereRadius = 1f;

    private void Awake()
    {
        //ignores collisions between droplets
        // Physics.IgnoreLayerCollision(7, 7);
    }


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sphereCount; i++)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.layer = 7;
            sphere.transform.localScale = new Vector3(1f, 1f, 1f);

            sphere.GetComponent<Renderer>().material = particleMaterial;

            Rigidbody sphereRB = sphere.AddComponent<Rigidbody>();
            sphereRB.collisionDetectionMode = CollisionDetectionMode.Continuous;

            //faucet position
            //sphere.transform.position = new Vector3(0,0,0);

            sphere.transform.position = new Vector3(Random.Range((-this.transform.localScale.x + sphere.transform.localScale.x) / 2, (this.transform.localScale.x - sphere.transform.localScale.x) / 2),
                                                    Random.Range((-this.transform.localScale.y + sphere.transform.localScale.y) / 2, (this.transform.localScale.y - sphere.transform.localScale.y) / 2), 
                                                    Random.Range((-this.transform.localScale.z + sphere.transform.localScale.z) / 2, (this.transform.localScale.z - sphere.transform.localScale.z) / 2));
            
            sphere.transform.parent = this.transform.GetChild(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
