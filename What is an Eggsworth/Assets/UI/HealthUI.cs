using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthUI : MonoBehaviour
{
    private VisualElement[] hearts;

    [SerializeField] private Sprite emptyHeart_spr;
    [SerializeField] private Sprite fullHeart_spr;

    private const string containerName = "Container";

    public void Init(int maxHealthCount)
    {
        Debug.Log("initalize health");
        VisualElement Container = GetComponent<UIDocument>().rootVisualElement.Q(containerName);

        for (int i = 0; 1 < maxHealthCount; i++)
        {
            Container.Add(child: fullHeartElement(maxHealthCount));
        }

    }
    public void updateHealth(int maxHealth, int currentHealth)
    {
        int lossHealth = maxHealth - currentHealth;
        VisualElement Container = GetComponent<UIDocument>().rootVisualElement.Q(containerName);

        for (int i = 0; 1 < currentHealth; i++)
        {
            Container.Add(child: fullHeartElement(maxHealth));
        }
        for (int i = 0; 1 < lossHealth; i++)
        {
            Container.Add(child: emptyHeartElement(maxHealth));
        }
    }

    private VisualElement emptyHeartElement(int maxHealth)
    {
        VisualElement heart = new VisualElement
        {
            style =
            {
                width = Length.Percent(100/maxHealth),
                marginBottom = 1,
                marginLeft = 1,
                marginTop = 1,
                marginRight = 1,
                backgroundImage = new StyleBackground(emptyHeart_spr)
            }

        };
        return heart;
    }
    private VisualElement fullHeartElement(int maxHealth)
    {
        VisualElement heart = new VisualElement
        {
            style =
            {
                width = Length.Percent(100/maxHealth),
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
