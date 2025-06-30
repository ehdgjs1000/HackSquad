using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;
using System;

public class Post : MonoBehaviour
{
    [SerializeField] Sprite[] spriteItemIcons;
    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI itemCountText;
    [SerializeField] TextMeshProUGUI itemTitleText;
    [SerializeField] TextMeshProUGUI itemContentText;
    [SerializeField] TextMeshProUGUI expirationDateText;

    [SerializeField] Button recieveBtn;

    BackendPostSystem backendPostSystem;
    PopUpPostBox popUpPostBox;
    PostData postData;

    public void SetUp(BackendPostSystem postSyste, PopUpPostBox postBox, PostData postData)
    {
        // 우편"수령" 버튼을 눌렀을 때 처리
        recieveBtn.onClick.AddListener(OnClickPostReceive);
        backendPostSystem = postSyste;
        popUpPostBox = postBox;
        this.postData = postData;

        itemTitleText.text = postData.title;
        itemContentText.text = postData.content;

        foreach (string itemKey in postData.postReward.Keys)
        {
            if (itemKey.Equals("gold")) itemIcon.sprite = spriteItemIcons[0];
            else if (itemKey.Equals("gem")) itemIcon.sprite = spriteItemIcons[1];
            else if (itemKey.Equals("energy")) itemIcon.sprite = spriteItemIcons[2];

            itemCountText.text = postData.postReward[itemKey].ToString();
            break;
        }
        Backend.Utils.GetServerTime(callback =>
        {
            if (!callback.IsSuccess())
            {
                Debug.LogError("서버 시간 불러오기에 실패했습니다.");
                return;
            }
            try
            {
                string serverTime = callback.GetFlattenJSON()["utcTime"].ToString();
                TimeSpan timeSpan = DateTime.Parse(postData.expirationDate) - DateTime.Parse(serverTime);
                expirationDateText.text = $"{timeSpan.TotalHours:F0}시간 후 만료";
                
            }catch (Exception e)
            {
                Debug.LogError(e);
            }

        });
    }

    private void OnClickPostReceive()
    {
        popUpPostBox.DestroyPost(gameObject);
        backendPostSystem.PostReceive(PostType.Admin, postData.inDate);
    }


}
