using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class EvolvingDungeon : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI remainAmountText;
    [SerializeField] Image firstClearImage;
    [SerializeField] Image cantPlayBG;

    [SerializeField] GameObject playBtn;
    [SerializeField] GameObject canSweepImage;
    [SerializeField] TextMeshProUGUI dungeonStartText;
    [SerializeField] TextMeshProUGUI stageText;
    [SerializeField] EvolvingSweepPanel sweepPanel;

    [SerializeField] int[] rewardProb;
    int dungeonNum => transform.GetSiblingIndex();
    Button startBtn => playBtn.GetComponent<Button>();


    private void Update()
    {
        UpdateUI();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetInt("remainEvolvingDungeonCount", 2);
        }
    }
    public void UpdateUI()
    {
        stageText.text = $"��������{dungeonNum + 1}";
        Color color;
        //������ ����
        if (dungeonNum == DungeonManager.instance.highestEvolvingDungeon)
        {
            playBtn.SetActive(true);
            firstClearImage.gameObject.SetActive(true);
            //��ư ���� ����
            ColorUtility.TryParseHtmlString("#3EFF20", out color);
            startBtn.image.color = color;
            remainAmountText.text = $"�÷��� ���� Ƚ�� : {DungeonManager.instance.remainEvolvingDungeon}";
            dungeonStartText.text = "����";
            canSweepImage.SetActive(false);
            cantPlayBG.gameObject.SetActive(false);
        }
        else if (dungeonNum < DungeonManager.instance.highestEvolvingDungeon)
        {
            //������ ���� 
            playBtn.SetActive(true);
            firstClearImage.gameObject.SetActive(false);
            //��ư ���� ����
            ColorUtility.TryParseHtmlString("#FFEB20", out color);
            startBtn.image.color = color;
            remainAmountText.text = $"���� ���� Ƚ�� : {DungeonManager.instance.remainEvolvingDungeon}";
            dungeonStartText.text = "����";
            if (DungeonManager.instance.remainEvolvingDungeon > 0) canSweepImage.SetActive(true);
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
        string text = "Evolving" + (dungeonNum + 1);

        if (dungeonNum == DungeonManager.instance.highestEvolvingDungeon)//������ ����
        {
            if (PlayerPrefs.HasKey("remainEvolvingDungeonCount") && PlayerPrefs.GetInt("remainGoremainEvolvingDungeonCountldDungeonCount") > 0)
            {
                StartCoroutine(StartGame());
                int remainEvolvingDungeonCount = PlayerPrefs.GetInt("remainEvolvingDungeonCount");
                remainEvolvingDungeonCount--;
                PlayerPrefs.SetInt("remainEvolvingDungeonCount", remainEvolvingDungeonCount);
                SceneManager.LoadScene("EvolvingDungeonCommon");
                SceneManager.LoadScene(text, LoadSceneMode.Additive);
            }
            else if (!PlayerPrefs.HasKey("remainEvolvingDungeonCount"))
            {
                StartCoroutine(StartGame());
                PlayerPrefs.SetInt("remainEvolvingDungeonCount", 1);
                SceneManager.LoadScene("EvolvingDungeonCommon");
                SceneManager.LoadScene(text, LoadSceneMode.Additive);
            }
            else if (PlayerPrefs.HasKey("remainEvolvingDungeonCount") &&
                PlayerPrefs.GetInt("remainEvolvingDungeonCount") <= 0) return;
        }
        else if (dungeonNum < DungeonManager.instance.highestEvolvingDungeon)//������ ����
        {
            if (PlayerPrefs.HasKey("remainEvolvingDungeonCount") && PlayerPrefs.GetInt("remainEvolvingDungeonCount") > 0)
            {
                int remainEvolvingDungeonCount = PlayerPrefs.GetInt("remainEvolvingDungeonCount");
                remainEvolvingDungeonCount--;
                PlayerPrefs.SetInt("remainEvolvingDungeonCount", remainEvolvingDungeonCount);
                sweepPanel.gameObject.SetActive(true);
                //sweepPanel.GetComponent<EvolvingSweepPanel>().SetUp(EvolvingAmount);
                int rewardNum = RewardChoose();
                sweepPanel.SetUp(rewardNum);
                BackEndGameData.Instance.GameDataUpdate();
            }
            else if (PlayerPrefs.HasKey("remainEvolvingDungeonCount") &&
                PlayerPrefs.GetInt("remainEvolvingDungeonCount") <= 0) return;
        }
    }
    private int RewardChoose()
    {
        int a = Random.Range(0,100);
        if(a < rewardProb[0])
        {
            BackEndGameData.Instance.UserInvenData.juiceLevel1ItemCount++;
            return 0;
        }
        else if (a >= rewardProb[0] && a < rewardProb[1])
        {
            BackEndGameData.Instance.UserInvenData.juiceLevel2ItemCount++;
            return 1;
        }
        else if (a >= rewardProb[1] && a < rewardProb[2])
        {
            BackEndGameData.Instance.UserInvenData.juiceLevel3ItemCount++;
            return 2;
        }
        else
        {
            BackEndGameData.Instance.UserInvenData.juiceLevel4ItemCount++;
            return 3;
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
