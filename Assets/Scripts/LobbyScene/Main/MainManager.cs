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
    string[] gameLevelName = new string[] {"null��������" ,"1���������̸�"};
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
        //��� ����
        gemText.text = BackEndGameData.Instance.UserGameData.gem.ToString();
        goldText.text = BackEndGameData.Instance.UserGameData.gold.ToString();
        energyText.text = BackEndGameData.Instance.UserGameData.energy.ToString();

        //���� ����
        gameLevelText.text = gameLevel.ToString()+". " +gameLevelName[gameLevel];
        highestWaveText.text = "�ִ� ���̺� <color=red>" + highestWave.ToString()+"/20</color>";

    }
    public void GameStartOnClick()
    {
        if (HeroSetManager.instance.squadCount > 0)
        {
            //���� ������ �� Hero ���� �ѱ��
            ChangeScene.instance.SetHeros();

            //SceneManager.LoadScene("GameScene");

            string scene = "Chapter" + gameLevel;
            SceneManager.LoadScene("InGameCommon");
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
        

    }
}
