using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using BackEnd;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    [SerializeField] UserInfo user;

    [SerializeField] TextMeshProUGUI gemText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI energyText;

    //GameData
    int gem;
    int gold;
    int energy;

    //GameLevelDatas
    [SerializeField] TextMeshProUGUI gameLevelText;
    [SerializeField] TextMeshProUGUI highestWaveText;
    string[] gameLevelName = new string[] {"null스테이지" ,"따뜻한 안방"};
    int gameLevel = 1;  
    int highestWave = 0;


    private void Awake()
    {
        instance = this;
        user.GetUserInfoFromBackEnd();
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
    public void QuestBtnOnClick()
    {
        QuestManager.instance.UpdateQuestUI();
    }
    public void UpdateMainUI()
    {
        //상단 정보
        gemText.text = BackEndGameData.Instance.UserGameData.gem.ToString();
        goldText.text = BackEndGameData.Instance.UserGameData.gold.ToString();
        energyText.text = BackEndGameData.Instance.UserGameData.energy.ToString();

        //게임 레벨
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
