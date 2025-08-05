using UnityEngine;
using TMPro;

public class DrawBtnCtrl : MonoBehaviour
{
    [SerializeField] int cost;
    [SerializeField] TextMeshProUGUI gemText;

    public void UpdateDrawBtn()
    {
        if (BackEndGameData.Instance.UserGameData.gem <= cost)
        {
            gemText.color = Color.red;
        }
        else gemText.color = Color.white;
    }

}
