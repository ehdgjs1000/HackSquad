using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillReDraw : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI redrawCountText;
    [SerializeField] Image videoImage;

    float redrawTerm = 3.0f;
    int redrawCount;

    private void Awake()
    {
        redrawCount = BackEndGameData.Instance.UserAbilityData.abilityLevel[10];
    }
    private void Start()
    {
        redrawTerm = 0.0f;
        UpdateUI();
    }
    private void Update()
    {
        if (redrawTerm > 0.0f) redrawTerm -= Time.deltaTime;
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
        if(redrawTerm <= 0.0f)
        {
            if (redrawCount > 0)
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


}
