using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class RewardPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldAmountText;
    [SerializeField] TextMeshProUGUI gemAmountText;
    [SerializeField] TextMeshProUGUI exitText;
    [SerializeField] GameObject rewardGO;
    float canExitTime = 1.0f;
    bool canExit = false;
    bool rewardUIOn = false;

    private void Update()
    {
        if (rewardUIOn)
        {
            canExitTime -= Time.deltaTime;
            if (canExitTime <= 0.0f)
            {
                exitText.gameObject.SetActive(true);
                canExit = true;
            }
                
        }
    }

    public void UpdateRewardUI(int goldAmount, int gemAmount)
    {
        rewardUIOn = true;
        canExitTime = 1.0f;
        this.transform.DOScale(Vector3.one, 0);
        rewardGO.transform.DOScale(Vector3.zero, 0);
        rewardGO.transform.DOScale(Vector3.one, 1.0f);
        goldAmountText.text = goldAmount.ToString();
        gemAmountText.text = gemAmount.ToString();
    }
    public void ExitRewardPanel()
    {
        if (canExit)
        {
            exitText.gameObject.SetActive(false);
            rewardUIOn = false;
            canExit = false;
            this.transform.DOScale(Vector3.zero, 0);
        }
    }
}
