using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DrawManager : MonoBehaviour
{
    public static DrawManager instance;

    [SerializeField] TextMeshProUGUI gemText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI drawRemainText;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void UpdateMainUI()
    {
        int gold = BackEndGameData.Instance.UserGameData.gold;
        gemText.text = BackEndGameData.Instance.UserGameData.gem.ToString();

        if (PlayerPrefs.HasKey("DrawVideo"))
        {
            int remainVideo = PlayerPrefs.GetInt("DrawVideo");
            drawRemainText.text = remainVideo + "/3";
        }
        else drawRemainText.text = "3/3";

        if (gold >= 10000)
        {
            goldText.text = (gold/1000).ToString() +"K";
        }else goldText.text = gold.ToString();

    }

}
