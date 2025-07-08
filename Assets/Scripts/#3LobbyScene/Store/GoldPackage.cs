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
        if(isUserWon) topGoldAmountText.text = "���̾� x" + goldAmount.ToString();
        else topGoldAmountText.text = "��� x" + goldAmount.ToString();

        goldAmountText.text = goldAmount.ToString();

        if(isUserWon) costText.text = costAmount.ToString() + "��";
        else costText.text = costAmount.ToString();

        goldImage.sprite = goldSprite;
    }
    public void BuyGoldOnClick()
    {
        if(BackEndGameData.Instance.UserGameData.gem >= costAmount)
        {
            Debug.Log("BuyGold :" + goldAmount );
            BackEndGameData.Instance.UserGameData.gem -= costAmount;
            BackEndGameData.Instance.UserGameData.gold += goldAmount;
            BackEndGameData.Instance.GameDataUpdate();
            StoreManager.instance.UpdateUI();
        }
        else PopUpMessageBase.instance.SetMessage(" ���̾ư� ������� �ʽ��ϴ�");
    }
    public void BuyGemOnClick()
    {
        //���� IAP �ֱ�
    }
}
