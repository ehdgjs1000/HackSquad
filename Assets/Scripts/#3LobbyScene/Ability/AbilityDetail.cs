using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityDetail : MonoBehaviour
{
    public Ability ability;

    [SerializeField] Image abilityBG;
    [SerializeField] TextMeshProUGUI abilityNameText;
    [SerializeField] Image abilityImage;
    [SerializeField] TextMeshProUGUI abilityLevelText;
    [SerializeField] TextMeshProUGUI abilityDescText;
    int abilityGrade;
    float canExitTime = 1.0f;
    private void Update()
    {
        canExitTime-= Time.deltaTime;
    }
    public void ExitOnClick()
    {
        if (canExitTime <= 0.0f)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void UpdateUI(Ability _ability)
    {
        canExitTime = 1.0f;
        ability = _ability;

        abilityNameText.text = ability.abilityName;
        abilityLevelText.text = "Lv." + ability.abilityLevel.ToString();
        abilityImage.sprite = ability.abilitySprite;
        abilityGrade = ability.abilityGrade;
        float nextAbilityLevel = ability.abilityLevel;
        if (ability.abilityId == 0) abilityDescText.text = "���ݷ� " + 5 * nextAbilityLevel + "% ����";
        else if (ability.abilityId == 1) abilityDescText.text = "��Ƽü�� " + 2 * nextAbilityLevel + " ����";
        else if (ability.abilityId == 2) abilityDescText.text = "������ " + 1 * nextAbilityLevel + " ����";
        else if (ability.abilityId == 3) abilityDescText.text = "���ݷ� " + 10 * nextAbilityLevel + "% ����";
        else if (ability.abilityId == 4) abilityDescText.text = "��Ƽü�� " + 5 * nextAbilityLevel + " ����";
        else if (ability.abilityId == 5) abilityDescText.text = "�޴� ���� " + 1 * nextAbilityLevel + " ����";
        else if (ability.abilityId == 6) abilityDescText.text = "���ݷ� " + 20 * nextAbilityLevel + "% ����";
        else if (ability.abilityId == 7) abilityDescText.text = "��Ƽü�� " + 10 * nextAbilityLevel + " ����";
        else if (ability.abilityId == 8) abilityDescText.text = "�޴����� " + 3 * nextAbilityLevel + " ����";
        else if (ability.abilityId == 9) abilityDescText.text = "�ִ� ������ " + 1 * nextAbilityLevel + " ����";
        else if (ability.abilityId == 10) abilityDescText.text = "���� ��ų ���� Ƚ�� " + 1 * nextAbilityLevel + "�� ����";
        else if (ability.abilityId == 11) abilityDescText.text = "��� �� ü�� " + 5 * nextAbilityLevel + "%�� ��Ȱ";


        Color color;
        if (abilityGrade == 0)
        {
            ColorUtility.TryParseHtmlString("#4CFF4F", out color);
            abilityBG.color = color;
        }
        else if (abilityGrade == 1)
        {
            ColorUtility.TryParseHtmlString("#6387F6", out color);
            abilityBG.color = color;
        }
        else if (abilityGrade == 2)
        {
            ColorUtility.TryParseHtmlString("#FFE031", out color);
            abilityBG.color = color;
        }
    }

}

