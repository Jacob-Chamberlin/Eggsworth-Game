using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlueBoss : MonoBehaviour
{
    public float flightSpd = 4.5f;
    public List<Transform> waypoints;
    public float disToWaypoint = 0.1f;
    public const float waitTime = 2f;

    public bool _playerIsHere = false;

    int doNext = 0;
    int didPrev = 99;

    private Rigidbody2D rb;
    Transform nextWaypoint;
    int wayPointNum = 7;
    int prevWayPointNum = 99;

    public enum BossState { Inactive, Flying, Waiting, Decide, Attack1, Attack2 }
    public BossState currentState;


    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        nextWaypoint = waypoints[wayPointNum];
        //currentState = BossState.Inactive;
        currentState = BossState.Flying;
    }
    public void playerIsHere()
    {
        currentState = BossState.Flying;
    }

    public void FixedUpdate()
    {
        switch (currentState)
        {
            case BossState.Inactive:
                break;
            case BossState.Flying:
                Flight();
                break;
            case BossState.Waiting:
                Waiting();
                break;
            case BossState.Decide:
                decideNextAction();
                break;
            case BossState.Attack1:
                horizontalSwoop();
                break;
            case BossState.Attack2:
                JonkingIt();
                break;
            default: break;
        }
    }
    public void Waiting()
    {
        rb.linearVelocity = Vector3.zero;
        StartCoroutine(waitTimer(waitTime));
    }

    public void Flight()
    {
        //fly to initial waypoint
        Vector2 dirToWaypoint = (nextWaypoint.position - transform.position).normalized;

        float distance = Vector2.Distance(nextWaypoint.position, transform.position);
        rb.linearVelocity = dirToWaypoint * flightSpd;

        //check if reached waypoint
        if (distance <= disToWaypoint)
        {
            //find random waypoint
            int randNum = Random.Range(0, waypoints.Count);

            if (randNum != prevWayPointNum)
            {
                prevWayPointNum = wayPointNum;
                wayPointNum = randNum;
            }
            nextWaypoint = waypoints[wayPointNum];
            currentState = BossState.Decide;
        }
    }
    public void horizontalSwoop()
    {
        Debug.Log("Do the Swoop");
        currentState = BossState.Waiting;
    }
    public void JonkingIt()
    {
        Debug.Log("Boutta Jonk it");
        currentState = BossState.Waiting;
    }
    public void decideNextAction()
    {
        //makes sure that the next option != prev option
        do
        {
            doNext = Random.Range(0, 3);

        }
        while (doNext == didPrev);
        didPrev = doNext;

        switch(doNext)
        {
            case 0:
                currentState = BossState.Waiting; 
                break;
            case 1:
                currentState = BossState.Attack1;
                break;
            case 2:
                currentState = BossState.Attack2;
                break;
        }
    }
    IEnumerator waitTimer(float time)
    {
        yield return new WaitForSeconds(time);
        currentState = BossState.Flying;
    }
}
