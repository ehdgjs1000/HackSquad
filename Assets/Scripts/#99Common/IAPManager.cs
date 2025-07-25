using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using TMPro;

public class IAPManager : MonoBehaviour, IStoreListener
{
    IStoreController storeController;

    string package1100 = "package1100";
    string package3300 = "package3300";
    string package9900 = "package9900";
    string package49000 = "package49000";
    string gem990 = "gem990";
    string gem4900 = "gem4900";
    string gem9900 = "gem9900";
    string gem49000 = "gem49000";
    string gem99000 = "gem99000";

    private void Start()
    {
        InitIAP();
    }

    private void InitIAP()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(package1100, ProductType.Consumable);
        builder.AddProduct(package3300, ProductType.Consumable);
        builder.AddProduct(package9900, ProductType.Consumable);
        builder.AddProduct(package49000, ProductType.Consumable);

        builder.AddProduct(gem990, ProductType.Consumable);
        builder.AddProduct(gem4900, ProductType.Consumable);
        builder.AddProduct(gem9900, ProductType.Consumable);
        builder.AddProduct(gem49000, ProductType.Consumable);
        builder.AddProduct(gem99000, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("초기화 실패 : " + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log("초기화 실패 : " + error + message);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("구매 실패");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;

        Debug.Log("구매 성공 : " + product.definition.id);

        if (product.definition.id == package1100)
        {
            PopUpMessageBase.instance.SetMessage("패키지 구매 성공");
            BackEndGameData.Instance.UserGameData.gold += 2000;
            BackEndGameData.Instance.UserGameData.gem += 200;
        }else if (product.definition.id == package3300)
        {
            PopUpMessageBase.instance.SetMessage("패키지 구매 성공");
            BackEndGameData.Instance.UserGameData.gold += 6000;
            BackEndGameData.Instance.UserGameData.gem += 660;
        }
        else if (product.definition.id == package9900)
        {
            PopUpMessageBase.instance.SetMessage("패키지 구매 성공");
            BackEndGameData.Instance.UserGameData.gold += 20000;
            BackEndGameData.Instance.UserGameData.gem += 2000;
        }
        else if (product.definition.id == package49000)
        {
            PopUpMessageBase.instance.SetMessage("패키지 구매 성공");
            BackEndGameData.Instance.UserGameData.gold += 100000;
            BackEndGameData.Instance.UserGameData.gem += 10000;
        }
        else if (product.definition.id == gem990)
        {
            PopUpMessageBase.instance.SetMessage("다이아 100개 구매 성공");
            BackEndGameData.Instance.UserGameData.gem += 100;
        }
        else if (product.definition.id == gem4900)
        {
            PopUpMessageBase.instance.SetMessage("다이아 500개 구매 성공");
            BackEndGameData.Instance.UserGameData.gem += 500;
        }
        else if (product.definition.id == gem9900)
        {
            PopUpMessageBase.instance.SetMessage("다이아 1000개 구매 성공");
            BackEndGameData.Instance.UserGameData.gem += 1000;
        }
        else if (product.definition.id == gem49000)
        {
            PopUpMessageBase.instance.SetMessage("다이아 5000개 구매 성공");
            BackEndGameData.Instance.UserGameData.gem += 5000;
        }
        else if (product.definition.id == gem99000)
        {
            PopUpMessageBase.instance.SetMessage("다이아 10000개 구매 성공");
            BackEndGameData.Instance.UserGameData.gem += 10000;
        }
        BackEndGameData.Instance.GameDataUpdate();
        LobbyManager.instance.UpdateUIAll();

        return PurchaseProcessingResult.Complete;
    }

    public void Purchase(string productID)
    {
        storeController.InitiatePurchase(productID);
    }

}
