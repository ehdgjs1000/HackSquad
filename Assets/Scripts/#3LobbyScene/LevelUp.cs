using UnityEngine;
using TMPro;

public class LevelUp : MonoBehaviour
{
    public static LevelUp instance;

    [SerializeField] GameObject levelUpPanel;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI gemText;
    [SerializeField] TextMeshProUGUI goldText;
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
        int level = BackEndGameData.Instance.UserGameData.level;
        levelText.text = "Lv." + level.ToString();
        gemAmount = ((level / 5) + 1) * 50;
        goldAmount = ((level/5)+1) * 500;
        gemText.text = gemAmount.ToString();
        goldText.text = goldAmount.ToString();
        BackEndGameData.Instance.UserGameData.gem += gemAmount;
        BackEndGameData.Instance.UserGameData.gold += goldAmount;
        
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
