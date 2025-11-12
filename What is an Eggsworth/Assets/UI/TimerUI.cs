using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private float initialTime = 60f; //seconds
    private float currentTime;
    private Label m_TimerLabel;
    bool m_Running = true;

    public void OnEnable()
    {
        var uiDoc = GetComponent<UIDocument>();
        m_TimerLabel = uiDoc.rootVisualElement.Q<Label>("TimerLabel");


        currentTime = initialTime;
        updateTimer();
    }

    private void Update()
    {
        if(currentTime > 0 && m_Running)
        {
            currentTime -= Time.deltaTime;
            updateTimer();
        }
        else if (currentTime < 0 && m_Running)
        {
            currentTime = 0;
            updateTimer();
            enabled = false;
            SceneManager.LoadScene(5);
        }
    }

    public void updateTimer()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        m_TimerLabel.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
    }
    public void stopTimer()
    {
        m_Running = false;
    }
}
