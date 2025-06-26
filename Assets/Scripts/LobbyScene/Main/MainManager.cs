using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    string[] gameLevelName = new string[] {"null스테이지" ,"1스테이지이름"};
    int gameLevel = 1;  
    int highestWave = 0;


    private void Awake()
    {
        instance = this;
        user.GetUserInfoFromBackEnd();
    }
    private void Start()
    {

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

            //SceneManager.LoadScene("GameScene");

            string scene = "Chapter" + gameLevel;
            SceneManager.LoadScene("InGameCommon");
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
        

    }
}
