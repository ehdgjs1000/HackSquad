using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Package : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI packageNameText;
    [SerializeField] Sprite reward1Sprite, reward2Sprite;
    [SerializeField] Image reward1Image, reward2Image;
    [SerializeField] TextMeshProUGUI reward1AmountText, reward2AmountText;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] string packageName;
    [SerializeField] int cost;
    [SerializeField] int reward1Amount, reward2Amount;
   


    private void Start()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        packageNameText.text = packageName;
        reward1Image.sprite = reward1Sprite; ;
        reward2Image.sprite = reward2Sprite; ;
        reward1AmountText.text = reward1Amount.ToString();
        reward2AmountText.text = reward2Amount.ToString();
        costText.text = cost.ToString()+ "¿ø";
    }


}
