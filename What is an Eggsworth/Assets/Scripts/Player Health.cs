using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public bool canTakeDmg = true;
    public float damageCD = 2;
    public string target = "RespawnPoint";

    public Action onHealthChange;

    public HealthUI healthUI;

    public void Awake()
    {
        currentHealth = maxHealth;
        healthUI = GetComponent<HealthUI>();
        GetComponent<HealthUI>().Init(maxHealth);
    }
    public int getMaxHealth()
    {
        return maxHealth;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            Debug.Log("collided with hazard");
            GameObject closestTarget = FindClosestObjectWithTag(target);
            transform.position = closestTarget.transform.position;
        }
        if (!other.gameObject.CompareTag("level element"))
        {
            StartCoroutine(TakeDamage(1));
        }
    }
    private IEnumerator TakeDamage(int amount)
    {
        if (canTakeDmg)
        {
            //healthUI.updateHealth(maxHealth, currentHealth);
            currentHealth = currentHealth - amount;
            onHealthChange?.Invoke();
            canTakeDmg = false;

            yield return new WaitForSeconds(damageCD);
            canTakeDmg = true;
        }
    }
    GameObject FindClosestObjectWithTag(string tag)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        GameObject closestObject = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject obj in taggedObjects)
        {
            Vector3 directionToTarget = obj.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude; // Use squared magnitude for performance

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestObject = obj;
            }
        }
        return closestObject;
    }
}
