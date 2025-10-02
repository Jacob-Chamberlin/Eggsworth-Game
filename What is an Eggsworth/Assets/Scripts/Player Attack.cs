using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private GameObject atkPrefab;

    private float spawnDisFor = 1f;
    private float spawnDisDown = -1f;
    private float hitboxDelay = 0f;
    private float hitboxLinger = 0.2f;

    private IEnumerator spawnHitbox()
    {
        if (sr.flipX == false)
        {
            spawnDisFor = 5f;
        }
        if (sr.flipX == true)
        {
            spawnDisFor = -5f;
        }

        //Vector3 spawnPos = transform.position + transform.forward * spawnDisFor +
        //transform.up * spawnDisDown;
        float DisFor = transform.position.x * spawnDisFor;
        float DisDown = transform.position.y * spawnDisDown;
        Vector3 spawnPos = new Vector3(DisFor,DisDown, 0f);
        GameObject hitbox = Instantiate(atkPrefab, spawnPos, transform.rotation);
        yield return new WaitForSeconds(hitboxLinger);
        Destroy(hitbox);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            StartCoroutine(spawnHitbox());
        }
    }
}
