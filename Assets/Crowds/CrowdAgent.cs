using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CrowdAgent : MonoBehaviour
{
    public Transform target;

    private NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Random.Range(4.0f, 5.0f);
        agent.SetDestination(target.position);
    }
}
