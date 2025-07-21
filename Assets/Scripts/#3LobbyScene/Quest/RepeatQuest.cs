using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RepeatQuest : MonoBehaviour
{
    [SerializeField] Image progressImage;
    [SerializeField] TextMeshProUGUI clearAmountText;
    [SerializeField] int targetAmount;
    [SerializeField] int clearAmount;
    [SerializeField] int questNum;
    Button clearBtn;

    private void Awake()
    {
        clearBtn = GetComponentsInChildren<Button>()[1];
    }
    public void UpdateUI()
    {
        clearAmount = BackEndGameData.Instance.UserQuestData.repeatQuest[questNum];

        clearAmountText.text = clearAmount.ToString() + "/" + targetAmount.ToString();
        progressImage.fillAmount = clearAmount / targetAmount;
        UpdateBtn();
    }
    private void UpdateBtn()
    {
        Color color;
        if (BackEndGameData.Instance.UserQuestData.repeatQuest[questNum] > targetAmount)
        {
            ColorUtility.TryParseHtmlString("#40FF0B", out color);
            clearBtn.image.color = color;
        }
        else
        {
            ColorUtility.TryParseHtmlString("#FFDB0B", out color);
            clearBtn.image.color = color;
        }
    }
    public void QuestClearBtnOnClick()
    {
        if (BackEndGameData.Instance.UserQuestData.repeatQuest[questNum] >= targetAmount)
        {
            //클리어 가능
            BackEndGameData.Instance.UserQuestData.repeatQuest[questNum] -= targetAmount;
            BackEndGameData.Instance.UserGameData.gem += 20;

            BackEndGameData.Instance.GameDataUpdate();

            UpdateUI();
            QuestManager.instance.UpdateQuestUI();
        }
    }
}
