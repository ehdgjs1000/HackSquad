using UnityEngine;
using TMPro;
using BackEnd;


public class BackendCouponSystem : MonoBehaviour
{
    [SerializeField] TMP_InputField inputFieldCode;
    [SerializeField] FadeEffect_TMP resultText;
    
    public void ReceiveCoupon()
    {
        string couponCode = inputFieldCode.text;
        if (couponCode.Trim().Equals(""))
        {
            resultText.FadeOut("���� �ڵ带 �Է����ּ���");
            return;
        }
        inputFieldCode.text = "";

        ReceiveCoupon(couponCode);
    }
    public void ReceiveCoupon(string couponCode)
    {
        Backend.Coupon.UseCoupon(couponCode, callback =>
        {
            if (!callback.IsSuccess())
            {
                FailedToRecieve(callback);
                return;
            }

            try
            {
                LitJson.JsonData jsonData = callback.GetFlattenJSON()["itemObject"];
                if (jsonData.Count <= 0)
                {
                    Debug.LogWarning("������ �������� �����ϴ�.");
                    return;
                }
                SaveToLocal(jsonData);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }

        });
    }
    private void FailedToRecieve(BackendReturnObject callback)
    {
        if (callback.GetMessage().Contains("���� ����"))
        {
            resultText.FadeOut("���� ���� ������ �����Ǿ��ų� �Ⱓ�� ����� �����Դϴ�.");
        }else if (callback.GetMessage().Contains("�̹� ����Ͻ� ����"))
        {
            resultText.FadeOut("�ش� ������ �̹� ����ϼ̽��ϴ�.");
        }
        else
        {
            resultText.FadeOut("���� �ڵ尡 �߸��Ǿ��ų� �̹� ����� �����Դϴ�.");
        }

    }
    private void SaveToLocal(LitJson.JsonData items)
    {
        try
        {
            string getItems = string.Empty;

            foreach (LitJson.JsonData item in items)
            {
                int itemId = int.Parse(item["item"]["itemId"].ToString());
                string itemName = item["item"]["itemName"].ToString();
                string itemInfo = item["item"]["itemInfo"].ToString();
                int itemCount = int.Parse(item["itemCount"].ToString());

                if (itemName.Equals("gold")) BackEndGameData.Instance.UserGameData.gold += itemCount;
                else if (itemName.Equals("gem")) BackEndGameData.Instance.UserGameData.gem += itemCount;
                else if (itemName.Equals("energy")) BackEndGameData.Instance.UserGameData.energy += itemCount;

                getItems += $"{itemName} : {itemCount}";
            }

            resultText.FadeOut($"���� ������� ������ {getItems}�� ȹ���߽��ϴ�.");

            BackEndGameData.Instance.GameDataUpdate();
            MainManager.instance.UpdateMainUI();
        }
        catch(System.Exception e)
        {
            Debug.LogError(e);
        }

    }
}
