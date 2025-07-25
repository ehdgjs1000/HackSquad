using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillReDraw : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI redrawCountText;
    [SerializeField] Image videoImage;

    int redrawCount;

    private void Awake()
    {
        redrawCount = BackEndGameData.Instance.UserAbilityData.abilityLevel[10];
    }
    private void Start()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        if (redrawCount > 0)
        {
            videoImage.gameObject.SetActive(false);
            redrawCountText.gameObject.SetActive(true);
            redrawCountText.text = "x" + redrawCount.ToString();
        }
        else
        {
            videoImage.gameObject.SetActive(true);
            redrawCountText.gameObject.SetActive(false);
        }
    }
    public void ReDrawSkillOnClick()
    {
        if(redrawCount > 0)
        {
            redrawCount--;
            GameManager.instance.SkillChoose();
        }
        else
        {
            //광고 보고 스킬리롤
            AdsVideo.instance.RefreshSkillsOnClick();
        }
        UpdateUI();
    }


}
