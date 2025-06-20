using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HeroCard : MonoBehaviour
{
    [SerializeField] GameObject heroPrefab;

    //UI
    [SerializeField] Image heroImage;
    [SerializeField] TextMeshProUGUI heroLevelText;
    [SerializeField] GameObject noHeroBg;
    [SerializeField] Image heroBg;
    [SerializeField] GameObject upgradeIcon;
    bool canUpgrade;
    int heroLevel;
    [SerializeField] HeroDetail heroDetail;


    private void Start()
    {
        UpdateHeroCard();
    }
    private void UpdateHeroCard()
    {
        CheckUpgrade();
        heroImage.sprite = heroPrefab.GetComponent<HeroInfo>().ReturnHeroProfile();
        heroLevel = heroPrefab.GetComponent<HeroInfo>().ReturnHeroLevel();
        heroLevelText.text = heroLevel.ToString() + "레벨";
        if(canUpgrade) upgradeIcon.gameObject.SetActive(true);
        else upgradeIcon.gameObject.SetActive(false);
        if (heroLevel > 0) noHeroBg.SetActive(false);
        int heroGrade = heroLevel = heroPrefab.GetComponent<HeroInfo>().ReturnHeroGrade();
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
    }
    //업그레이드가 가능한지 체크
    private void CheckUpgrade()
    {

    }
    //Hero상세정보창 Open
    public void HeroBtnOnClick()
    {
        heroDetail.gameObject.SetActive(true);
        heroDetail.SetHero(heroPrefab);
        heroDetail.HeroDetailUpdate();
    }

}
