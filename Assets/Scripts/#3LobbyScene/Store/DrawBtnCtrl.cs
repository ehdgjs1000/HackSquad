using UnityEngine;
using TMPro;

public class DrawBtnCtrl : MonoBehaviour
{
    [SerializeField] int cost;
    [SerializeField] TextMeshProUGUI gemText;

    public void UpdateDrawBtn()
    {
        Color color;
        if (BackEndGameData.Instance.UserGameData.gem <= cost)
        {
            //ColorUtility.TryParseHtmlString("#6BFF28", out color);
            gemText.color = Color.red;
        }
        else gemText.color = Color.white;
    }

}
