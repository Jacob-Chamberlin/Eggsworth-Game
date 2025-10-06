using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int hp = 5;
    public bool canTakeDmg = true;
    public float damageCD = 2;
    public string target = "RespawnPoint";


    // Update is called once per frame
    void Update()
    {
        
        if (hp > 0)
        {

        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            Debug.Log("collided with hazard");
            GameObject closestTarget = FindClosestObjectWithTag(target);
            transform.position = closestTarget.transform.position;
            StartCoroutine(TakeDamage(1));
        }
    }
    private IEnumerator TakeDamage(int amount)
    {
        if (canTakeDmg)
        {
            hp = hp - amount;
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
