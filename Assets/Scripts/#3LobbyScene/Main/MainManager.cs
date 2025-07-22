using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using BackEnd;
using DG.Tweening;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    [SerializeField] UserInfo user;

    [SerializeField] TextMeshProUGUI gemText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI energyText;
    [SerializeField] TextMeshProUGUI levelText;

    [SerializeField] GameObject midGameClearSet;
    //GameData
    int gem;
    int gold;
    int energy;

    //GameLevelDatas
    [SerializeField] TextMeshProUGUI gameLevelText;
    [SerializeField] TextMeshProUGUI highestWaveText;
    string[] gameLevelName = new string[] {"null��������" ,"������ �ȹ�","�������� �ֹ�", "������ ȭ���",
    "������ ��ǻ�ͽ�", "å�� ������", "���� ���� ��ǻ��","4��"};
    int gameLevel = 1;  
    int highestWave = 0;

    //ChapterClear
    [SerializeField] ChapterClear chapterClear;
    [SerializeField] QuestManager questManager;
    [SerializeField] SweepManager sweepManager;

    private void Awake()
    {
        instance = this;
        user.GetUserInfoFromBackEnd();
    }
    private void Start()
    {
        UpdateMainUI();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Backend.Utils.GetServerTime(callback =>
            {
                string serverTime = callback.GetFlattenJSON()["utcTime"].ToString();
                DateTime parsedDate = DateTime.Parse(serverTime);
                Debug.Log(serverTime + " : " + parsedDate);
                Debug.Log(DateTime.Now);
                Debug.Log(DateTime.UtcNow);
            });
        }
    }
    public void ChapterOnClick()
    {
        SoundManager.instance.BtnClickPlay();
        chapterClear.gameObject.SetActive(true);
        chapterClear.UpdateRecentReward();
    }
    public void QuestBtnOnClick()
    {
        SoundManager.instance.BtnClickPlay();
        QuestManager.instance.UpdateQuestUI();
    }
    public void SweepOnClick()
    {
        SoundManager.instance.BtnClickPlay();
        sweepManager.gameObject.SetActive(true);
        sweepManager.UpdateUI();
    }
    public void UpdateMainUI()
    {
        //��� ����
        gemText.text = BackEndGameData.Instance.UserGameData.gem.ToString();
        goldText.text = BackEndGameData.Instance.UserGameData.gold.ToString();
        energyText.text = BackEndGameData.Instance.UserGameData.energy.ToString();
        levelText.text = BackEndGameData.Instance.UserGameData.level.ToString();

        //���� ����
        gameLevel = (int)((BackEndGameData.Instance.UserGameData.highestChapter+1) / 2) + 1;
        gameLevelText.text = gameLevel.ToString()+". " +gameLevelName[gameLevel];
        highestWaveText.text = "�ִ� ���̺� <color=red>" + highestWave.ToString()+"/20</color>";

        if (gameLevel > 1) midGameClearSet.SetActive(true);
        else midGameClearSet.SetActive(false);

    }
    public void GameStartOnClick()
    {
        if (HeroSetManager.instance.squadCount > 0)
        {
            SoundManager.instance.BtnClickPlay();
            //���� ������ �� Hero ���� �ѱ��
            ChangeScene.instance.SetHeros();
            ChangeScene.instance.chapterName = gameLevelName[gameLevel];
            BackEndGameData.Instance.UserQuestData.questProgress[1]++;
            BackEndGameData.Instance.GameDataUpdate();

            string scene = "Chapter" + gameLevel;
            SceneManager.LoadScene("InGameCommon");
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }else if (HeroSetManager.instance.squadCount <= 0)
        {
            SoundManager.instance.ErrorClipPlay();
            PopUpMessageBase.instance.SetMessage("�����忡 �����̸� ������ּ���");
        }
    }
}
