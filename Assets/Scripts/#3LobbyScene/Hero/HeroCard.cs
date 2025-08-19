using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HeroCard : MonoBehaviour
{
    [SerializeField] GameObject heroPrefab;
    HeroInfo hero;

    //UI
    [SerializeField] Image heroImage;
    [SerializeField] TextMeshProUGUI heroLevelText;
    [SerializeField] GameObject noHeroBg;
    [SerializeField] Image heroBg;
    [SerializeField] GameObject upgradeIcon;
    bool canUpgrade;
    int heroLevel;
    int heroCount;
    [SerializeField] HeroDetail heroDetail;
    [SerializeField] Image heroTypeBg;
    [SerializeField] Image heroTypeImage;
    [SerializeField] TextMeshProUGUI heroNameText;
    [SerializeField] Sprite[] heroTypeSprite;
    [SerializeField] GameObject[] starSets;
    string heroType;


    private void Start()
    {
       
        UpdateHeroCard();
    }
    public void UpdateHeroCard()
    {
        CheckUpgrade();

        hero = heroPrefab.GetComponent<HeroInfo>();
        hero.UpdateHeroInfo();
        heroImage.sprite = hero.ReturnHeroProfile();
        heroCount = hero.ReturnHeroCount();
        heroLevel = hero.ReturnHeroLevel();
        heroType = hero.ReturnHeroType();
        //보유 히어로가 없을경우 배경 처리
        if (heroLevel == 0 && heroCount == 0) noHeroBg.gameObject.SetActive(true);
        else noHeroBg.gameObject.SetActive(false);
        heroLevelText.text = heroLevel.ToString() + "레벨";
        heroNameText.text = heroPrefab.GetComponent<PlayerCtrl>().characterName;

        if (heroCount >= Mathf.Pow(2, heroLevel)) canUpgrade = true;
        else canUpgrade = false;

        if(canUpgrade)
        {
            upgradeIcon.gameObject.SetActive(true);
            StartCoroutine(upgradeIcon.GetComponent<IconAnim>().PlayIconAnim());
            
        }
        else upgradeIcon.gameObject.SetActive(false);

        if (heroLevel > 0) noHeroBg.SetActive(false);
        int heroGrade = hero.ReturnHeroGrade();
        Color color;
        if(heroGrade == 0)
        {
            ColorUtility.TryParseHtmlString("#6BFF28", out color);
            heroBg.color = color;
        }else if (heroGrade == 1)
        {
            ColorUtility.TryParseHtmlString("#3B91FF", out color);
            heroBg.color = color;
        }
        else if (heroGrade == 2)
        {
            ColorUtility.TryParseHtmlString("#FF37FB", out color);
            heroBg.color = color;
        }
        else if (heroGrade == 3)
        {
            ColorUtility.TryParseHtmlString("#EEFF37", out color);
            heroBg.color = color;
        }

        //영웅 직업
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
        heroTypeImage.sprite = hero.ReturnHeroTypeImage();

        //히어로 진화 star UI Update
        int heroEvolvingLevel = BackEndGameData.Instance.UserEvolvingData.evolvingLevel[hero.ReturnHeroNum()];
        if(heroEvolvingLevel == 0)
        {
            starSets[0].SetActive(false);
            starSets[1].SetActive(false);
            starSets[2].SetActive(false);
        }else if (heroEvolvingLevel == 1)
        {
            starSets[0].SetActive(true);
            starSets[1].SetActive(false);
            starSets[2].SetActive(false);
        }
        else if (heroEvolvingLevel == 2)
        {
            starSets[0].SetActive(false);
            starSets[1].SetActive(true);
            starSets[2].SetActive(true);
        }
        else if (heroEvolvingLevel == 3)
        {
            starSets[0].SetActive(true);
            starSets[1].SetActive(true);
            starSets[2].SetActive(true);
        }


    }
    //업그레이드가 가능한지 체크
    private void CheckUpgrade()
    {

    }
    //Hero상세정보창 Open
    public void HeroBtnOnClick()
    {
        heroDetail.gameObject.SetActive(true);
        heroDetail.SetHero(heroPrefab, canUpgrade);
        heroDetail.HeroDetailUpdate();
        heroDetail.InitHeroDetail();
    }

}
