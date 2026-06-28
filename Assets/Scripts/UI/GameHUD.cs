using UnityEngine;
using TMPro;

public class GameHUD : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;

    void Update()
    {
        var sm = SquadManager.Instance;
        if (sm == null) return;
        if (levelText != null) levelText.text = $"Lv.{sm.currentLevel}";
        if (expText != null) expText.text = $"EXP {sm.currentExp}/{sm.expToNextLevel}";
    }
}