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
        goldSweepText.text = (100 * (Mathf.Pow(1.1f,highestChapter))).ToString("F0") + "/�ð�";
        expSweepText.text = (150 * (Mathf.Pow(1.1f, highestChapter))).ToString("F0") + "/�ð�";

        //�ܿ��� ǥ���ϱ�
        leftVideoText.text = "�ܿ�:" + BackEndGameData.Instance.ToString();
    }
    public void ResetSweep()
    {


        UpdateUI();
    }
    //����
    public void SweepOnClick()
    {


        UpdateUI();
    }
    public void VideoSweepOnClick()
    {
        //���� ��û

        //���� ����
        float highestChapter = BackEndGameData.Instance.UserGameData.highestChapter;
        goldSweepAount = 100 * (Mathf.Pow(1.1f, highestChapter));
        expSweepAount = 150 * (Mathf.Pow(1.1f, highestChapter));
        BackEndGameData.Instance.UserGameData.gold += Mathf.FloorToInt(goldSweepAount);

        BackEndGameData.Instance.GameDataUpdate();
    }
    //���� ����
    public void FastSweepOnClick()
    {
        //������ �Ҹ�
        if(BackEndGameData.Instance.UserGameData.energy >= 15)
        {
            BackEndGameData.Instance.UserGameData.energy -= 15;

            //���� ���� ����
            float highestChapter = BackEndGameData.Instance.UserGameData.highestChapter;
            goldSweepAount = 100 * (Mathf.Pow(1.1f, highestChapter));
            expSweepAount = 150 * (Mathf.Pow(1.1f, highestChapter));
            BackEndGameData.Instance.UserGameData.gold += Mathf.FloorToInt(goldSweepAount);

            BackEndGameData.Instance.GameDataUpdate();
        }
        
        UpdateUI();
    }

}
