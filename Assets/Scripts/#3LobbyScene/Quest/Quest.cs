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
        UpdateBtn();
    }
    private void UpdateBtn()
    {
        Color color;
        if (BackEndGameData.Instance.UserQuestData.dailyCleared[questNum])
        {
            clearBtn.image.color = Color.gray;
        }else if (!BackEndGameData.Instance.UserQuestData.dailyCleared[questNum] &&
            BackEndGameData.Instance.UserQuestData.questProgress[questNum] >= targetAmount)
        {
            ColorUtility.TryParseHtmlString("#40FF0B", out color);
            clearBtn.image.color = color;
        }
        else if (!BackEndGameData.Instance.UserQuestData.dailyCleared[questNum] &&
           BackEndGameData.Instance.UserQuestData.questProgress[questNum] < targetAmount)
        {
            ColorUtility.TryParseHtmlString("#FFDB0B", out color);
            clearBtn.image.color = color;
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
