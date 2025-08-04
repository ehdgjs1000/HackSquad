using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChapterClear : MonoBehaviour
{
    public static ChapterClear instance;
    [SerializeField] ChapterReward[] chapterRewards;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] Button receiveBtn;
    [SerializeField] AudioClip chapterRecieveClip;

    public float highestChapter;
    public float highestRewardChapter;
    public float itemSize = 400;
    public float veiwSize = 1080;

    private void Awake()
    {
        if(instance == null) instance = this;
    }
    private void Start()
    {
        UpdateRecentReward();
    }
    public void UpdateRecentReward()
    {
        Color color;
        highestChapter = BackEndGameData.Instance.UserGameData.highestChapter;
        highestRewardChapter = BackEndGameData.Instance.UserGameData.highestRewardChapter;
        Debug.Log("HighestChapter : " + highestChapter +  " / HighestReward : " + highestRewardChapter);
        float value = (highestRewardChapter * itemSize) / (scrollRect.content.rect.width - veiwSize);
        scrollRect.horizontalNormalizedPosition = value;

        //현재 보상 챕터가 수령가능한 챕터보다 높을경우
        if (highestRewardChapter <= highestChapter)
        {
            //버튼 색상 변경 수령가능
            ColorUtility.TryParseHtmlString("#11FF0E", out color);
            receiveBtn.image.color = color;
        }
        else
        {
            //버튼 색상 변경 수령불가
            ColorUtility.TryParseHtmlString("#828282", out color);
            receiveBtn.image.color= color;
        }
    }
    public void ChapterRewardOnClick()
    {
        //현재 보상 챕터가 수령가능한 챕터보다 높을경우
        if (BackEndGameData.Instance.UserGameData.highestRewardChapter <=
            BackEndGameData.Instance.UserGameData.highestChapter)
        {
            //보상 제공
            if(BackEndGameData.Instance.UserGameData.highestRewardChapter == -1)
            {
                BackEndGameData.Instance.UserGameData.highestRewardChapter = 0;
            } 
            SoundManager.instance.PlaySound(chapterRecieveClip);
            chapterRewards[(int)BackEndGameData.Instance.UserGameData.highestRewardChapter].RewardRecieve();
            BackEndGameData.Instance.UserGameData.highestRewardChapter++;
            UpdateRecentReward();
            BackEndGameData.Instance.GameDataUpdate();
            LobbyManager.instance.UpdateUIAll();
        }
    }
}
