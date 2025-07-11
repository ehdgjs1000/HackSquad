using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SweepManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldSweepText;
    [SerializeField] TextMeshProUGUI expSweepText;
    [SerializeField] TextMeshProUGUI leftVideoText, leftFastText;

    float goldSweepAount;
    float expSweepAount;

    private void Start()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        float highestChapter = BackEndGameData.Instance.UserGameData.highestChapter;
        goldSweepText.text = (100 * (Mathf.Pow(1.1f,highestChapter))).ToString("F0") + "/시간";
        expSweepText.text = (150 * (Mathf.Pow(1.1f, highestChapter))).ToString("F0") + "/시간";

        //잔여량 표시하기
        leftVideoText.text = "잔여:" + BackEndGameData.Instance.ToString();
    }
    public void ResetSweep()
    {


        UpdateUI();
    }
    //소탕
    public void SweepOnClick()
    {


        UpdateUI();
    }
    public void VideoSweepOnClick()
    {
        //비디오 시청

        //보상 제공
        float highestChapter = BackEndGameData.Instance.UserGameData.highestChapter;
        goldSweepAount = 100 * (Mathf.Pow(1.1f, highestChapter));
        expSweepAount = 150 * (Mathf.Pow(1.1f, highestChapter));
        BackEndGameData.Instance.UserGameData.gold += Mathf.FloorToInt(goldSweepAount);

        BackEndGameData.Instance.GameDataUpdate();
    }
    //빠른 소탕
    public void FastSweepOnClick()
    {
        //에너지 소모
        if(BackEndGameData.Instance.UserGameData.energy >= 15)
        {
            BackEndGameData.Instance.UserGameData.energy -= 15;

            //보상 제공 적용
            float highestChapter = BackEndGameData.Instance.UserGameData.highestChapter;
            goldSweepAount = 100 * (Mathf.Pow(1.1f, highestChapter));
            expSweepAount = 150 * (Mathf.Pow(1.1f, highestChapter));
            BackEndGameData.Instance.UserGameData.gold += Mathf.FloorToInt(goldSweepAount);

            BackEndGameData.Instance.GameDataUpdate();
        }
        
        UpdateUI();
    }

}
