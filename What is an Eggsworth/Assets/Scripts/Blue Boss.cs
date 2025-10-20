using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlueBoss : MonoBehaviour
{

    public List<Transform> waypoints;
    public List<Transform> swoopPoints;
    Transform nextWaypoint;
    Transform nextSwoop;
    public Transform playerObj;

    //move speed values
    public const float flightSpd = 7f;
    public const float swoopSpd = 12.5f;

    //check if should become active
    public bool _playerIsHere = false;

    //random logic nums
    int doNext = 0;
    int didPrev = 99;

    //waypoint vars
    private float disToWaypoint = 0.1f;
    private float disToSwoopPos = 0.25f;
    public const float waitTime = 1f;
    int wayPointNum = 7;
    int prevWayPointNum = 99;

    //swoop atk vars
    private Vector3 targetPos;
    bool complete = false;
    public bool choseSide = false;
    public bool doingSwoop = false;
    const float atkDelay = 1f;
    public float currentDelay = 0f;
    bool LR = true;

    //spit attack vars
    public GameObject projectilePrefab;
    float initialSpd = 5f;
    float angleOffset = 45f;

    //initializers
    private Rigidbody2D rb;
    Vector3 pSide; //check the side the player is on relative to you

    //state machine
    public enum BossState { Inactive, Flying, Waiting, Decide, SwoopPrepare, Swoop, Attack2 }
    public BossState currentState;


    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = BossState.Inactive;
        //currentState = BossState.Flying;
    }
    public void playerIsHere()
    {
        nextWaypoint = waypoints[wayPointNum];
        currentState = BossState.Flying;
    }

    public void FixedUpdate()
    {
        pSide = playerObj.transform.InverseTransformPoint(transform.position);
        switch (currentState)
        {
            case BossState.Inactive:
                playerIsHere();
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
            case BossState.SwoopPrepare:
                prepareSwoop();
                break;
            case BossState.Swoop:
                Swoop();
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

    public void prepareSwoop()
    {
        if (choseSide == false)
        {
            Debug.Log("Prepare the Swoop");

            if (pSide.x < 0)
            {
                nextSwoop = swoopPoints[1];
            }
            if (pSide.x > 0)
            {
                nextSwoop = swoopPoints[0];
            }
            choseSide = true;
        }

        targetPos.x = nextSwoop.position.x;
        targetPos.y = playerObj.position.y;
        targetPos.z = transform.position.z;

        float step = swoopSpd * Time.deltaTime;
        float distance = Vector2.Distance(targetPos, transform.position);
        //Debug.Log(distance);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        if (distance <= disToSwoopPos)
        {
            currentDelay += Time.deltaTime;
            if (currentDelay >= atkDelay)
            {
                rb.linearVelocity = Vector3.zero;
                complete = true;
                currentState = BossState.Swoop;
            }

        }
        if (complete)
        {
            choseSide = false;
            currentDelay = 0;
        }

    }
    public void Swoop()
    {
        if (!choseSide)
        {
            //Debug.Log("Prepare the Swoop");
            if (pSide.x < 0)
            {
                LR = true;
            }
            if (pSide.x > 0)
            {
                LR = false;
            }
            choseSide = true;
            doingSwoop = true;
        }
        switch (LR)
        {
            case true:
                //Debug.Log("right");
                transform.Translate(Vector3.right * swoopSpd * 2.2f * Time.deltaTime);
                break;
            case false:
                //Debug.Log("left");
                transform.Translate(Vector3.right * -swoopSpd * 2.2f * Time.deltaTime);
                break;
        }

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

        switch (doNext)
        {
            case 0:
                currentState = BossState.Waiting;
                break;
            case 1:
                currentState = BossState.SwoopPrepare;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(targetPos, 0.5f);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (doingSwoop)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                currentState = BossState.Attack2;
            }
            else
            {
                currentState = BossState.Waiting;
            }
            doingSwoop = false;
        }

        complete = false;
        choseSide = false;
        currentDelay = 0;
        currentState = BossState.Flying;
    }
}
