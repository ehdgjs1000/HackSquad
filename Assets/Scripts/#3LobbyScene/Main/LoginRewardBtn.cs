using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginRewardBtn : MonoBehaviour
{
    [SerializeField] GameObject clearBG;
    [SerializeField] Image rewardItemImage;
    [SerializeField] Sprite rewardItemSprite;
    [SerializeField] TextMeshProUGUI rewardAmountText;
    [SerializeField] int rewardAmount;
    [SerializeField] string rewardType;
    [SerializeField] int num;
    [SerializeField] TextMeshProUGUI dayText;
    [SerializeField] Image bgImage;
    [SerializeField] Image topImage;

    private void Start()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        //Reward Clear BG Upadte
        string text = "loginReward" + num;
        int isClear; //0 = nonClear, else = Clear
        if (PlayerPrefs.HasKey(text))
        {
            isClear = PlayerPrefs.GetInt(text);
        }
        else isClear = 0;

        if (isClear == 1) clearBG.SetActive(true);
        else clearBG.SetActive(false);

        if (BackEndGameData.Instance.UserGameData.loginCount >= num && PlayerPrefs.GetInt(text) == 0)
        {
            dayText.text = "수령";
            bgImage.color = Color.green;
            topImage.color = Color.green;
        }
        else
        {
            dayText.text = num + "일차";
            Color color1;
            Color color2;
            ColorUtility.TryParseHtmlString("#69C7FF", out color1);
            ColorUtility.TryParseHtmlString("#DAF5FF", out color2);
            bgImage.color = color1;
            topImage.color = color2;
        }
        rewardItemImage.sprite = rewardItemSprite;
        rewardAmountText.text = rewardAmount.ToString();
    }
    public void RewardBtnClick()
    {
        string text = "loginReward" + num;
        Debug.Log($"{text} Click : " + num);
        if(PlayerPrefs.GetInt(text) == 0 && num <= BackEndGameData.Instance.UserGameData.loginCount) //수령 가능
        {
            if(rewardType == "골드")
            {
                BackEndGameData.Instance.UserGameData.gold += rewardAmount;
            }else if (rewardType == "다이아")
            {
                BackEndGameData.Instance.UserGameData.gem += rewardAmount;
            }
            else
            {
                BackEndGameData.Instance.UserHeroData.heroCount[10]++;
            }
            PlayerPrefs.SetInt(text, 1);
            BackEndGameData.Instance.GameDataUpdate();
            UpdateUI();
        }

    }

}
