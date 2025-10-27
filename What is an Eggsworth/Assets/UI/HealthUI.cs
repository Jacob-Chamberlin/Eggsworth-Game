using System;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthUI : MonoBehaviour
{

    private Label m_HealthLabel;
    public PlayerHealth PlayerHealth;


    public void Start()
    {
        var uiDoc = GetComponent<UIDocument>();
        m_HealthLabel = uiDoc.rootVisualElement.Q<Label>("HealthLabel");
        PlayerHealth.onHealthChange += HealthChanged;

        HealthChanged();

        //updateTimer();
    }
    public void HealthChanged()
    {
        m_HealthLabel.text = $"{PlayerHealth.currentHealth}/{PlayerHealth.maxHealth}";
    }
}
