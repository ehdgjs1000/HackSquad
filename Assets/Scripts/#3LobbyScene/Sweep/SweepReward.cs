using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SweepReward : MonoBehaviour
{
    [SerializeField] Image rewardImage;
    [SerializeField] TextMeshProUGUI rewardText;

    [SerializeField] Sprite rewardSprite;
    [SerializeField] int rewardCount;

    public void Setting(float _amount)
    {
        rewardText.text = _amount.ToString("F0");
    }
    public void RewardAnimation()
    {
        StartCoroutine(StartAnim());
    }
    
    IEnumerator StartAnim()
    {
        this.transform.DOScale(Vector3.zero, 0);
        this.transform.DOScale(new Vector3(3,3,3), 0.3f);
        yield return new WaitForSeconds(0.3f);
        this.transform.DOScale(Vector3.one, 0.2f);
    }

}
