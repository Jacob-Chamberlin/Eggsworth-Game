using UnityEngine;
using TMPro;

public class WinScreen : MonoBehaviour
{
    public TextMeshProUGUI displayTime;

    private void Start()
    {
        float recievedValue = PlayerPrefs.GetFloat("Time", 0f);
        float roundedFloat = Mathf.Round(recievedValue * 100f) / 100f;
        displayTime.text = "Time remaining: " + roundedFloat.ToString() + " seconds";
    }
}
