using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HeroDetail : MonoBehaviour
{
    public GameObject heroGo;
    HeroInfo heroInfo;
    int heroNum;
    [SerializeField] TextMeshProUGUI heroNameText;
    [SerializeField] TextMeshProUGUI heroGradeText;
    [SerializeField] TextMeshProUGUI heroLevelText;
    [SerializeField] TextMeshProUGUI heroDpsText;
    [SerializeField] TextMeshProUGUI heroPowerText;
    [SerializeField] TextMeshProUGUI heroHpText;
    [SerializeField] TextMeshProUGUI needGoldText;
    [SerializeField] TextMeshProUGUI nowGoldText;
    [SerializeField] TextMeshProUGUI needHeroAmountText;
    [SerializeField] TextMeshProUGUI nowHeroAmountText;
    [SerializeField] PreviewManager previewManager;
    [SerializeField] Image bgImage;
    [SerializeField] Button upgradeBtn;
    bool canUpgrade;
    int heroLevel;
    int[] needGold = new int[] {1000,2000,4000,8000,16000,32000,64000,128000,256000};
    int[] needHeroConut = new int[] { 1,2,4,8,16,32,64,128,256};
    
    /// <summary>
    /// 히어로 정보를 UI에 동기화
    /// </summary>
    public void HeroDetailUpdate()
    {
        PlayerCtrl hero = heroGo.GetComponent<PlayerCtrl>();
        heroInfo = heroGo.GetComponent<HeroInfo>();
        heroNum = heroInfo.ReturnHeroNum();
        heroNameText.text = hero.characterName;
        Color color;
        if (heroInfo.ReturnHeroGrade() == 0)
        {
            heroGradeText.text = "normal";
            ColorUtility.TryParseHtmlString("#6BFF28", out color);
            heroGradeText.color = color;
            bgImage.color = color;
        }
        else if (heroInfo.ReturnHeroGrade() == 1)
        {
            heroGradeText.text = "special";
            ColorUtility.TryParseHtmlString("#3B91FF", out color);
            heroGradeText.color = color;
            bgImage.color = color;
        }
        else if (heroInfo.ReturnHeroGrade() == 2)
        {
            heroGradeText.text = "epic";
            ColorUtility.TryParseHtmlString("#FF37FB", out color);
            heroGradeText.color = color;
            bgImage.color = color;
        }
        else if (heroInfo.ReturnHeroGrade() == 3)
        {
            heroGradeText.text = "legendary";
            ColorUtility.TryParseHtmlString("#EEFF37", out color);
            heroGradeText.color = color;
            bgImage.color = color;
        }
        heroLevel = heroInfo.ReturnHeroLevel();

        //UI Update
        heroLevelText.text = heroLevel.ToString() + "레벨";
        needGoldText.text = needGold[heroLevel].ToString();
        needHeroAmountText.text = needHeroConut[heroLevel].ToString();
        nowHeroAmountText.text = BackEndGameData.Instance.UserHeroData.heroCount[heroNum].ToString();
        nowGoldText.text = BackEndGameData.Instance.UserGameData.gold.ToString();
        float heroDamge = hero.initDamage * Mathf.Pow(1.5f, 
            BackEndGameData.Instance.UserHeroData.heroLevel[heroNum]);
        if (canUpgrade)
        {
            ColorUtility.TryParseHtmlString("#FFE500", out color);
            upgradeBtn.GetComponent<Image>().color = color;

            ColorUtility.TryParseHtmlString("#6BFF28", out color);
            nowHeroAmountText.color = color;
            nowGoldText.color = color;
        }
        if (BackEndGameData.Instance.UserGameData.gold < needGold[heroLevel])
        {
            ColorUtility.TryParseHtmlString("#545454", out color);
            upgradeBtn.GetComponent<Image>().color = color;
            nowGoldText.color = Color.red;
        }
        else if (BackEndGameData.Instance.UserHeroData.heroCount[heroNum] < needHeroConut[heroLevel])
        {
            ColorUtility.TryParseHtmlString("#545454", out color);
            upgradeBtn.GetComponent<Image>().color = color;
            nowHeroAmountText.color = Color.red;
        }

        float dps = (1 / hero.initFireRate) * heroDamge;
        heroDpsText.text = dps.ToString("F1");
        heroPowerText.text = heroDamge.ToString("F1");
        previewManager.UpdateHeroGO(hero.characterName);
        heroHpText.text = (hero.ReturnInitHp() + (5 * BackEndGameData.Instance.UserHeroData.heroLevel[heroNum])).ToString();

        // TODO 
        //needGold.text = 
    }
    public void SetHero(GameObject _hero, bool _canUpgrade)
    {
        canUpgrade = _canUpgrade;
        heroGo = _hero;
    }
    public void HeroLevelUpOnClick()
    {
        //TODO : 골드 있으면 레벨업
        if(BackEndGameData.Instance.UserGameData.gold >= needGold[heroLevel] &&
            BackEndGameData.Instance.UserHeroData.heroCount[heroNum] >= needHeroConut[heroLevel])
        {
            //Upgrade Hero
            UpgradeHero();
        }else if (BackEndGameData.Instance.UserGameData.gold <= needGold[heroLevel])
        {
            PopUpMessageBase.instance.SetMessage("골드가 충분하지 않습니다");
        }else if (BackEndGameData.Instance.UserHeroData.heroCount[heroNum] >= needHeroConut[heroLevel])
        {
            PopUpMessageBase.instance.SetMessage("영웅 갯수가 충분하지 않습니다");
        }

        HeroDetailUpdate();
    }
    private void UpgradeHero()
    {
        //TODO : 서버(레벨업 / 갯수 차감 / 골드 차감)
        BackEndGameData.Instance.UserGameData.gold -= needGold[heroLevel];
        //HeroInfo heroInfo = heroGo.GetComponent<HeroInfo>();
        //int heroNum = heroInfo.ReturnHeroNum();
        BackEndGameData.Instance.UserHeroData.heroLevel[heroNum]++;
        BackEndGameData.Instance.UserHeroData.heroCount[heroNum] -= needHeroConut[heroLevel];

        heroInfo.UpdateHeroInfo();
        HeroSetManager.instance.UpdateInfo();
        BackEndGameData.Instance.GameDataUpdate();
    }
    public void ChooseHeroBtnClick()
    {
        if(HeroSetManager.instance.squadCount < 4 && heroLevel > 0)
        {
            // 중복된 히어로 확인
            for (int a = 0; a < 4; a++)
            {
                if (HeroSetManager.instance.ReturnHeroInfo(a).GetComponent<SquadHero>().hero != null &&
                    HeroSetManager.instance.ReturnHeroInfo(a).GetComponent<SquadHero>().hero.GetComponent<HeroInfo>().ReturnHeroNum() == 
                    heroGo.GetComponent<HeroInfo>().ReturnHeroNum())
                {
                    PopUpMessageBase.instance.SetMessage("이미 등록된 핵쟁이입니다");
                    return;
                }
            }
            //히어로 등록
            for (int a = 0; a < 4; a++)
            {
                //비어있는 칸에 넣기
                if (HeroSetManager.instance.ReturnHeroInfo(a).GetComponent<SquadHero>().hero == null)
                {
                    BackEndGameData.Instance.UserHeroData.heroChooseNum[a] = heroNum;
                    HeroSetManager.instance.ChooseHero(a,heroGo);
                    break;
                }
            }
            BackEndGameData.Instance.GameDataUpdate();
            this.gameObject.SetActive(false);
        }
        else if(HeroSetManager.instance.squadCount >= 4)
        {
            PopUpMessageBase.instance.SetMessage("스쿼드가 가득 찼습니다");
        }
        else if (heroLevel <= 0)
        {
            PopUpMessageBase.instance.SetMessage("핵쟁이가 0레벨 입니다");
        }
    }

}
