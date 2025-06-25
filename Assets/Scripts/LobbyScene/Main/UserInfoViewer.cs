using UnityEngine;
using TMPro;

public class UserInfoViewer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nickNameText;

    public void UpdateNickName()
    {
        nickNameText.text = UserInfo.Data.nickName == null ?
                            UserInfo.Data.gamerId : UserInfo.Data.nickName;
    }

}
