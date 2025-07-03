using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DrawHeroGo : MonoBehaviour
{
    [SerializeField] GameObject hero;

    [SerializeField] Image heroImage;
    [SerializeField] TextMeshProUGUI heroNameText;
    [SerializeField] Image bgImage;

    public void InfoUpdate()
    {
        Color color;
        heroImage.sprite = hero.GetComponent<HeroInfo>().ReturnHeroProfile();
        heroNameText.text = hero.GetComponent<PlayerCtrl>().characterName;
        if (hero.GetComponent<HeroInfo>().ReturnHeroGrade() == 0)
        {
            ColorUtility.TryParseHtmlString("#6BFF28", out color);
            bgImage.color = color;
        }else if (hero.GetComponent<HeroInfo>().ReturnHeroGrade() == 1)
        {
            ColorUtility.TryParseHtmlString("#3B91FF", out color);
            bgImage.color = color;
        }
        else if (hero.GetComponent<HeroInfo>().ReturnHeroGrade() == 2)
        {
            ColorUtility.TryParseHtmlString("#FF37FB", out color);
            bgImage.color = color;
        }
        else if (hero.GetComponent<HeroInfo>().ReturnHeroGrade() == 3)
        {
            ColorUtility.TryParseHtmlString("#EEFF37", out color);
            bgImage.color = color;
        }
    }
    public void SetHero(GameObject _hero)
    {
        hero = _hero;
    }

}
