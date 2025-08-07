using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GoldDungeon : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldAmountText;
    [SerializeField] TextMeshProUGUI remainAmountText;

    [SerializeField] int dungeonNum;
    [SerializeField] int goldAmount;


    private void Start()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        string text = "gold" + dungeonNum;
        goldAmountText.text = goldAmount.ToString();
        remainAmountText.text = $"¼ÒÅÁ °¡´É È½¼ö:{PlayerPrefs.GetInt("text")}";

        //Update Button UI
        //if(dungeonNum <= )

    }


}
