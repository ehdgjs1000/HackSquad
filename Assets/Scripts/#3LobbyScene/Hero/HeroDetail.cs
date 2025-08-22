using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HeroDetail : MonoBehaviour
{
    public GameObject heroGo;
    HeroInfo heroInfo;
    EvolvingInfo evolvingInfo;
    int heroNum;
    [Header("Hero Info UI")]
    [SerializeField] TextMeshProUGUI heroNameText;
    [SerializeField] TextMeshProUGUI heroGradeText;
    [SerializeField] TextMeshProUGUI heroJobText;
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
    [SerializeField] TextMeshProUGUI skillName;
    [SerializeField] TextMeshProUGUI skillDesc;
    [SerializeField] Image skillImage;

    [SerializeField] GameObject levelUpGO;
    [SerializeField] GameObject evolvingGO;
    [SerializeField] Button levelUpBtn, evolvingBtn;
    
    int upgradeType;
    bool canUpgrade;
    int heroLevel;
    int[] needGold = new int[] { 1000, 2000, 4000, 8000, 16000, 32000, 64000, 128000, 256000,512000,1024000 };
    int[] needHeroConut = new int[] { 1, 2, 4, 8, 16, 32, 64, 128, 256,512,1024 };

    //Audio
    [SerializeField] AudioClip upgradeClip;

    /// <summary>
    /// 히어로 정보를 UI에 동기화
    /// </summary>
    public void HeroDetailUpdate()
    {
        PlayerCtrl hero = heroGo.GetComponent<PlayerCtrl>();
        heroInfo = heroGo.GetComponent<HeroInfo>();
        evolvingInfo = heroGo.GetComponent<EvolvingInfo>();
        heroNum = heroInfo.ReturnHeroNum();
        heroNameText.text = hero.characterName;
        heroJobText.text = heroInfo.heroJob;
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
        SkillUiUpdate();
        heroLevelText.text = heroLevel.ToString() + "레벨";

        if (needGold[heroLevel] >= 10000) needGoldText.text = $"{needGold[heroLevel] / 1000}K";
        else needGoldText.text = needGold[heroLevel].ToString();
        if (BackEndGameData.Instance.UserGameData.gold >= 10000) nowGoldText.text = $"{BackEndGameData.Instance.UserGameData.gold / 1000}K";
        else nowGoldText.text = BackEndGameData.Instance.UserGameData.gold.ToString();

        needHeroAmountText.text = needHeroConut[heroLevel].ToString();
        nowHeroAmountText.text = BackEndGameData.Instance.UserHeroData.heroCount[heroNum].ToString();

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
        if (BackEndGameData.Instance.UserEvolvingData.evolvingLevel[heroNum] > 0) dps *= 1.3f;
;
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
    public void InitHeroDetail()
    {
        levelUpGO.SetActive(true);
        evolvingGO.SetActive(false);
        upgradeType = 0;
        levelUpBtn.image.color = levelUpBtn.colors.selectedColor;
        levelUpBtn.gameObject.SetActive(true);
        //evolvingBtn.image.color = evolvingBtn.colors.normalColor;
    }
    private void SkillUiUpdate()
    {
        PlayerCtrl playerCtrl = heroGo.GetComponent<PlayerCtrl>();
        SkillData finalSkill = playerCtrl.finalSkill;

        skillImage.sprite = finalSkill.skillImage;
        skillName.text = finalSkill.skillName;
        skillDesc.text = finalSkill.skillDescription;
    }
    public void HeroUpgradeOnClick()
    {
        if (upgradeType == 0)
        {
            //TODO : 골드 있으면 레벨업
            if (BackEndGameData.Instance.UserGameData.gold >= needGold[heroLevel] &&
                BackEndGameData.Instance.UserHeroData.heroCount[heroNum] >= needHeroConut[heroLevel])
            {
                //Upgrade Hero
                UpgradeHero();
            }
            else if (BackEndGameData.Instance.UserGameData.gold <= needGold[heroLevel])
            {
                SoundManager.instance.ErrorClipPlay();
                PopUpMessageBase.instance.SetMessage("골드가 충분하지 않습니다");
            }
            else if (BackEndGameData.Instance.UserHeroData.heroCount[heroNum] >= needHeroConut[heroLevel])
            {
                SoundManager.instance.ErrorClipPlay();
                PopUpMessageBase.instance.SetMessage("영웅 갯수가 충분하지 않습니다");
            }
            HeroDetailUpdate();
        }else if (upgradeType == 1)
        {
            //진화 아이템 갯수 확인 후 진화 진행

        }
    }
    
    private void UpgradeHero()
    {
        BackEndGameData.Instance.UserGameData.gold -= needGold[heroLevel];
        //HeroInfo heroInfo = heroGo.GetComponent<HeroInfo>();
        //int heroNum = heroInfo.ReturnHeroNum();
        BackEndGameData.Instance.UserQuestData.questProgress[7]++;
        BackEndGameData.Instance.UserQuestData.questProgress[2] += needGold[heroLevel];
        BackEndGameData.Instance.UserHeroData.heroLevel[heroNum]++;
        BackEndGameData.Instance.UserHeroData.heroCount[heroNum] -= needHeroConut[heroLevel];
        BackEndGameData.Instance.UserQuestData.repeatQuest[6] += needGold[heroLevel];
        BackEndGameData.Instance.UserQuestData.repeatQuest[8] ++;

        SoundManager.instance.PlaySound(upgradeClip);
        heroInfo.UpdateHeroInfo();
        HeroSetManager.instance.UpdateInfo();
        BackEndGameData.Instance.GameDataUpdate();
    }
    public void ChooseHeroBtnClick()
    {
        SoundManager.instance.BtnClickPlay();
        if (HeroSetManager.instance.squadCount < 4 && heroLevel > 0)
        {
            // 중복된 히어로 확인
            for (int a = 0; a < 4; a++)
            {
                if (HeroSetManager.instance.ReturnHeroInfo(a).GetComponent<SquadHero>().hero != null &&
                    HeroSetManager.instance.ReturnHeroInfo(a).GetComponent<SquadHero>().hero.GetComponent<HeroInfo>().ReturnHeroNum() == 
                    heroGo.GetComponent<HeroInfo>().ReturnHeroNum())
                {
                    SoundManager.instance.ErrorClipPlay();
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
            SoundManager.instance.ErrorClipPlay();
            PopUpMessageBase.instance.SetMessage("스쿼드가 가득 찼습니다");
        }
        else if (heroLevel <= 0)
        {
            SoundManager.instance.ErrorClipPlay();
            PopUpMessageBase.instance.SetMessage("핵쟁이가 0레벨 입니다");
        }
    }
    public void LevelUpOnClick()
    {
        evolvingGO.SetActive(false);
        levelUpGO.SetActive(true);
        upgradeType = 0;
        SoundManager.instance.BtnClickPlay();
        HeroDetailUpdate();
    }
    public void EvolvingOnClick()
    {
        evolvingGO.SetActive(true);
        levelUpGO.SetActive(false);
        levelUpBtn.image.color = levelUpBtn.colors.normalColor;
        upgradeType = 1;
        SoundManager.instance.BtnClickPlay();
        EvolvingManager.instance.UpdateUI(heroNum, evolvingInfo, heroInfo);
    }

}
