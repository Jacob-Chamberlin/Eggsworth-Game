using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool triggered = false;
    public float shutDis = 1.5f;


    public void Awake()
    {

    }

    public void childTriggered()
    {
        //GameManager gm = Camera.main.GetComponent<GameManager>();

        if (triggered == false)
        {
            Vector3 currentPos = transform.position;
            currentPos.y -= shutDis;
            transform.position = currentPos;
            Debug.Log("Door moved");

            //gm.inBossRoom();
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
