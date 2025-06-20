using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HeroDetail : MonoBehaviour
{
    GameObject heroGo;
    [SerializeField] TextMeshProUGUI heroNameText;
    [SerializeField] TextMeshProUGUI heroGradeText;
    [SerializeField] TextMeshProUGUI heroLevelText;
    [SerializeField] TextMeshProUGUI heroDpsText;
    [SerializeField] TextMeshProUGUI heroPowerText;
    [SerializeField] TextMeshProUGUI heroHpText;
    [SerializeField] TextMeshProUGUI needGold;

    /// <summary>
    /// ����� ������ UI�� ����ȭ
    /// </summary>
    public void HeroDetailUpdate()
    {
        PlayerCtrl hero = heroGo.GetComponent<PlayerCtrl>();
        HeroInfo heroInfo = heroGo.GetComponent<HeroInfo>();
        heroNameText.text = hero.characterName;
        heroGradeText.text = heroInfo.ReturnHeroGrade().ToString();
        heroLevelText.text = heroInfo.ReturnHeroLevel().ToString();
        float dps = (1 / hero.fireRate) * hero.damage;
        heroDpsText.text = dps.ToString();
        heroPowerText.text = hero.damage.ToString();
        
        // TODO 
        //heroHpText.text = hero.hp.ToString();
        //needGold.text = 
    }
    public void SetHero(GameObject _hero)
    {
        heroGo = _hero;
    }
    public void HeroLevelUpOnClick()
    {
        //TODO : ��� ������ ������

        HeroDetailUpdate();
    }

}
