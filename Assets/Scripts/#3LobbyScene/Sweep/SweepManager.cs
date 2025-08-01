using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SweepManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldSweepText;
    [SerializeField] TextMeshProUGUI expSweepText;
    [SerializeField] TextMeshProUGUI leftVideoText;
    [SerializeField] ResultPanel resultPanel;
    [SerializeField] SweepReward[] rewards;
    [SerializeField] TextMeshProUGUI rewardTimeText;

    float goldSweepAount;
    float expSweepAount;

    DateTime recentSweepTime;

    private void Start()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        Timer();
        float highestChapter = BackEndGameData.Instance.UserGameData.highestChapter;
        goldSweepText.text = (66 * (Mathf.Pow(1.1f,highestChapter))).ToString("F0") + "/�ð�";
        expSweepText.text = (99 * (Mathf.Pow(1.1f, highestChapter))).ToString("F0") + "/�ð�";

        DateTime now = DateTime.Now;
        int time = (int)(now - recentSweepTime).TotalMinutes / 60;
        if ((now - recentSweepTime).TotalMinutes / 60 > 0)
        {
            if (time > 6) time = 6;
            rewards[0].gameObject.SetActive(true);
            rewards[1].gameObject.SetActive(true);
            rewards[0].Setting(66 * (Mathf.Pow(1.1f, highestChapter)) * time);
            rewards[1].Setting(99 * (Mathf.Pow(1.1f, highestChapter)) * time);
            rewardTimeText.text = "<color=#FFE100>" + time.ToString() + "</color>" + "�ð� ���� ����";
            
        }


        //�ܿ��� ǥ���ϱ�
        if (PlayerPrefs.HasKey("sweepLeftVideo"))
        {
            int leftVideo = PlayerPrefs.GetInt("sweepLeftVideo");
            leftVideoText.text = "�ܿ�:" + leftVideo;
        }
        else
        {
            leftVideoText.text = "�ܿ�:3";
        }
        
        LobbyManager.instance.UpdateUIAll();
    }
    private void Timer()
    {
        if (PlayerPrefs.HasKey("recentSweepTime"))
        {
            recentSweepTime = DateTime.ParseExact(PlayerPrefs.GetString("recentSweepTime"), "yyyy-MM-dd HH:mm:ss", null);
        }
        else
        {
            recentSweepTime = DateTime.Now;
            PlayerPrefs.SetString("recentSweepTime", recentSweepTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
    //����
    public void SweepOnClick()
    {
        DateTime now = DateTime.Now;
        //�ð��� ���� ���� ����
        int time = (int)(now - recentSweepTime).TotalMinutes / 60;
        if (time > 0)
        {

            if (time > 6) time = 6;
            //���� ȹ�� ����
            float highestChapter = BackEndGameData.Instance.UserGameData.highestChapter;
            goldSweepAount = 66 * (Mathf.Pow(1.1f, highestChapter)) * time;
            expSweepAount = 99 * (Mathf.Pow(1.1f, highestChapter)) * time;
            BackEndGameData.Instance.UserGameData.gold += (int)goldSweepAount;
            BackEndGameData.Instance.UserGameData.exp += (int)expSweepAount;
            BackEndGameData.Instance.UserQuestData.repeatQuest[3]++;

            //���� ���� �� �ð� �ʱ�ȭ
            SoundManager.instance.BtnClickPlay();
            PlayerPrefs.SetString("recentSweepTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            BackEndGameData.Instance.UserQuestData.questProgress[5]++;
            BackEndGameData.Instance.GameDataUpdate();
            OpenResultPanel(goldSweepAount, expSweepAount);
        }

        UpdateUI();
    }
    public void VideoSweepOnClick()
    {
        //���� ��û
        int leftVideo =  PlayerPrefs.GetInt("sweepLeftVideo");
        if(leftVideo > 0)
        {
            AdsVideo.instance.ShowVideo();

            leftVideo--;
            PlayerPrefs.SetInt("sweepLeftVideo", leftVideo);
            //���� ����
            float highestChapter = BackEndGameData.Instance.UserGameData.highestChapter;
            goldSweepAount = 6 * 66 * (Mathf.Pow(1.1f, highestChapter));
            expSweepAount = 6 * 99 * (Mathf.Pow(1.1f, highestChapter));
            BackEndGameData.Instance.UserGameData.gold += Mathf.FloorToInt(goldSweepAount);
            BackEndGameData.Instance.UserGameData.exp += Mathf.FloorToInt(expSweepAount);
            BackEndGameData.Instance.UserQuestData.questProgress[5]++;
            BackEndGameData.Instance.UserQuestData.repeatQuest[0]++;

            BackEndGameData.Instance.GameDataUpdate();
            UpdateUI();
            OpenResultPanel(goldSweepAount, expSweepAount);
        }
        else
        {
            PopUpMessageBase.instance.SetMessage("���� ���� ��� ��û�Ͽ����ϴ�");
        }
        
    }
    //���� ����
    public void FastSweepOnClick()
    {
        //������ �Ҹ�
        if(BackEndGameData.Instance.UserGameData.energy >= 15)
        {
            BackEndGameData.Instance.UserGameData.energy -= 15;

            //���� ���� ����
            SoundManager.instance.BtnClickPlay();
            float highestChapter = BackEndGameData.Instance.UserGameData.highestChapter;
            goldSweepAount = 6 * 66 * (Mathf.Pow(1.1f, highestChapter));
            expSweepAount = 6 * 99 * (Mathf.Pow(1.1f, highestChapter));
            BackEndGameData.Instance.UserGameData.gold += Mathf.FloorToInt(goldSweepAount);
            BackEndGameData.Instance.UserGameData.exp += Mathf.FloorToInt(expSweepAount);
            BackEndGameData.Instance.UserQuestData.questProgress[5]++;
            BackEndGameData.Instance.UserQuestData.repeatQuest[0]++;

            BackEndGameData.Instance.GameDataUpdate();
        }
        
        UpdateUI();
        OpenResultPanel(goldSweepAount, expSweepAount);
    }
    private void OpenResultPanel(float _goldAmount, float _expAmount)
    {
        resultPanel.gameObject.SetActive(true);
        StartCoroutine(resultPanel.UpdateUI(_goldAmount, _expAmount));
    }
}
