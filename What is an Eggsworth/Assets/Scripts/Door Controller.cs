using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameManager gm;

    private bool triggered = false;
    public float shutDis = 1.5f;


    public void Awake()
    {

    }

    public void childTriggered()
    {
        if (triggered == false)
        {
            gm.inBossRoom();
            Vector3 currentPos = transform.position;
            currentPos.y -= shutDis;
            transform.position = currentPos;
            Debug.Log("Door moved");

            triggered = true;
        }

    }
    public void bossDefeated()
    {
        Vector3 currentPos = transform.position;
        currentPos.y += shutDis;
        transform.position = currentPos;
    }
}
