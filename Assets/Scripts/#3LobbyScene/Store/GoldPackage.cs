using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GoldPackage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI topGoldAmountText;
    [SerializeField] TextMeshProUGUI goldAmountText;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] Image goldImage;

    [SerializeField] int goldAmount;
    [SerializeField] int costAmount;
    [SerializeField] Sprite goldSprite;

    [SerializeField] bool isUserWon;

    private void Start()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        if(isUserWon) topGoldAmountText.text = "핵코인 x" + goldAmount.ToString();
        else topGoldAmountText.text = "골드 x" + goldAmount.ToString();

        goldAmountText.text = goldAmount.ToString();

        if(isUserWon) costText.text = costAmount.ToString() + "원";
        else costText.text = costAmount.ToString();

        goldImage.sprite = goldSprite;
    }
    public void BuyGoldOnClick()
    {
        BuyCheck.instance.UpdateUI("gold", goldAmount, costAmount);
    }
    public void BuyGemOnClick()
    {
        //추후 IAP 넣기
    }
}
