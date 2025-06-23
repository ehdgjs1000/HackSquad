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
    [SerializeField] PreviewManager previewManager;

    /// <summary>
    /// 히어로 정보를 UI에 동기화
    /// </summary>
    public void HeroDetailUpdate()
    {
        PlayerCtrl hero = heroGo.GetComponent<PlayerCtrl>();
        HeroInfo heroInfo = heroGo.GetComponent<HeroInfo>();
        heroNameText.text = hero.characterName;
        heroGradeText.text = heroInfo.ReturnHeroGrade().ToString()+"등급";
        heroLevelText.text = heroInfo.ReturnHeroLevel().ToString();
        float dps = (1 / hero.initFireRate) * hero.initDamage;
        heroDpsText.text = dps.ToString();
        heroPowerText.text = hero.initDamage.ToString();
        previewManager.UpdateHeroGO(hero.characterName);
        
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
        //TODO : 골드 있으면 레벨업

        HeroDetailUpdate();
    }
    public void ChooseHeroBtnClick()
    {
        if(HeroSetManager.instance.squadCount < 4)
        {
            //히어로 등록
            for (int a = 0; a < 4; a++)
            {
                //비어있는 칸에 넣기
                if (HeroSetManager.instance.ReturnHeroInfo(a).GetComponent<SquadHero>().hero == null)
                {
                    HeroSetManager.instance.ChooseHero(a,heroGo);
                    break;
                }
            }

            this.gameObject.SetActive(false);
        }
    }

}
