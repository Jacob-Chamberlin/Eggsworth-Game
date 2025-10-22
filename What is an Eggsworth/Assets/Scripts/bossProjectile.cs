using UnityEngine;

public class bossProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    float spd = 9f;
    public int bounces = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * spd * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int collidedLayer = collision.gameObject.layer;
        if (LayerMask.LayerToName(collidedLayer) == "Ground")
        {
            bounces--;
        }
        if (LayerMask.LayerToName(collidedLayer) == "Player")
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("PlayerAtk"))
        {
            Destroy(gameObject);
        }

        if (bounces <= 0)
        {
            Destroy(gameObject);
        }
    }
}
