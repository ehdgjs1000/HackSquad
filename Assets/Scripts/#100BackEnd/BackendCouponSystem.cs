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
            resultText.FadeOut("쿠폰 코드를 입력해주세요");
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
                    Debug.LogWarning("쿠폰에 아이템이 없습니다.");
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
        if (callback.GetMessage().Contains("전부 사용된"))
        {
            resultText.FadeOut("쿠폰 발행 개수가 소진되었거나 기간이 만료된 쿠폰입니다.");
        }else if (callback.GetMessage().Contains("이미 사용하신 쿠폰"))
        {
            resultText.FadeOut("해당 쿠폰은 이미 사용하셨습니다.");
        }
        else
        {
            resultText.FadeOut("쿠폰 코드가 잘못되었거나 이미 사용한 쿠폰입니다.");
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

            resultText.FadeOut($"쿠폰 사용으로 아이템 {getItems}을 획득했습니다.");

            BackEndGameData.Instance.GameDataUpdate();
            MainManager.instance.UpdateMainUI();
        }
        catch(System.Exception e)
        {
            Debug.LogError(e);
        }

    }
}
