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

        //���� ���� é�Ͱ� ���ɰ����� é�ͺ��� �������
        if (highestRewardChapter <= highestChapter)
        {
            //��ư ���� ���� ���ɰ���
            ColorUtility.TryParseHtmlString("#11FF0E", out color);
            receiveBtn.image.color = color;
        }
        else
        {
            //��ư ���� ���� ���ɺҰ�
            ColorUtility.TryParseHtmlString("#828282", out color);
            receiveBtn.image.color= color;
        }
    }
    public void ChapterRewardOnClick()
    {
        //���� ���� é�Ͱ� ���ɰ����� é�ͺ��� �������
        if (BackEndGameData.Instance.UserGameData.highestRewardChapter <=
            BackEndGameData.Instance.UserGameData.highestChapter)
        {
            //���� ����
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
