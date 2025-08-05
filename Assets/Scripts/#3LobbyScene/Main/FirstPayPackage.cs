using UnityEngine;
using UnityEngine.UI;

public class FirstPayPackage : MonoBehaviour
{
    [SerializeField] Button buyButton; 
    [SerializeField] GameObject buyCompleteGo;

    private void Update()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        if (!BackEndGameData.Instance.UserCashData.isBuyFirstPackage)
        {
            buyButton.image.color = Color.yellow;
            buyButton.interactable = true;
        }
        else
        {
            buyButton.gameObject.SetActive(false);
            buyCompleteGo.SetActive(true);
        }
    }

}
