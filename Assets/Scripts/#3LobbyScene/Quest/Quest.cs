using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quest : MonoBehaviour
{
    [SerializeField] Image progressImage;
    [SerializeField] TextMeshProUGUI clearAmountText;
    [SerializeField] float targetAmount;
    [SerializeField] float clearAmount;
     
    private void Start()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        clearAmountText.text = clearAmount.ToString() +"/"+targetAmount.ToString();
        progressImage.fillAmount = clearAmount / targetAmount;
    }
}
