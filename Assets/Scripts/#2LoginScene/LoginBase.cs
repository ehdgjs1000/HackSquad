using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;

public class LoginBase : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMessage;

    /// <summary>
    /// 메세지 내용 초기화
    /// </summary>
    /// <param name="images"></param>
    protected void ResetUI(params Image[] images)
    {
        textMessage.text = string.Empty;
        for (int i = 0; i<images.Length; i++)
        {
            images[i].color = Color.white;
        }
    }
    protected void SetMessage(string msg)
    {
        textMessage.text = msg;
    }
    protected void GuidForIncorrectlyEnteredData(Image image, string msg)
    {
        textMessage.text = msg;
        image.color = Color.red;
    }
    protected bool IsFieldDataEmpty(Image image, string field, string result)
    {
        if (field.Trim().Equals(""))
        {
            GuidForIncorrectlyEnteredData(image, $"\"{result}\" 필드를 채워주세요");

            return true;
        }
        return false;
    }

}
