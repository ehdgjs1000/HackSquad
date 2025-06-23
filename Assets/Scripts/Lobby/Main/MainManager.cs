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

    private void Awake()
    {
        
    }
    private void Start()
    {
        UpdateMainData();
    }
    //���� ���� ���� ����ȭ(�κ�κ��� ������)
    private void UpdateAccountInfo()
    {
        gem = LobbyManager.instance.ReturnAccount(0);
        gold = LobbyManager.instance.ReturnAccount(1);
        energy = LobbyManager.instance.ReturnAccount(2);
    }
    private void UpdateMainData()
    {
        UpdateAccountInfo();
        gemText.text = gem.ToString();
        goldText.text = gold.ToString();
        energyText.text = energy.ToString();
    }
    public void GameStartOnClick()
    {
        //���� ������ �� Hero ���� �ѱ��
        ChangeScene.instance.SetHeros();
        SceneManager.LoadScene("GameScene");
    }
}
