using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool triggered = false;
    public void childTriggered()
    {
        {
            if (triggered == false)
            {
                Vector3 currentPos = transform.position;
                currentPos.y -= 1.6f;
                transform.position = currentPos;
                Debug.Log("Door moved");


                triggered = true;
            }

        }
    }
}
