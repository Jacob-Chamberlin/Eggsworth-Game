using System;
using UnityEngine;
using UnityEngine.UIElements; // For UI Toolkit

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private float initialTime = 60f; //seconds
    private float currentTime;
    private Label m_TimerLabel;

    public void OnEnable()
    {
        var uiDoc = GetComponent<UIDocument>();
        m_TimerLabel = uiDoc.rootVisualElement.Q<Label>("TimerLabel");


        currentTime = initialTime;
        updateTimer();
    }

    private void Update()
    {
        if(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            updateTimer();
        }
        else
        {
            currentTime = 0;
            updateTimer();
            enabled = false;
        }
    }

    public void updateTimer()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        m_TimerLabel.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
    }
}
