using UnityEngine;
using TMPro;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;

    [SerializeField] AbilityDetail abilityDetail;
    [SerializeField] TextMeshProUGUI goldAmountText;
    [SerializeField] TextMeshProUGUI abilityCostText;
    [SerializeField] Ability[] abilities;

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
    public void UpdateUI()
    {
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
            //´É·Â »Ì±â
            DrawAbility();
            BackEndGameData.Instance.UserGameData.gold -= abilityCost;
            BackEndGameData.Instance.GameDataUpdate();
            UpdateUI();
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
        
        //AbilityDetail¿¡ ¶ç¿ì±â
        UpdateUI();
        OpenAbilityDetail(choosedAbility);
        BackEndGameData.Instance.UserAbilityData.abilityLevel[ranNum]++;
        BackEndGameData.Instance.GameDataUpdate();
    }
    public void OpenAbilityDetail(Ability _ability)
    {
        abilityDetail.gameObject.SetActive(true);
        abilityDetail.UpdateUI(_ability);
    }


}
