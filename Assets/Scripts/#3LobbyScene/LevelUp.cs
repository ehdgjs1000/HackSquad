using UnityEngine;
using TMPro;

public class LevelUp : MonoBehaviour
{
    public static LevelUp instance;

    [SerializeField] AudioClip levelUpClip;
    [SerializeField] GameObject levelUpPanel;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI gemText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI upgradeJuiceText;
    int gemAmount, goldAmount;

    float closeTerm = 1.0f;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Update()
    {
        closeTerm -= Time.deltaTime;
    }

    public void LevelUpUpdate()
    {
        closeTerm = 1.0f;
        levelUpPanel.transform.localScale = Vector3.one;
        SoundManager.instance.PlaySound(levelUpClip);
        int level = BackEndGameData.Instance.UserGameData.level;

        //Text UI Update
        levelText.text = "Lv." + level.ToString();
        gemAmount = ((level / 5) + 1) * 50;
        goldAmount = ((level/5)+1) * 500;
        gemText.text = gemAmount.ToString();
        goldText.text = goldAmount.ToString();
        upgradeJuiceText.text = "1";

        //Backend Update
        BackEndGameData.Instance.UserInvenData.juiceLevel1ItemCount++;
        BackEndGameData.Instance.UserGameData.gem += gemAmount;
        BackEndGameData.Instance.UserGameData.gold += goldAmount;
        BackEndGameData.Instance.UserGameData.energy += 20;
        
        BackEndGameData.Instance.GameDataUpdate();
        LobbyManager.instance.UpdateUIAll();
    }
    public void CloseLevelUpPanel()
    {
        if (closeTerm <= 0.0f)
        {
            levelUpPanel.transform.localScale = Vector3.zero;
        }

        
    }

}
