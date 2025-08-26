using UnityEngine;
using UnityEngine.UI;

public class EvolvingSweepPanel : MonoBehaviour
{
    [SerializeField] Image rewardImage;
    public void SetUp(int rewardType)
    {
        Color color;
        if(rewardType == 0)
        {
            ColorUtility.TryParseHtmlString("#08FF00", out color);
            rewardImage.color = color;
        }else if (rewardType == 1)
        {
            ColorUtility.TryParseHtmlString("#1C57FF", out color);
            rewardImage.color = color;
        }
        else if (rewardType == 2)
        {
            ColorUtility.TryParseHtmlString("#D01CFF", out color);
            rewardImage.color = color;
        }
        else
        {
            ColorUtility.TryParseHtmlString("#FF1C28", out color);
            rewardImage.color = color;
        }

    }

}
