using UnityEngine;
using UnityEngine.UI;

public class GameSpeedPackage : MonoBehaviour
{

    [SerializeField] Button buyButton;
    [SerializeField] GameObject buyCompleteGo;

    private void Update()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        if (!BackEndGameData.Instance.UserCashData.isBuyGameSpeedPackage)
        {
            buyButton.image.color = Color.yellow;
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
            buyCompleteGo.SetActive(true);
        }
    }
}
