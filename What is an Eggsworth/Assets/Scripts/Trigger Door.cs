using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    public DoorController dc;
    public GameObject door;

    public void Start()
    {
        dc = door.GetComponent<DoorController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            {
                Debug.Log("child triggered");
                dc.childTriggered();
            }
        }
    }
}
