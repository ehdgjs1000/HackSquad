using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quest : MonoBehaviour
{
    [SerializeField] Image progressImage;
    [SerializeField] TextMeshProUGUI clearAmountText;
    [SerializeField] TextMeshProUGUI rewardAmountText;
    [SerializeField] float targetAmount;
    [SerializeField] float clearAmount;
    [SerializeField] float rewardAmount;
    [SerializeField] int questNum;
    Button clearBtn;
    public bool canRecieve = false;

    private void Awake()
    {
        clearBtn = GetComponentInChildren<Button>();
    }
    private void Start()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        clearAmount = BackEndGameData.Instance.UserQuestData.questProgress[questNum];

        clearAmountText.text = clearAmount.ToString() +"/"+targetAmount.ToString();
        progressImage.fillAmount = clearAmount / targetAmount;
        rewardAmountText.text = rewardAmount.ToString();
        QuestManager.instance.AlarmCheck();
        UpdateBtn();
    }
    public void UpdateBtn()
    {
        Color color;
        //클리어 했을경우
        if (BackEndGameData.Instance.UserQuestData.dailyCleared[questNum])
        {
            ColorUtility.TryParseHtmlString("#283FFF", out color);
            clearBtn.image.color = Color.gray;
            progressImage.color = color;
            canRecieve = false;
        }
        else if (!BackEndGameData.Instance.UserQuestData.dailyCleared[questNum] &&
            BackEndGameData.Instance.UserQuestData.questProgress[questNum] >= targetAmount)
        {
            ColorUtility.TryParseHtmlString("#40FF0B", out color);
            progressImage.color = Color.white;
            canRecieve = true;
            clearBtn.image.color = color;
        }
        else if (!BackEndGameData.Instance.UserQuestData.dailyCleared[questNum] &&
           BackEndGameData.Instance.UserQuestData.questProgress[questNum] < targetAmount)
        {
            ColorUtility.TryParseHtmlString("#FFDB0B", out color);
            progressImage.color = Color.white;
            canRecieve = false;
            clearBtn.image.color = color;
        }
        else
        {

        }
    }
    public void QuestClearBtnOnClick()
    {
        if (!BackEndGameData.Instance.UserQuestData.dailyCleared[questNum] &&
            BackEndGameData.Instance.UserQuestData.questProgress[questNum] >= targetAmount)
        {
            //클리어 가능
            BackEndGameData.Instance.UserQuestData.weeklyClearAmount += rewardAmount;
            BackEndGameData.Instance.UserQuestData.dailyClearAmount += rewardAmount;
            BackEndGameData.Instance.UserQuestData.dailyCleared[questNum] = true;
            BackEndGameData.Instance.GameDataUpdate();

            UpdateUI();
            QuestManager.instance.UpdateQuestUI();
        }
    }
}
