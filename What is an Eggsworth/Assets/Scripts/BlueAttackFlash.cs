using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlueAttackFlash : MonoBehaviour
{
    [SerializeField] private Color _flashColor = Color.white;
    [SerializeField] private float _flashtime = 0.25f;
    [SerializeField] private float _flashDelayTime = 0f;
    [SerializeField] private bool delay = true;

    private float currFlashAmt = 0f;

    private SpriteRenderer[] sr;
    private Material[] mat;

    private Coroutine _flashCoroutine;
    private Coroutine _flashDelay;
    private Coroutine _delayReset;

    public void Awake()
    {
        sr = GetComponentsInChildren<SpriteRenderer>();
        Init();
    }
    private void Init()
    {
        mat = new Material[sr.Length];

        for (int i = 0; i < sr.Length; i++)
        {
            mat[i] = sr[i].material;
        }
    }
    private void Update()
    {
        if (currFlashAmt <= 0f)
        {
            delay = true;
        }
    }
    public void CallFlasher()
    {
        if(delay == true)
        {
           _flashDelay = StartCoroutine(DelayFlash());
        }
        else
        {
            _flashCoroutine = StartCoroutine(TellFlasher());
        }
        
    }
    private IEnumerator DelayFlash()
    {
        yield return new WaitForSeconds(_flashDelayTime);
        delay = false;
    }
    private IEnumerator ResetDelay()
    {
        yield return new WaitForSeconds(2f);
        delay = true;
    }
    private IEnumerator TellFlasher()
    {
        //set color
        SetFlashColor();

        //lerp flash
        currFlashAmt = 0f;
        float elaspedTime = 0f;
        while(elaspedTime < _flashtime)
        {
            elaspedTime += Time.deltaTime;

            currFlashAmt = Mathf.Lerp(1f, 0f, (elaspedTime / _flashtime));
            SetFlashAmount(currFlashAmt);
            yield return null;
        }
    }
    private void SetFlashColor()
    {
        for (int i = 0; i < mat.Length; i++)
        {
            mat[i].SetColor("_TellColor", _flashColor);
        }
    }

        private void SetFlashAmount(float amount)
    {
        for(int i = 0; i < mat.Length; i++)
        {
            mat[i].SetFloat("_FlashAmount", amount);
        }
    }
}
