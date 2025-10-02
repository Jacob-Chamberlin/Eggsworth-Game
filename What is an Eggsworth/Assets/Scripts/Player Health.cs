using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int hp = 5;
    public bool canTakeDmg = true;
    public float damageCD = 2;

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
}
