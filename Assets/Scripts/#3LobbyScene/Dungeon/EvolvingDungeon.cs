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
        stageText.text = $"스테이지{dungeonNum + 1}";
        Color color;
        //도전할 던전
        if (dungeonNum == DungeonManager.instance.highestEvolvingDungeon)
        {
            playBtn.SetActive(true);
            firstClearImage.gameObject.SetActive(true);
            //버튼 색상 변경
            ColorUtility.TryParseHtmlString("#3EFF20", out color);
            startBtn.image.color = color;
            remainAmountText.text = $"플레이 가능 횟수 : {DungeonManager.instance.remainEvolvingDungeon}";
            dungeonStartText.text = "도전";
            canSweepImage.SetActive(false);
            cantPlayBG.gameObject.SetActive(false);
        }
        else if (dungeonNum < DungeonManager.instance.highestEvolvingDungeon)
        {
            //소탕할 던전 
            playBtn.SetActive(true);
            firstClearImage.gameObject.SetActive(false);
            //버튼 색상 변경
            ColorUtility.TryParseHtmlString("#FFEB20", out color);
            startBtn.image.color = color;
            remainAmountText.text = $"소탕 가능 횟수 : {DungeonManager.instance.remainEvolvingDungeon}";
            dungeonStartText.text = "소탕";
            if (DungeonManager.instance.remainEvolvingDungeon > 0) canSweepImage.SetActive(true);
            else canSweepImage.SetActive(false);
            cantPlayBG.gameObject.SetActive(false);
        }
        else
        {
            //아직 도전 할 수 없는 던전 
            playBtn.SetActive(false);
            canSweepImage.SetActive(false);
            firstClearImage.gameObject.SetActive(true);
            cantPlayBG.gameObject.SetActive(true);

        }
    }
    public void StartBtnClick()
    {
        string text = "Evolving" + (dungeonNum + 1);

        if (dungeonNum == DungeonManager.instance.highestEvolvingDungeon)//도전할 던전
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
        else if (dungeonNum < DungeonManager.instance.highestEvolvingDungeon)//소탕할 던전
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
        //다음 씬으로 고른 Hero 정보 넘기기
        ChangeScene.instance.SetHeros(0);
        BackEndGameData.Instance.UserQuestData.questProgress[1]++;
        BackEndGameData.Instance.GameDataUpdate();

        //StartCoroutine(sceneTransition.TransitionCoroutine());
        yield return new WaitForSeconds(2.0f);
    }
}
