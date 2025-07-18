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

    //GameData
    int gem;
    int gold;
    int energy;

    //GameLevelDatas
    [SerializeField] TextMeshProUGUI gameLevelText;
    [SerializeField] TextMeshProUGUI highestWaveText;
    string[] gameLevelName = new string[] {"null스테이지" ,"따뜻한 안방","냄새나는 주방", "축축한 화장실",
    "나른한 컴퓨터실", "책상 위에서"};
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
        chapterClear.gameObject.SetActive(true);
        chapterClear.UpdateRecentReward();
    }
    public void QuestBtnOnClick()
    {
        QuestManager.instance.UpdateQuestUI();
    }
    public void SweepOnClick()
    {
        sweepManager.gameObject.SetActive(true);
        sweepManager.UpdateUI();
    }
    public void UpdateMainUI()
    {
        //상단 정보
        gemText.text = BackEndGameData.Instance.UserGameData.gem.ToString();
        goldText.text = BackEndGameData.Instance.UserGameData.gold.ToString();
        energyText.text = BackEndGameData.Instance.UserGameData.energy.ToString();
        levelText.text = BackEndGameData.Instance.UserGameData.level.ToString();

        //게임 레벨
        gameLevel = (int)((BackEndGameData.Instance.UserGameData.highestChapter+1) / 2) + 1;
        gameLevelText.text = gameLevel.ToString()+". " +gameLevelName[gameLevel];
        highestWaveText.text = "최대 웨이브 <color=red>" + highestWave.ToString()+"/20</color>";

    }
    public void GameStartOnClick()
    {
        if (HeroSetManager.instance.squadCount > 0)
        {

            //다음 씬으로 고른 Hero 정보 넘기기
            ChangeScene.instance.SetHeros();
            ChangeScene.instance.chapterName = gameLevelName[gameLevel];
            BackEndGameData.Instance.UserQuestData.questProgress[1]++;
            BackEndGameData.Instance.GameDataUpdate();

            string scene = "Chapter" + gameLevel;
            SceneManager.LoadScene("InGameCommon");
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }else if (HeroSetManager.instance.squadCount <= 0)
        {
            PopUpMessageBase.instance.SetMessage("스쿼드에 핵쟁이를 등록해주세요");
        }
    }
}
