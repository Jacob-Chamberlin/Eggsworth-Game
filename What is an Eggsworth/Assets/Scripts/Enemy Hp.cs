using System.Collections;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    [SerializeField] private DoorController dc;
    [SerializeField] private TimerUI TimerUI;

    public int hp = 3;
    public bool canTakeDmg = true;
    public float damageCD = 0.2f;
    public string myTag;

    // Update is called once per frame
    private void Start()
    {
        myTag = gameObject.tag;
    }

    void Update()
    {
        if (hp <= 0 && myTag == "Boss")
        {
            dc.bossDefeated();
            TimerUI.stopTimer();

            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        if (hp <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    public void setHealth(int health)
    {
        hp = health;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAtk"))
        {
            //Debug.Log("collided player attack");
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
