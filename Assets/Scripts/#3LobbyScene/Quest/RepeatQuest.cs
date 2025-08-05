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
    public bool canRecieve = false;

    private void Awake()
    {
        clearBtn = GetComponentsInChildren<Button>()[1];
    }
    public void UpdateUI()
    {
        clearAmount = BackEndGameData.Instance.UserQuestData.repeatQuest[questNum];

        clearAmountText.text = clearAmount.ToString() + "/" + targetAmount.ToString();
        progressImage.fillAmount = (float)clearAmount / (float)targetAmount;
        QuestManager.instance.AlarmCheck();
        UpdateBtn();
    }
    public void UpdateBtn()
    {
        Color color;
        if (BackEndGameData.Instance.UserQuestData.repeatQuest[questNum] >= targetAmount)
        {
            ColorUtility.TryParseHtmlString("#40FF0B", out color);
            canRecieve = true;
            clearBtn.image.color = color;
        }
        else
        {
            ColorUtility.TryParseHtmlString("#FFDB0B", out color);
            canRecieve = false;
            clearBtn.image.color = color;
        }
    }
    public void QuestClearBtnOnClick()
    {
        if (BackEndGameData.Instance.UserQuestData.repeatQuest[questNum] >= targetAmount)
        {
            //클리어 가능
            SoundManager.instance.BtnClickPlay();
            BackEndGameData.Instance.UserQuestData.repeatQuest[questNum] -= targetAmount;
            BackEndGameData.Instance.UserGameData.gem += 20;

            BackEndGameData.Instance.GameDataUpdate();

            UpdateUI();
            QuestManager.instance.UpdateQuestUI();
        }
    }
}
