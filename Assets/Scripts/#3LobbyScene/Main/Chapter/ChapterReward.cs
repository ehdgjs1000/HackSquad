using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ChapterReward : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI chapterNumText;
    [SerializeField] TextMeshProUGUI chapterWaveNumText;
    [SerializeField] TextMeshProUGUI goldAmountText, gemAmountText;
    [SerializeField] int chapterNum;
    [SerializeField] int chapterWaveNum;

    [SerializeField] int goldAmount,gemAmount;
    private void Start()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        chapterNumText.text = "é��" + chapterNum.ToString();
        chapterWaveNumText.text = "���̺�" + chapterWaveNum.ToString();
        goldAmountText.text = goldAmount.ToString();
        gemAmountText.text = gemAmount.ToString();
    }
    public void RewardRecieve()
    {
        BackEndGameData.Instance.UserGameData.gold += goldAmount;
        BackEndGameData.Instance.UserGameData.gem += gemAmount;
    }

}
