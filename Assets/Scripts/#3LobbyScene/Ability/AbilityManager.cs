using UnityEngine;
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

    float canExitDraw = 1.0f;
    int abilityLevel = 0;
    int abilityCost = 0;
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
            if (BackEndGameData.Instance.UserAbilityData.abilityLevel[i] > 0) abilityLevel++;
        }
        abilityLevelText.text = "Lv." + abilityLevel.ToString();
        goldAmountText.text = BackEndGameData.Instance.UserGameData.gold.ToString();

        abilityCost = 10000 + (abilityLevel * 1000);
        abilityCostText.text = abilityCost.ToString();

        for (int i = 0; i < abilities.Length; i++)
        {
            abilities[i].UpdateUI();
        }
    }
    public void DrawAbilityOnClick()
    {
        if(BackEndGameData.Instance.UserGameData.gold >= abilityCost)
        {
            //�ɷ� �̱�
            DrawAbility();
            BackEndGameData.Instance.UserGameData.gold -= abilityCost;
            BackEndGameData.Instance.UserQuestData.questProgress[2] += abilityCost;
            BackEndGameData.Instance.GameDataUpdate();
            UpdateUI();
        }
        else
        {
            PopUpMessageBase.instance.SetMessage("��尡 ������� �ʽ��ϴ�.");
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
        
        //AbilityDetail�� ����
        UpdateUI();
        BackEndGameData.Instance.UserAbilityData.abilityLevel[ranNum]++;
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
