using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    //��� �޼�����
    [SerializeField] TextMeshProUGUI weeklyAchieveText;
    [SerializeField] TextMeshProUGUI dailyAchieveText;
    [SerializeField] Image weeklyAchieveProgress;
    [SerializeField] Image dailyAchieveProgress;
    public float weeklyAchieve = 0;
    public float dailyAchieve = 0;
    public bool[] isWeeklyRecieved = new bool[5];
    public bool[] isDailyRecieved = new bool[5];
    public bool[] dailyCleard = new bool[10];
    [SerializeField] Image[] weeklyClearBg = new Image[5];
    [SerializeField] Image[] dailyClearBg = new Image[5];
    [SerializeField] Quest[] questGOs;
    [SerializeField] RepeatQuest[] repeatGOs;
    [SerializeField] TextMeshProUGUI questNameText;
    [SerializeField] TextMeshProUGUI timerText;

    //�߾� ��������Ʈ
    public RewardPanel rewardPanel;

    //�ϴ� ��ư
    [SerializeField] Button dailyBtn, repeatBtn;
    [SerializeField] GameObject dailySet, repeatSet;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        UpdateQuestUI();
        //ColorBlock cb = dailyBtn.colors;
        //dailyBtn.targetGraphic.color = cb.selectedColor;
    }
    private void Update()
    {
        UpdateTimerUI();
    }
    private void UpdateTimerUI()
    {
        timerText.text = TimeManager.instance.dailyStr;
    }
    //������ �Ѿ�� ����Ʈ �ʱ�ȭ
    public void ResetQuest()
    {
        weeklyAchieve = 0;
        dailyAchieve = 0;
        for (int i = 0; i < 5; i++)
        {
            isWeeklyRecieved[i] = false;
            isDailyRecieved[i] = false;
        }
        for (int i = 0; i < 10; i++) dailyCleard[i] = false;
    }
    public void DailyBtnOnClick()
    {
        SoundManager.instance.BtnClickPlay();
        dailySet.transform.localScale = Vector3.one;
        repeatSet.transform.localScale = Vector3.zero;
        questNameText.text = "���� ����Ʈ";
    }
    public void RepeatBtnOnClick()
    {
        SoundManager.instance.BtnClickPlay();
        dailySet.transform.localScale = Vector3.zero;
        repeatSet.transform.localScale = Vector3.one;
        questNameText.text = "�ݺ� ����Ʈ";
    }
    public void UpdateQuestUI()
    {
        //��������
        dailyAchieve = BackEndGameData.Instance.UserQuestData.dailyClearAmount;
        weeklyAchieve = BackEndGameData.Instance.UserQuestData.weeklyClearAmount;
        for (int i = 0; i < 5; i++)
        {
            isWeeklyRecieved[i] = BackEndGameData.Instance.UserQuestData.weeklyRewardRecieved[i];
            isDailyRecieved[i] = BackEndGameData.Instance.UserQuestData.dailyRewardRecieved[i];
        }
        for (int i = 0; i< 10; i++)
        {
            dailyCleard[i] = BackEndGameData.Instance.UserQuestData.dailyCleared[i];
        }

        //UI ������Ʈ
        weeklyAchieveText.text = weeklyAchieve.ToString();
        dailyAchieveText.text = dailyAchieve.ToString();
        weeklyAchieveProgress.fillAmount = (weeklyAchieve/500);
        dailyAchieveProgress.fillAmount = (dailyAchieve/100);
        
        //���� Ŭ���� ��ư ������Ʈ
        for (int i = 0; i < 5; i++)
        {
            if (!isWeeklyRecieved[i]) weeklyClearBg[i].gameObject.SetActive(false);
            else weeklyClearBg[i].gameObject.SetActive(true);
            if (!isDailyRecieved[i]) dailyClearBg[i].gameObject.SetActive(false);
            else dailyClearBg[i].gameObject.SetActive(true);
        }

        //����Ʈ GO UI������Ʈ
        for (int i =0; i < questGOs.Length; i++)
        {
            questGOs[i].UpdateUI();
        }
        for (int i = 0; i < repeatGOs.Length; i++)
        {
            repeatGOs[i].UpdateUI();
        }

        MainManager.instance.UpdateMainUI();
    }


}
