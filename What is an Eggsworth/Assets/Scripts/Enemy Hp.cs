using System.Collections;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public int hp = 3;
    public bool canTakeDmg = true;
    public float damageCD = 0.2f;

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAtk"))
        {
            Debug.Log("collided player attack");
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
