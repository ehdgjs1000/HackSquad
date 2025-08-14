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
        if (ability.abilityId == 0) abilityDescText.text = "공격력 " + 5 * nextAbilityLevel + "% 증가";
        else if (ability.abilityId == 1) abilityDescText.text = "파티체력 " + 2 * nextAbilityLevel + " 증가";
        else if (ability.abilityId == 2) abilityDescText.text = "에너지 " + 1 * nextAbilityLevel + " 증가";
        else if (ability.abilityId == 3) abilityDescText.text = "공격력 " + 10 * nextAbilityLevel + "% 증가";
        else if (ability.abilityId == 4) abilityDescText.text = "파티체력 " + 5 * nextAbilityLevel + " 증가";
        else if (ability.abilityId == 5) abilityDescText.text = "받는 피해 " + 1 * nextAbilityLevel + " 감소";
        else if (ability.abilityId == 6) abilityDescText.text = "공격력 " + 20 * nextAbilityLevel + "% 증가";
        else if (ability.abilityId == 7) abilityDescText.text = "파티체력 " + 10 * nextAbilityLevel + " 증가";
        else if (ability.abilityId == 8) abilityDescText.text = "받는피해 " + 3 * nextAbilityLevel + " 감소";
        else if (ability.abilityId == 9) abilityDescText.text = "최대 에너지 " + 1 * nextAbilityLevel + " 증가";
        else if (ability.abilityId == 10) abilityDescText.text = "무료 스킬 리롤 횟수 " + 1 * nextAbilityLevel + "개 증가";
        else if (ability.abilityId == 11) abilityDescText.text = "사망 시 체력 " + 5 * nextAbilityLevel + "%로 부활";


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

