using UnityEngine;
using UnityEngine.UI;

public class FollowPlayerUI : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Transform Reference1;
    [SerializeField] private Transform Reference2;
    [SerializeField] private Transform BarStart;
    [SerializeField] private Transform BarEnd;

    public float totalDistance = 0f;
    public float distanceFromStart = 0f;
    public float percentagePos = 0f;

    public void Awake()
    {
        totalDistance = Vector3.Distance(Reference1.position, Reference2.position);
    }
    public void Update()
    {
        distanceFromStart = Vector3.Distance(Reference1.position, Player.position);

        percentagePos = distanceFromStart/totalDistance;

        Vector3 newPos = Vector3.Lerp(BarStart.position, BarEnd.position, percentagePos);

        transform.position = newPos;
    }

}
