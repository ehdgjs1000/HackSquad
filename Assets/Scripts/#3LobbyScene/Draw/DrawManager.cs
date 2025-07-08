using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DrawManager : MonoBehaviour
{
    public static DrawManager instance;

    [SerializeField] TextMeshProUGUI gemText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] DrawBtnCtrl[] drawBtns;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void UpdateMainUI()
    {
        gemText.text = BackEndGameData.Instance.UserGameData.gem.ToString();
        goldText.text = BackEndGameData.Instance.UserGameData.gold.ToString();
        for (int i = 0; i <drawBtns.Length; i++)
        {
            drawBtns[i].UpdateDrawBtn();
        }
    }

}
