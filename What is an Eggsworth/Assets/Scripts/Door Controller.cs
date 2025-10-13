using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool triggered = false;
    public bool _inBossRoom;


    public float shutDis = 1.5f;

    public void childTriggered()
    {
        {
            if (triggered == false)
            {
                Vector3 currentPos = transform.position;
                currentPos.y -= shutDis;
                transform.position = currentPos;
                Debug.Log("Door moved");

                _inBossRoom = true;
                triggered = true;
            }

        }
    }
    public bool inBossRoom()
    {
        return _inBossRoom;
    }
}
