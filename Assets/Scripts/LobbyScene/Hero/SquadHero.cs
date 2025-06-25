using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SquadHero : MonoBehaviour
{
    public GameObject hero;
    [SerializeField] Image heroImage;
    [SerializeField] TextMeshProUGUI heroLevelText;
    [SerializeField] Image bgImage;
    
    public void SquadHeroBtnClick()
    {
        if(hero != null)
        {
            hero = null;
            HeroSetManager.instance.squadCount--;
        }
        UpdateHeroInfo();
    }
    public void UpdateHeroInfo()
    {
        if(hero != null)
        {
            heroImage.sprite = hero.GetComponent<HeroInfo>().ReturnHeroProfile();
            heroLevelText.text = hero.GetComponent<HeroInfo>().ReturnHeroLevel().ToString() + "����";

            Color color;
            if (hero.GetComponent<HeroInfo>().ReturnHeroGrade() == 0)
            {
                ColorUtility.TryParseHtmlString("#6BFF28", out color);
                bgImage.color = color;
            }
            else if (hero.GetComponent<HeroInfo>().ReturnHeroGrade() == 1)
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
        else
        {   //��ϵ� ����ΰ� �������
            heroImage.sprite = null;
            heroLevelText.text = "";
            bgImage.color = Color.white;
        }
        

        
    }

}
