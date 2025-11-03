using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public int currHealth;
    public int maxHealth;

    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Image[] hearts;

    public PlayerHealth PlayerHealth;
    private void Update()
    {
        currHealth = PlayerHealth.currentHealth;
        maxHealth = PlayerHealth.maxHealth;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i< maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
