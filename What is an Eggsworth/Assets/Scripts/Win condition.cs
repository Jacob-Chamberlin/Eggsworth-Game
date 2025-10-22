using UnityEngine;
using UnityEngine.SceneManagement;

public class Wincondition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //more logic if needed
            Debug.Log("Win");
            SceneManager.LoadScene("Win Screen");
        }
    }
}
