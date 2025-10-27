using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthUI : MonoBehaviour
{
    private VisualElement[] hearts;

    [SerializeField] private Sprite emptyHeart_spr;
    [SerializeField] private Sprite fullHeart_spr;

    public void Init(int maxHeartCount)
    {
        VisualElement Container = GetComponent<UIDocument>().rootVisualElement.Q(name: "Container");
    }
}
