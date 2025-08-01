using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SquadHero : MonoBehaviour
{
    public GameObject hero;
    [SerializeField] Image heroImage;
    [SerializeField] TextMeshProUGUI heroLevelText;
    [SerializeField] Image heroTypeBg;
    [SerializeField] Image heroTypeImage;
    [SerializeField] TextMeshProUGUI heroNameText;
    [SerializeField] Sprite[] heroTypeSprite;
    [SerializeField] Image bgImage;
    [SerializeField] GameObject upgradeIcon;
    [SerializeField] int squadNum;
    bool canUpgrade = false;
    string heroType;
    public void SquadHeroBtnClick()
    {
        if(hero != null)
        {
            hero = null;
            HeroSetManager.instance.squadCount--;
            BackEndGameData.Instance.UserHeroData.heroChooseNum[squadNum] = 99;
            BackEndGameData.Instance.GameDataUpdate();
            this.gameObject.SetActive(false);
        }
        UpdateHeroInfo();
    }
    public void UpdateHeroInfo()
    {
        if(hero != null)
        {
            heroImage.sprite = hero.GetComponent<HeroInfo>().ReturnHeroProfile();
            heroLevelText.text = hero.GetComponent<HeroInfo>().ReturnHeroLevel().ToString() + "레벨";
            HeroInfo heroInfo = hero.GetComponent<HeroInfo>();
            heroType = heroInfo.ReturnHeroType();
            heroNameText.text = hero.GetComponent<PlayerCtrl>().characterName;
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
            if (heroType == "공격형")
            {
                ColorUtility.TryParseHtmlString("#FD0000", out color);
                heroTypeBg.sprite = heroTypeSprite[0];

            }
            else if (heroType == "마법형")
            {
                heroTypeBg.sprite = heroTypeSprite[1];
            }
            else if (heroType == "외계형")
            {
                heroTypeBg.sprite = heroTypeSprite[2];
            }
            else if (heroType == "치유형")
            {
                heroTypeBg.sprite = heroTypeSprite[3];
            }
            heroTypeImage.sprite = heroInfo.ReturnHeroTypeImage();
        }
        else
        {   //등록된 히어로가 없을경우
            heroImage.sprite = null;
            heroLevelText.text = "";
            bgImage.color = Color.white;
        }

        if (canUpgrade)
        {
            upgradeIcon.SetActive(true);
        }
        else
        {
            upgradeIcon.SetActive(false);
        }

        
    }

}
