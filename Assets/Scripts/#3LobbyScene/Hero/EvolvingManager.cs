using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvolvingManager : MonoBehaviour
{
    public static EvolvingManager instance;

    EvolvingInfo evolvingInfo;
    HeroInfo heroInfo;

    [SerializeField] GameObject[] leftStars;
    [SerializeField] GameObject[] rightStars;
    [SerializeField] TextMeshProUGUI needItemText;
    [SerializeField] TextMeshProUGUI skillDesc;
    [SerializeField] Image skillImage;
    [SerializeField] GameObject mainSet;
    [SerializeField] GameObject highestLevelSet;
    [SerializeField] GameObject notYetLevelSet;
    [SerializeField] AudioClip upgradeClip;
    [SerializeField] TextMeshProUGUI needEvolvingNeedCountText;
    [SerializeField] TextMeshProUGUI nowEvolvingNeedCountText;

    int nowEvolvingLevel;
    int nextEvolvingLevel;
    int heroNum;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateUI(int _heroNum, EvolvingInfo _evolvingInfo, HeroInfo _heroInfo)
    {
        heroNum = _heroNum;
        heroInfo = _heroInfo;
        evolvingInfo = _evolvingInfo;
        nowEvolvingLevel = BackEndGameData.Instance.UserEvolvingData.evolvingLevel[_heroNum];
        nextEvolvingLevel = nowEvolvingLevel + 1;

        //진화 재료 갯수 text 관리
        int nowEvolvingNeedCount = 0;
        int needEvolvingNeedCount = 0;
        needEvolvingNeedCount = (BackEndGameData.Instance.UserEvolvingData.evolvingLevel[heroInfo.ReturnHeroNum()]+1) * 2;
        if(heroInfo.ReturnHeroGrade() == 0) nowEvolvingNeedCount = BackEndGameData.Instance.UserInvenData.juiceLevel1ItemCount;
        else if(heroInfo.ReturnHeroGrade() == 1) nowEvolvingNeedCount = BackEndGameData.Instance.UserInvenData.juiceLevel2ItemCount;
        else if(heroInfo.ReturnHeroGrade() == 2) nowEvolvingNeedCount = BackEndGameData.Instance.UserInvenData.juiceLevel3ItemCount;
        else if(heroInfo.ReturnHeroGrade() == 3) nowEvolvingNeedCount = BackEndGameData.Instance.UserInvenData.juiceLevel4ItemCount;
        needEvolvingNeedCountText.text = needEvolvingNeedCount.ToString();
        nowEvolvingNeedCountText.text = nowEvolvingNeedCount.ToString();
        if (needEvolvingNeedCount > nowEvolvingNeedCount) nowEvolvingNeedCountText.color = Color.red;
        else nowEvolvingNeedCountText.color = Color.green;

        //상단 별 ui관리
        foreach (GameObject starGo in leftStars) starGo.SetActive(false);
        foreach (GameObject starGo in rightStars) starGo.SetActive(false);
        if (BackEndGameData.Instance.UserHeroData.heroLevel[_heroNum] < 3)
        {
            notYetLevelSet.SetActive(true);
            mainSet.SetActive(false);
            highestLevelSet.SetActive(false);
        }
        else
        {
            notYetLevelSet.SetActive(false);
            if (nowEvolvingLevel < 3)
            {
                highestLevelSet.SetActive(false);
                mainSet.SetActive(true);
                for (int i = 0; i < nowEvolvingLevel; i++) leftStars[i].SetActive(true);
                for (int i = 0; i < nextEvolvingLevel; i++) rightStars[i].SetActive(true);

                //Evolving Info Ui Update
                if (evolvingInfo.evolvingSprites[nowEvolvingLevel] != null)
                {
                    skillImage.sprite = evolvingInfo.evolvingSprites[nowEvolvingLevel];
                }
                else
                {
                    skillImage.sprite = null;
                }
                skillDesc.text = evolvingInfo.evolvingDesc[nowEvolvingLevel];
            }
            else
            {
                
                highestLevelSet.SetActive(true);
                mainSet.SetActive(false);
            }
        }
    }
    public void HeroEvolvingOnClick()
    {
        int needCount = (BackEndGameData.Instance.UserEvolvingData.evolvingLevel[heroInfo.ReturnHeroNum()]+1) * 2;
        if (heroInfo.ReturnHeroGrade() == 0)
        {
            //진화 갯수가 있을 경우
            if (BackEndGameData.Instance.UserInvenData.juiceLevel1ItemCount > needCount)
            {
                BackEndGameData.Instance.UserInvenData.juiceLevel1ItemCount -= needCount;
                EvolveHero(heroInfo.ReturnHeroNum());
            }
            else PopUpMessageBase.instance.SetMessage("진화 재료가 부족합니다");
        }else if (heroInfo.ReturnHeroGrade() == 1)
        {
            //진화 갯수가 있을 경우
            if (BackEndGameData.Instance.UserInvenData.juiceLevel2ItemCount > needCount)
            {
                BackEndGameData.Instance.UserInvenData.juiceLevel2ItemCount -= needCount;
                EvolveHero(heroInfo.ReturnHeroNum());
            }
            else PopUpMessageBase.instance.SetMessage("진화 재료가 부족합니다");
        }
        else if (heroInfo.ReturnHeroGrade() == 2)
        {
            //진화 갯수가 있을 경우
            if (BackEndGameData.Instance.UserInvenData.juiceLevel3ItemCount > needCount)
            {
                BackEndGameData.Instance.UserInvenData.juiceLevel3ItemCount -= needCount;
                EvolveHero(heroInfo.ReturnHeroNum());
            }
            else PopUpMessageBase.instance.SetMessage("진화 재료가 부족합니다");
        }
        else if (heroInfo.ReturnHeroGrade() == 3)
        {
            //진화 갯수가 있을 경우
            if (BackEndGameData.Instance.UserInvenData.juiceLevel4ItemCount > needCount)
            {
                BackEndGameData.Instance.UserInvenData.juiceLevel4ItemCount -= needCount;
                EvolveHero(heroInfo.ReturnHeroNum());
            }
            else PopUpMessageBase.instance.SetMessage("진화 재료가 부족합니다");
        }
    }
    public void EvolveHero(int _heroNum)
    {
        BackEndGameData.Instance.UserEvolvingData.evolvingLevel[_heroNum]++;
        SoundManager.instance.PlaySound(upgradeClip);
        heroInfo.UpdateHeroInfo();
        UpdateUI(heroNum,evolvingInfo,heroInfo);
        HeroSetManager.instance.UpdateInfo();
        BackEndGameData.Instance.GameDataUpdate();
    }


}
