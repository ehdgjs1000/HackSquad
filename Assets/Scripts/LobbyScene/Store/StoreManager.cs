using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public static StoreManager instance;

    [SerializeField] TextMeshProUGUI gemText;
    [SerializeField] TextMeshProUGUI goldText;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void UpdateMainUI()
    {
        gemText.text = BackEndGameData.Instance.UserGameData.gem.ToString();
        goldText.text = BackEndGameData.Instance.UserGameData.gold.ToString();
    }

}
