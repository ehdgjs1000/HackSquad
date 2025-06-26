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
    int heroCount;
    [SerializeField] HeroDetail heroDetail;


    private void Start()
    {
        UpdateHeroCard();
    }
    public void UpdateHeroCard()
    {
        CheckUpgrade();

        HeroInfo hero = heroPrefab.GetComponent<HeroInfo>();
        hero.UpdateHeroInfo();
        heroImage.sprite = hero.ReturnHeroProfile();
        heroCount = hero.ReturnHeroCount();
        heroLevel = hero.ReturnHeroLevel();
        //���� ����ΰ� ������� ��� ó��
        if (heroLevel == 0 && heroCount == 0) noHeroBg.gameObject.SetActive(true);
        else noHeroBg.gameObject.SetActive(false);
        heroLevelText.text = heroLevel.ToString() + "����";

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
    }
    //���׷��̵尡 �������� üũ
    private void CheckUpgrade()
    {

    }
    //Hero������â Open
    public void HeroBtnOnClick()
    {
        heroDetail.gameObject.SetActive(true);
        heroDetail.SetHero(heroPrefab, canUpgrade);
        heroDetail.HeroDetailUpdate();
    }

}
