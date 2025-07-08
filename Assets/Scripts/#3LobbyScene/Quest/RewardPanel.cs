using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class RewardPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldAmountText;
    [SerializeField] TextMeshProUGUI gemAmountText;

    public void UpdateRewardUI(int goldAmount, int gemAmount)
    {
        this.transform.DOScale(Vector3.one, 0.2f);
        goldAmountText.text = goldAmount.ToString();
        gemAmountText.text = gemAmount.ToString();
    }
    public void ExitRewardPanel()
    {
        this.transform.DOScale(Vector3.zero, 0.2f);
    }
}
