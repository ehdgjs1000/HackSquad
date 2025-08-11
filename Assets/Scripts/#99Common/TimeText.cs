using UnityEngine;
using TMPro;

public class TimeText : MonoBehaviour
{
    TextMeshProUGUI timerText;
    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        timerText.text = TimeManager.instance.dailyStr;
    }

}
