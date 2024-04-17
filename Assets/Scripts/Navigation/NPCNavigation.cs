using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class NPCNavigation : MonoBehaviour
{

    NavMeshAgent agent;

    [SerializeField, Min(0)] float distanceThreshold = 0.1f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    // Start is called before the first frame update
    void Start()
    {
        SetDestination();               
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance <= distanceThreshold) SetDestination();
    }


    private void SetDestination()
    {
        agent.destination = NavPointers.instance.GetNewPoint(agent.destination);
    }
}
