using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DoctorPatrol : MonoBehaviour
{
    // Reference to NavMeshAgent component
    private NavMeshAgent nav;

    // Movement Speed
    public float patrolSpeed = 2.0f;

    // Waypoints
    public Transform[] waypoints;

    private int curWaypoint = 0;
    private int maxWaypoint;

    public float minWaypointDistance = 0.1f;

    // When the game starts...
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();

        maxWaypoint = waypoints.Length - 1;
    }

    // Every frame...
    private void Update()
    {
        Patrolling();
    }

    public void Patrolling()
    {
        // Set the ai agents movement speed to patrol speed
        nav.speed = patrolSpeed;

        // Create two Vector3 variables, one to buffer the ai agents local position, the other to
        // buffer the next waypoints position
        Vector3 tempLocalPosition;
        Vector3 tempWaypointPosition;

        // Agents position (x, set y to 0, z)
        tempLocalPosition = transform.position;
        tempLocalPosition.y = 0f;

        // Current waypoints position (x, set y to 0, z)
        tempWaypointPosition = waypoints[curWaypoint].position;
        tempWaypointPosition.y = 0f;

        // Is the distance between the agent and the current waypoint within the minWaypointDistance?
        if (Vector3.Distance(tempLocalPosition, tempWaypointPosition) <= minWaypointDistance)
        {
            // Have we reached the last waypoint?
            if (curWaypoint == maxWaypoint)
                // If so, go back to the first waypoint and start over again
                curWaypoint = 0;
            else
                // If we haven't reached the Last waypoint, just move on to the next one
                curWaypoint++;
        }

        // Set the destination for the agent
        // The navmesh agent is going to do the rest of the work
        nav.SetDestination(waypoints[curWaypoint].position);
    }
}