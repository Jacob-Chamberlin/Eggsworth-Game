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
    public void OnCollisionEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hazard"))
        {
            
        }
    }
    private IEnumerator TakeDamage(int amount)
    {
        if (canTakeDmg)
        {
            canTakeDmg = false;
            hp = hp - amount;
            yield return new WaitForSeconds(damageCD);
            canTakeDmg = true;
        }
    }
}
