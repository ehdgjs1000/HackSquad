using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using BackEnd;
using DG.Tweening;
using System.Collections;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    [SerializeField] UserInfo user;

    [SerializeField] TextMeshProUGUI gemText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI energyText;
    [SerializeField] TextMeshProUGUI levelText;

    [SerializeField] GameObject midGameClearSet;
    [SerializeField] GameObject energyBuyPanel;
    [SerializeField] GameObject dungeonBtn;
    [SerializeField] GameObject inventoryPanel;
    //GameData
    int gem;
    int gold;
    int energy;

    //GameLevelDatas
    [SerializeField] TextMeshProUGUI gameLevelText;
    [SerializeField] TextMeshProUGUI highestWaveText;
    [SerializeField] Image chapterImage;
    [SerializeField] Sprite[] chapterSprites;
    string[] gameLevelName = new string[] {"null��������" ,"������ �ȹ�","�������� �ֹ�", "������ ȭ���",
    "������ ��ǻ�ͽ�", "å�� ������", "���� ���� ��ǻ��","4��","��Ÿ���","�ܿﵿȭ","�Ϲ�����","��������"};
    int gameLevel = 1;  
    int highestWave = 0;

    //ChapterClear
    [SerializeField] ChapterClear chapterClear;
    [SerializeField] QuestManager questManager;
    [SerializeField] SweepManager sweepManager;

    [SerializeField] SceneTransition sceneTransition;

    float startBtnclickTerm = 1.0f;
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
        if (startBtnclickTerm > 0.0f) startBtnclickTerm -= Time.deltaTime;

    }
    public void ResetPlayerPrefab()
    {
        PlayerPrefs.DeleteAll();
    }
    public void BuyEnergyOnClick()
    {
        energyBuyPanel.SetActive(true);
    }
    public void ChapterOnClick()
    {
        SoundManager.instance.BtnClickPlay();
        chapterClear.transform.DOScale(Vector3.one,0);
        chapterClear.UpdateRecentReward();
    }
    public void QuestBtnOnClick()
    {
        SoundManager.instance.BtnClickPlay();
        questManager.transform.DOScale(Vector3.one, 0);
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
        int gold = BackEndGameData.Instance.UserGameData.gold;
        if (gold >= 10000)
        {
            goldText.text = (gold / 1000).ToString() + "K";
        }
        else goldText.text = gold.ToString();
        energyText.text = BackEndGameData.Instance.UserGameData.energy.ToString() + "/" + (30+
            BackEndGameData.Instance.UserAbilityData.abilityLevel[9]);
        levelText.text = BackEndGameData.Instance.UserGameData.level.ToString();

        //���� ����
        gameLevel = (int)((BackEndGameData.Instance.UserGameData.highestChapter+1) / 2) + 1;
        gameLevelText.text = gameLevel.ToString()+". " +gameLevelName[gameLevel];
        highestWaveText.text = "�ִ� ���̺� <color=red>" + highestWave.ToString()+"/20</color>";
        chapterImage.sprite = chapterSprites[gameLevel-1];

        if (gameLevel > 1) midGameClearSet.SetActive(true);
        else midGameClearSet.SetActive(false);
        if (gameLevel > 2) dungeonBtn.SetActive(true);
        else dungeonBtn.SetActive(false);

    }
    public void GameStartOnClick()
    {
        if (HeroSetManager.instance.squadCount >= 3)
        {
            if(startBtnclickTerm <= 0.0f)
            {
                startBtnclickTerm = 3.0f;
                StartCoroutine(StartGame());
            }
            
        }else if (HeroSetManager.instance.squadCount < 3)
        {
            SoundManager.instance.ErrorClipPlay();
            PopUpMessageBase.instance.SetMessage("�����忡 �����̸� 3�� �̻� ������ּ���");
        }
    }
    IEnumerator StartGame()
    {
        if (BackEndGameData.Instance.UserGameData.energy >= 5)
        {
            SoundManager.instance.BtnClickPlay();
            //���� ������ �� Hero ���� �ѱ��
            ChangeScene.instance.SetHeros(gameLevel);
            ChangeScene.instance.chapterName = gameLevelName[gameLevel];
            BackEndGameData.Instance.UserGameData.energy -= 5;
            BackEndGameData.Instance.UserQuestData.questProgress[1]++;
            BackEndGameData.Instance.GameDataUpdate();

            StartCoroutine(sceneTransition.TransitionCoroutine());
            yield return new WaitForSeconds(2.0f);

            string scene = "Chapter" + gameLevel;
            SceneManager.LoadScene("InGameCommon");
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
        else PopUpMessageBase.instance.SetMessage("�������� �����մϴ�.");
    }
}
