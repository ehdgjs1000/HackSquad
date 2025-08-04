using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;

    [SerializeField] AbilityDetail abilityDetail;
    [SerializeField] Transform abilityDrawCard;
    [SerializeField] TextMeshProUGUI goldAmountText;
    [SerializeField] TextMeshProUGUI abilityCostText;
    [SerializeField] Ability[] abilities;
    [SerializeField] TextMeshProUGUI abilityLevelText;
    [SerializeField] AudioClip drawAbilityClip;
    [SerializeField] TextMeshProUGUI remainVideoText;
    [SerializeField] Button videoBtn;

    float canExitDraw = 1.0f;
    int abilityLevel = 0;
    int abilityCost = 0;
    int remainVideoCount = 3;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        UpdateUI();
    }
    private void Update()
    {
        canExitDraw -= Time.deltaTime;
    }
    public void UpdateUI()
    {
        abilityLevel = 0;
        for (int i = 0; i < BackEndGameData.Instance.UserAbilityData.abilityLevel.Length; i++)
        {
            if (BackEndGameData.Instance.UserAbilityData.abilityLevel[i] > 0) abilityLevel += BackEndGameData.Instance.UserAbilityData.abilityLevel[i];
        }
        abilityLevelText.text = "Lv." + abilityLevel.ToString();
        int gold = BackEndGameData.Instance.UserGameData.gold;
        if (gold >= 10000)
        {
            goldAmountText.text = (gold / 1000).ToString() + "K";
        }
        else goldAmountText.text = gold.ToString();

        abilityCost = 4500 + (abilityLevel * 500);
        abilityCostText.text = abilityCost.ToString();

        for (int i = 0; i < abilities.Length; i++)
        {
            abilities[i].UpdateUI();
        }

        if (PlayerPrefs.HasKey("remainAbilityVideo")) 
            remainVideoCount = PlayerPrefs.GetInt("remainAbilityVideo");
        remainVideoText.text = remainVideoCount + "/1";
        if (remainVideoCount <= 0) videoBtn.image.color = Color.gray;
    }
    public void DrawAbilityVideoOnClick()
    {
        if (remainVideoCount > 0 && AdsVideo.instance.buttonClickTime <= 0.0f)
        {
            AdsVideo.instance.ShowVideo();
            DrawAbility();
            remainVideoCount--;
            PlayerPrefs.SetInt("remainAbilityVideo",remainVideoCount);
            BackEndGameData.Instance.UserQuestData.repeatQuest[1]++;
            BackEndGameData.Instance.GameDataUpdate();
            UpdateUI();
        }
        else if (remainVideoCount >= 0) PopUpMessageBase.instance.SetMessage("어빌리티 무료 뽑기를 모두 사용했습니다.");
        else PopUpMessageBase.instance.SetMessage("잠시 후 다시 눌러주세요.");

    }
    public void DrawAbilityOnClick()
    {
        SoundManager.instance.BtnClickPlay();
        if (BackEndGameData.Instance.UserGameData.gold >= abilityCost)
        {
            //능력 뽑기
            BackEndGameData.Instance.UserGameData.gold -= abilityCost;
            BackEndGameData.Instance.UserQuestData.questProgress[2] += abilityCost;
            BackEndGameData.Instance.UserQuestData.repeatQuest[1]++;
            BackEndGameData.Instance.UserQuestData.repeatQuest[6]+= abilityCost;
            DrawAbility();
            BackEndGameData.Instance.GameDataUpdate();
            UpdateUI();
        }
        else
        {
            PopUpMessageBase.instance.SetMessage("골드가 충분하지 않습니다.");
        }
    }
    private void DrawAbility()
    {
        Ability choosedAbility;
        float ranAbilityType = Random.Range(0, 100);
        int ranNum;
        if (ranAbilityType >= 0 && ranAbilityType <65)
        {
            ranNum = Random.Range(0, 3);
            choosedAbility = abilities[ranNum];
        }
        else if (ranAbilityType >= 65 && ranAbilityType < 90)
        {
            ranNum = Random.Range(3, 6);
            choosedAbility = abilities[ranNum];
        }
        else
        {
            ranNum = Random.Range(6, 12);
            choosedAbility = abilities[ranNum];
        }

        //AbilityDetail에 띄우기
        SoundManager.instance.PlaySound(drawAbilityClip);
        
        BackEndGameData.Instance.UserAbilityData.abilityLevel[ranNum]++;
        UpdateUI();
        OpenAbilityDetail(choosedAbility);
        BackEndGameData.Instance.GameDataUpdate();
    }
    public void OpenAbilityDetail(Ability _ability)
    {
        canExitDraw = 1.1f;
        abilityDetail.gameObject.SetActive(true);
        abilityDetail.UpdateUI(_ability);
        abilityDrawCard.DOScale(Vector3.one, 1.0f);
        abilityDrawCard.DOLocalMoveY(0,1.0f);
    }
    public void ExitDrawCard()
    {
        if (canExitDraw <= 0.0f)
        {
            abilityDrawCard.DOScale(Vector3.zero, 0);
            abilityDrawCard.DOLocalMoveY(-1500, 0f);
            abilityDetail.gameObject.SetActive(false);
        }
    }


}
