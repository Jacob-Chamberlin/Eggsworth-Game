using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthUI : MonoBehaviour
{
    private VisualElement[] hearts;
    public PlayerHealth health; 

    [SerializeField] private Sprite emptyHeart_spr;
    [SerializeField] private Sprite fullHeart_spr;

    private const string containerName = "Container";

    public void Awake()
    {
        health = GetComponent<PlayerHealth>();
    }
    public void Init(int maxHeartCount)
    {
        VisualElement Container = GetComponent<UIDocument>().rootVisualElement.Q(name: "Container");

        for (int i = 0; 1 < maxHeartCount; i++)
        {
            Container.Add(child: fullHeartElement(health.maxHealth, health.currentHealth));
        }

    }
    public void emptyHeartElement(int maxHealth, int currentHealth)
    {

    }

    private VisualElement fullHeartElement(int maxHealth, int currentHealth)
    {
        VisualElement heart = new VisualElement
        {
            style =
            {
                width = Length.Percent(20),
                marginBottom = 1,
                marginLeft = 1,
                marginTop = 1,
                marginRight = 1,
                backgroundImage = new StyleBackground(fullHeart_spr)
            }

        };
        return heart;
    }
}
