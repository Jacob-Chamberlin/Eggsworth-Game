using UnityEngine;
using UnityEngine.SceneManagement;

public class Wincondition : MonoBehaviour
{
    [SerializeField] private TimerUI timer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //more logic if needed
            float timeRemaining = timer.getRemainingTime();
            PlayerPrefs.SetFloat("Time", timeRemaining);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Win Screen");
        }
    }
}
