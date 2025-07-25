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
        int gold = BackEndGameData.Instance.UserGameData.gold;
        gemText.text = BackEndGameData.Instance.UserGameData.gem.ToString();
        
        if(gold >= 10000)
        {
            goldText.text = (gold/1000).ToString() +"K";
        }else goldText.text = gold.ToString();

        for (int i = 0; i <drawBtns.Length; i++)
        {
            drawBtns[i].UpdateDrawBtn();
        }
    }

}
