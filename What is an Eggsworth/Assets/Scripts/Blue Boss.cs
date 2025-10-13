using System.Collections.Generic;
using UnityEngine;

public class BlueBoss : MonoBehaviour
{
    public float flightSpd = 2.5f;
    public List<Transform> waypoints;
    public float disToWaypoint = 0.1f;

    public bool isAlive = true;
    public bool canMove = true;

    private Rigidbody2D rb;
    Transform nextWaypoint;
    int wayPointNum = 0;
    int prevWayPointNum = 99;


    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextWaypoint = waypoints[wayPointNum];
    }

    public void FixedUpdate()
    {
        if (isAlive)
        {
            if (canMove)
            {
                Flight();
            }
            else
            {
                rb.linearVelocity = Vector3.zero;
            }
        }
    }

    private void Flight()
    {
        //fly to random waypoint
        Vector2 dirToWaypoint = (nextWaypoint.position - transform.position).normalized;

        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.linearVelocity = dirToWaypoint * flightSpd;

        if (distance <= disToWaypoint)
        {
            //random waypoint
            int randNum = Random.Range(0, waypoints.Count);
            if (randNum != prevWayPointNum)
            {
                prevWayPointNum = wayPointNum;
                wayPointNum = randNum;
            }

            nextWaypoint = waypoints[wayPointNum];
        }
    }
}
