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
        goldSweepText.text = (66 * (Mathf.Pow(1.1f,highestChapter))).ToString("F0") + "/시간";
        expSweepText.text = (99 * (Mathf.Pow(1.1f, highestChapter))).ToString("F0") + "/시간";

        DateTime now = DateTime.Now;
        int time = (int)(now - recentSweepTime).TotalMinutes / 60;
        if ((now - recentSweepTime).TotalMinutes / 60 > 0)
        {
            if (time > 6) time = 6;
            rewards[0].gameObject.SetActive(true);
            rewards[1].gameObject.SetActive(true);
            rewards[0].Setting(66 * (Mathf.Pow(1.1f, highestChapter)) * time);
            rewards[1].Setting(99 * (Mathf.Pow(1.1f, highestChapter)) * time);
            rewardTimeText.text = "<color=#FFE100>" + time.ToString() + "</color>" + "시간 소탕 보상";
            
        }


        //잔여량 표시하기
        if (PlayerPrefs.HasKey("sweepLeftVideo"))
        {
            int leftVideo = PlayerPrefs.GetInt("sweepLeftVideo");
            leftVideoText.text = "잔여:" + leftVideo;
        }
        else
        {
            leftVideoText.text = "잔여:3";
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
    //소탕
    public void SweepOnClick()
    {
        DateTime now = DateTime.Now;
        //시간에 따른 보상 지급
        int time = (int)(now - recentSweepTime).TotalMinutes / 60;
        if (time > 0)
        {

            if (time > 6) time = 6;
            //보상 획득 가능
            float highestChapter = BackEndGameData.Instance.UserGameData.highestChapter;
            goldSweepAount = 66 * (Mathf.Pow(1.1f, highestChapter)) * time;
            expSweepAount = 99 * (Mathf.Pow(1.1f, highestChapter)) * time;
            BackEndGameData.Instance.UserGameData.gold += (int)goldSweepAount;
            BackEndGameData.Instance.UserGameData.exp += (int)expSweepAount;
            BackEndGameData.Instance.UserQuestData.repeatQuest[3]++;

            //보상 받을 후 시간 초기화
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
        //비디오 시청
        int leftVideo =  PlayerPrefs.GetInt("sweepLeftVideo");
        if(leftVideo > 0)
        {
            AdsVideo.instance.ShowVideo();

            leftVideo--;
            PlayerPrefs.SetInt("sweepLeftVideo", leftVideo);
            //보상 제공
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
            PopUpMessageBase.instance.SetMessage("당일 광고를 모두 시청하였습니다");
        }
        
    }
    //빠른 소탕
    public void FastSweepOnClick()
    {
        //에너지 소모
        if(BackEndGameData.Instance.UserGameData.energy >= 15)
        {
            BackEndGameData.Instance.UserGameData.energy -= 15;

            //보상 제공 적용
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
