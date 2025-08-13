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
    int dungeonNum => transform.GetSiblingIndex();
    [SerializeField] int goldAmount;


    private void Awake()
    {
        
    }
    private void Start()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        stageText.text = $"스테이지{dungeonNum+1}";
        goldAmountText.text = (1000 * (dungeonNum+1)).ToString();

        remainAmountText.text = $"소탕 가능 횟수:{DungeonManager.instance.remainGoldSweep}";
        
        //도전할 던전
        if(dungeonNum == DungeonManager.instance.highestGoldDungeon)
        {
            playBtn.SetActive(true);
            firstClearImage.gameObject.SetActive(true);
            dungeonStartText.text = "도전";
            canSweepImage.SetActive(false);
            cantPlayBG.gameObject.SetActive(false);
        }
        else if(dungeonNum < DungeonManager.instance.highestGoldDungeon)
        {
            //소탕할 던전 
            playBtn.SetActive(true);
            firstClearImage.gameObject.SetActive(false);
            dungeonStartText.text = "소탕";
            canSweepImage.SetActive(true);
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
        string text = "Gold" + (dungeonNum+1);
        SceneManager.LoadScene("GoldDungeonCommon");
        SceneManager.LoadScene(text, LoadSceneMode.Additive);
    }


}
