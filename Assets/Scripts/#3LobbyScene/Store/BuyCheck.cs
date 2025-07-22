using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BuyCheck : MonoBehaviour
{
    public static BuyCheck instance;

    [SerializeField] Sprite[] itemSprites; // gold, gem,
    [SerializeField] Image buyItemImage;
    [SerializeField] TextMeshProUGUI buyItemCount;
    [SerializeField] AudioClip buyClip;

    public int buyAmount;
    string buyType;
    int cost;


    private void Awake()
    {
        if (instance == null) instance = this;
    }
    public void UpdateUI(string _buyType, int _buyAmount, int _cost)
    {
        this.transform.DOScale(Vector3.one, 0.15f);
        switch (_buyType)
        {
            case "gold":
                buyType = "gold";
                buyItemImage.sprite = itemSprites[0];
                break;
        }
        cost = _cost;
        buyAmount = _buyAmount;
        buyItemCount.text = buyAmount.ToString();

    }
    public void BuyOnClick()
    {
        switch (buyType)
        {
            case "gold":
                if (BackEndGameData.Instance.UserGameData.gem >= cost)
                {
                    Debug.Log("BuyGold :" + buyAmount);
                    BackEndGameData.Instance.UserGameData.gem -= cost;
                    BackEndGameData.Instance.UserGameData.gold += buyAmount;
                    BackEndGameData.Instance.UserQuestData.questProgress[3] += cost;
                    BackEndGameData.Instance.UserQuestData.repeatQuest[7] += cost;
                    BackEndGameData.Instance.GameDataUpdate();
                    StoreManager.instance.UpdateUI();
                    SoundManager.instance.PlaySound(buyClip);
                    this.transform.DOScale(Vector3.zero, 0.15f);
                    PopUpMessageBase.instance.SetMessage("골드 구매 완료");
                }
                else PopUpMessageBase.instance.SetMessage(" 다이아가 충분하지 않습니다");
                break;
        }
    }
    public void CloseOnClick()
    {
        this.transform.DOScale(Vector3.zero, 0.15f);
    }


}
