using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBoss : MonoBehaviour
{
    public float flightSpd = 4.5f;
    public List<Transform> waypoints;
    public float disToWaypoint = 0.1f;
    public float waitTime = 1.5f;

    public bool isAlive = true;
    public bool canMove = true;

    private Rigidbody2D rb;
    Transform nextWaypoint;
    int wayPointNum = 7;
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
                StartCoroutine(Flight());
            }
            else
            {
                rb.linearVelocity = Vector3.zero;
            }
        }
    }

    IEnumerator Flight()
    {
        //fly to random waypoint
        Vector2 dirToWaypoint = (nextWaypoint.position - transform.position).normalized;

        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.linearVelocity = dirToWaypoint * flightSpd;

        if (distance <= disToWaypoint)
        {
            //find random waypoint
            int randNum = Random.Range(0, waypoints.Count);

            if (randNum != prevWayPointNum)
            {
                prevWayPointNum = wayPointNum;
                wayPointNum = randNum;
            }
            //int range = Random.Range(5, -5);
            //int randNum = Mathf.Clamp(range, 0, waypoints.Count);

            //prevWayPointNum = wayPointNum;
            //wayPointNum = randNum;

            yield return new WaitForSeconds(waitTime);
            nextWaypoint = waypoints[wayPointNum];

        }
    }
    IEnumerator delayedFlight()
    {
        yield return new WaitForSeconds(waitTime);
        bool wait = false;
    }
}
