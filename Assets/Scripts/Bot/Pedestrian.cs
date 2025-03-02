using UnityEngine;
using UnityEngine.AI;

public class Pedestrian : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoints;
    public Animator animator;
    private int currentWaypoint = 0;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        MoveToNextWaypoint();
    }

    private void Update()
    {
        if (agent.remainingDistance < 0.5f)
        {
            MoveToNextWaypoint();
        }

        animator.SetBool("isWalking", agent.velocity.magnitude > 0.1f);
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;
        agent.SetDestination(waypoints[currentWaypoint].position);
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
    }
}
