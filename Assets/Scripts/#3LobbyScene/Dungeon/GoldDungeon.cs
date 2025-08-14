using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GoldDungeon : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldAmountText;
    [SerializeField] TextMeshProUGUI remainAmountText;
    [SerializeField] Image firstClearImage;
    [SerializeField] Image cantPlayBG;

    [SerializeField] GameObject playBtn;
    [SerializeField] GameObject canSweepImage;
    [SerializeField] TextMeshProUGUI dungeonStartText;
    [SerializeField] TextMeshProUGUI stageText;
    [SerializeField] GameObject sweepPanel;
    int dungeonNum => transform.GetSiblingIndex();
    [SerializeField] int goldAmount;
    Button startBtn => playBtn.GetComponent<Button>();


    private void Update()
    {
        UpdateUI();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetInt("remainGoldDungeonCount", 2);
        }
    }
    public void UpdateUI()
    {
        stageText.text = $"��������{dungeonNum+1}";
        goldAmount = 1000 * (dungeonNum + 1);
        goldAmountText.text = goldAmount.ToString();
        Color color;
        //������ ����
        if (dungeonNum == DungeonManager.instance.highestGoldDungeon)
        {
            playBtn.SetActive(true);
            firstClearImage.gameObject.SetActive(true);
            //��ư ���� ����
            ColorUtility.TryParseHtmlString("#3EFF20", out color);
            startBtn.image.color = color;
            remainAmountText.text = $"�÷��� ���� Ƚ�� : {DungeonManager.instance.remainGoldDungeon}";
            dungeonStartText.text = "����";
            canSweepImage.SetActive(false);
            cantPlayBG.gameObject.SetActive(false);
        }
        else if(dungeonNum < DungeonManager.instance.highestGoldDungeon)
        {
            //������ ���� 
            playBtn.SetActive(true);
            firstClearImage.gameObject.SetActive(false);
            //��ư ���� ����
            ColorUtility.TryParseHtmlString("#FFEB20", out color);
            startBtn.image.color = color;
            remainAmountText.text = $"���� ���� Ƚ�� : {DungeonManager.instance.remainGoldDungeon}";
            dungeonStartText.text = "����";
            if (DungeonManager.instance.remainGoldDungeon > 0)canSweepImage.SetActive(true);
            else canSweepImage.SetActive(false);
            cantPlayBG.gameObject.SetActive(false);
        }
        else
        {
            //���� ���� �� �� ���� ���� 
            playBtn.SetActive(false);
            canSweepImage.SetActive(false);
            firstClearImage.gameObject.SetActive(true);
            cantPlayBG.gameObject.SetActive(true);

        }
    }
    public void StartBtnClick()
    {
        string text = "Gold" + (dungeonNum+1);
        
        if (dungeonNum == DungeonManager.instance.highestGoldDungeon)//������ ����
        {
            if (PlayerPrefs.HasKey("remainGoldDungeonCount") && PlayerPrefs.GetInt("remainGoldDungeonCount") > 0)
            {
                StartCoroutine(StartGame());
                int remainGoldDungeonCount = PlayerPrefs.GetInt("remainGoldDungeonCount");
                remainGoldDungeonCount--;
                PlayerPrefs.SetInt("remainGoldDungeonCount", remainGoldDungeonCount);
                SceneManager.LoadScene("GoldDungeonCommon");
                SceneManager.LoadScene(text, LoadSceneMode.Additive);
            }
            else if (!PlayerPrefs.HasKey("remainGoldDungeonCount"))
            {
                StartCoroutine(StartGame());
                PlayerPrefs.SetInt("remainGoldDungeonCount", 1);
                SceneManager.LoadScene("GoldDungeonCommon");
                SceneManager.LoadScene(text, LoadSceneMode.Additive);
            }
            else if (PlayerPrefs.HasKey("remainGoldDungeonCount") &&
                PlayerPrefs.GetInt("remainGoldDungeonCount") <= 0) return;
        }
        else if (dungeonNum < DungeonManager.instance.highestGoldDungeon)//������ ����
        {
            if (PlayerPrefs.HasKey("remainGoldDungeonCount") && PlayerPrefs.GetInt("remainGoldDungeonCount") > 0)
            {
                int remainGoldDungeonCount = PlayerPrefs.GetInt("remainGoldDungeonCount");
                remainGoldDungeonCount--;
                PlayerPrefs.SetInt("remainGoldDungeonCount", remainGoldDungeonCount);
                sweepPanel.SetActive(true);
                sweepPanel.GetComponent<GoldSweepPanel>().SetUp(goldAmount);
                BackEndGameData.Instance.UserGameData.gold += goldAmount;
                BackEndGameData.Instance.GameDataUpdate();
            }
            else if (PlayerPrefs.HasKey("remainGoldDungeonCount") &&
                PlayerPrefs.GetInt("remainGoldDungeonCount") <= 0) return;
        }
    }
    IEnumerator StartGame()
    {
        SoundManager.instance.BtnClickPlay();
        //���� ������ �� Hero ���� �ѱ��
        ChangeScene.instance.SetHeros(0);
        BackEndGameData.Instance.UserQuestData.questProgress[1]++;
        BackEndGameData.Instance.GameDataUpdate();

        //StartCoroutine(sceneTransition.TransitionCoroutine());
        yield return new WaitForSeconds(2.0f);
    }


}
