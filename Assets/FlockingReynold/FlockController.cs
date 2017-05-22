using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlockController : MonoBehaviour
{
    public float minVelocity = 1;   // min velocity
    public float maxVelocity = 8;   // max velocity
    public int flockSize = 20;  // number of flocks in the group

    // How far the boids should stick to the center (the more weight stick closer to the center)
    public float centerWeight = 1;
    public float velocityWeight = 1;    // allignment behaviour

    // How far each boid should be separated within the flock
    public float separationWeight = 1;

    // How close each boid should follow to the leader (the more weight make the closer follow)
    public float followWeight = 1;

    // Additional Random Noise
    public float randomizeWeight = 1;

    public Flock prefab;
    public Transform target;

    // Center position of the flock in the group
    internal Vector3 flockCenter;
    internal Vector3 flockVelocity; // average velocity

    public ArrayList flockList = new ArrayList();


    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < flockSize; i++)
        {
            Flock flock = Instantiate(prefab, transform.position, transform.rotation) as Flock;

            flock.transform.parent = transform;
            flock.controller = this;
            flockList.Add(flock);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the center and velocity of the whole flock group
        Vector3 center = Vector3.zero;
        Vector3 velocity = Vector3.zero;

        foreach (Flock flock in flockList)
        {
            center += flock.transform.localPosition;
            velocity += flock.GetComponent<Rigidbody>().velocity;
        }

        flockCenter = center / flockSize;
        flockVelocity = velocity / flockSize;
    }
}
