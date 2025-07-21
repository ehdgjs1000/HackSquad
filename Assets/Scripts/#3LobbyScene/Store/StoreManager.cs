using UnityEngine;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public static StoreManager instance;

    [SerializeField] TextMeshProUGUI goldAmountText;
    [SerializeField] TextMeshProUGUI gemAmountText;

    [SerializeField] GameObject[] storeGOs;

    private void Awake()
    {
        if(instance == null) instance = this;
    }
    private void Start()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        goldAmountText.text = BackEndGameData.Instance.UserGameData.gold.ToString();
        gemAmountText.text = BackEndGameData.Instance.UserGameData.gem.ToString(); 
    }
    public void StoreTypeOnClick(int num)
    {
        for (int i = 0; i < 3; i++)
        {
            if(i == num)storeGOs[i].SetActive(true);
            else storeGOs[i].SetActive(false);
        }
    }

}
