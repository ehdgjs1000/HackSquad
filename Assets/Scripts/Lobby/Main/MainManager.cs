using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{

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
    string[] gameLevelName = new string[] {"null스테이지" ,"스테이지이름"};
    int gameLevel = 1;  
    int highestWave = 0;


    private void Awake()
    {
        
    }
    private void Start()
    {
        UpdateMainUI();
    }
    //계정 계좌 정보 동기화(로비로부터 가져옴)
    private void UpdateAccountInfo()
    {
        gem = LobbyManager.instance.ReturnAccount(0);
        gold = LobbyManager.instance.ReturnAccount(1);
        energy = LobbyManager.instance.ReturnAccount(2);
    }
    private void UpdateMainUI()
    {
        //상단 정보
        UpdateAccountInfo();
        gemText.text = gem.ToString();
        goldText.text = gold.ToString();
        energyText.text = energy.ToString();

        //게임 레벨
        gameLevelText.text = gameLevel.ToString()+". " +gameLevelName[gameLevel];
        highestWaveText.text = "최대 웨이브 <color=red>" + highestWave.ToString()+"/20</color>";

    }
    public void GameStartOnClick()
    {
        //다음 씬으로 고른 Hero 정보 넘기기
        ChangeScene.instance.SetHeros();

        //SceneManager.LoadScene("GameScene");

        string scene = "Chapter" + gameLevel;
        SceneManager.LoadScene("TestSceneA");
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);

    }
}
