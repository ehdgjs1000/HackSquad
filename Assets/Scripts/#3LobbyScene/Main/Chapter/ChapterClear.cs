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
    [SerializeField] Image canClearImage;

    public float highestChapter;
    public float highestRewardChapter;
    public float itemSize = 400;
    public float veiwSize = 1080;

    float updateInterval = 1.0f;
    float updateCheck = 0.0f;
    float value;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Update()
    {
        updateCheck += Time.deltaTime;
        if (updateCheck >= updateInterval) UpdateRecentReward();   
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
        value = (highestRewardChapter * itemSize) / (scrollRect.content.rect.width - veiwSize);
        scrollRect.horizontalNormalizedPosition = value;

        //���� ���� é�Ͱ� ���ɰ����� é�ͺ��� �������
        if (highestRewardChapter <= highestChapter)
        {
            //��ư ���� ���� ���ɰ���
            canClearImage.gameObject.SetActive(true);
            ColorUtility.TryParseHtmlString("#11FF0E", out color);
            receiveBtn.image.color = color;
        }
        else
        {
            //��ư ���� ���� ���ɺҰ�
            canClearImage.gameObject.SetActive(false);
            ColorUtility.TryParseHtmlString("#828282", out color);
            receiveBtn.image.color= color;
        }
    }
    public void ChapterRewardOnClick()
    {
        Debug.Log($"Click!! {BackEndGameData.Instance.UserGameData.highestRewardChapter} <= {BackEndGameData.Instance.UserGameData.highestChapter}");
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
    public void ChapterClearExitOnClick()
    {
        this.transform.localScale = Vector3.zero;
        SoundManager.instance.BtnClickPlay();
    }
}
