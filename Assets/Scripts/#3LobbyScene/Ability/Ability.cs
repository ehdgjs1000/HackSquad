using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ability : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI abilityNameText;
    [SerializeField] Image abilityImage;
    [SerializeField] TextMeshProUGUI abilityLevelText;
    [SerializeField] GameObject noAbilityBg;

    public int abilityId;
    public Sprite abilitySprite;
    public string abilityName;
    public int abilityGrade;
    public int abilityLevel;

    private void Start()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        if (abilityName.Contains("���ݷ�"))
        {
            abilityNameText.text = "<color=#F60000>" + "���ݷ�" + "</color>" + " ����";
        }
        else if (abilityName.Contains("����"))
        {
            abilityNameText.text = "<color=#0040F5>" + "����" + "</color>" + " ����";
        }
        else if (abilityName.Contains("ũ��Ƽ��"))
        {
            abilityNameText.text = "<color=#16E300>" + "ũ��Ƽ��" + "</color>" + " Ȯ������";
        }
        else if (abilityName.Contains("������"))
        {
            abilityNameText.text = "�ִ�" + "<color=#FFE100>" + "������" + "</color>" + " ����";
        }
        else abilityNameText.text = abilityName;

        abilityLevel = BackEndGameData.Instance.UserAbilityData.abilityLevel[abilityId];
        abilityLevelText.text = "Lv."+ abilityLevel.ToString();
        abilityImage.sprite = abilitySprite;
        if(abilityLevel == 0) noAbilityBg.SetActive(true);
        else noAbilityBg.SetActive(false);

        Image bgImage = GetComponent<Image>();
        Color color;
        if(abilityGrade == 0)
        {
            ColorUtility.TryParseHtmlString("#4CFF4F", out color);
            bgImage.color = color;
        }else if (abilityGrade == 1)
        {
            ColorUtility.TryParseHtmlString("#6387F6", out color);
            bgImage.color = color;
        }
        else if (abilityGrade == 2)
        {
            ColorUtility.TryParseHtmlString("#FFE031", out color);
            bgImage.color = color;
        }
    }
    public void AbilityOnClick()
    {
        AbilityManager.instance.OpenAbilityDetail(this);
    }
}
