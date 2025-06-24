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
    string[] gameLevelName = new string[] {"null��������" ,"���������̸�"};
    int gameLevel = 1;  
    int highestWave = 0;


    private void Awake()
    {
        
    }
    private void Start()
    {
        UpdateMainUI();
    }
    //���� ���� ���� ����ȭ(�κ�κ��� ������)
    private void UpdateAccountInfo()
    {
        gem = LobbyManager.instance.ReturnAccount(0);
        gold = LobbyManager.instance.ReturnAccount(1);
        energy = LobbyManager.instance.ReturnAccount(2);
    }
    private void UpdateMainUI()
    {
        //��� ����
        UpdateAccountInfo();
        gemText.text = gem.ToString();
        goldText.text = gold.ToString();
        energyText.text = energy.ToString();

        //���� ����
        gameLevelText.text = gameLevel.ToString()+". " +gameLevelName[gameLevel];
        highestWaveText.text = "�ִ� ���̺� <color=red>" + highestWave.ToString()+"/20</color>";

    }
    public void GameStartOnClick()
    {
        //���� ������ �� Hero ���� �ѱ��
        ChangeScene.instance.SetHeros();

        //SceneManager.LoadScene("GameScene");

        string scene = "Chapter" + gameLevel;
        SceneManager.LoadScene("TestSceneA");
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);

    }
}
