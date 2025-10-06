using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private GameObject atkPrefab;

    private float spawnDisFor = .7f;
    private float spawnDisDown = -.2f;
    private float atkCD = 0.3f;
    private float hitboxDelay = 0f;//for animation purposes later
    private float hitboxLinger = 0.05f;
    private bool canAtk = true;

    private IEnumerator spawnHitbox()
    {
        canAtk = false;
        if (sr.flipX == false)
        {
            Vector3 spawnPos = transform.position + transform.right * spawnDisFor + transform.up * spawnDisDown;
            GameObject hitbox = Instantiate(atkPrefab, spawnPos, transform.rotation);
            yield return new WaitForSeconds(hitboxLinger);
            Destroy(hitbox);
        }
        if (sr.flipX == true)
        {
            Vector3 spawnPos = transform.position + transform.right * -spawnDisFor + transform.up * spawnDisDown;
            GameObject hitbox = Instantiate(atkPrefab, spawnPos, transform.rotation);
            yield return new WaitForSeconds(hitboxLinger);
            Destroy(hitbox);
        }
        yield return new WaitForSeconds(atkCD);
        canAtk = true;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (canAtk)
        {
            if (context.performed)
            {
                StartCoroutine(spawnHitbox());
            }
        }
    }
}
