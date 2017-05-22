using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    internal FlockController controller;
    private Rigidbody _rigidbody = null;

    // Use this for initialization
    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller)
        {
            Vector3 relativePos = steer() * Time.deltaTime;

            if (relativePos != Vector3.zero)
                _rigidbody.velocity = relativePos;

            // enforce minimum and maximum speeds for the boids
            float speed = _rigidbody.velocity.magnitude;

            if(speed > controller.maxVelocity)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * controller.maxVelocity;
            }
            else if (speed < controller.minVelocity)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * controller.minVelocity;
            }
        }
    }

    private Vector3 steer()
    {
         Vector3 center = controller.flockCenter - transform.localPosition;  // cohesion

        Vector3 velocity = controller.flockVelocity - _rigidbody.velocity;  // alignment

        Vector3 follow = controller.target.localPosition - transform.localPosition; // follow leader

        Vector3 separation = Vector3.zero;

        foreach(Flock flock in controller.flockList)
        {
            if(flock != this)
            {
                Vector3 relativePos = transform.localPosition - flock.transform.localPosition;

                separation += relativePos / (relativePos.sqrMagnitude);
            }
        }

        // randomize
        Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);

        randomize.Normalize();

        return (controller.centerWeight * center +
                controller.velocityWeight * velocity +
                controller.separationWeight * separation +
                controller.followWeight * follow +
                controller.randomizeWeight * randomize);
    }
}
