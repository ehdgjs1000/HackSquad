using UnityEngine;

public class QuestReward : MonoBehaviour
{
    [SerializeField] int rewardGemAmount;
    [SerializeField] int rewardGoldAmount;

    public void WeeklyQuestOnClick(int _clearNum)
    {
        //����Ʈ Ŭ���� ������ ���� �������� �ʾ������
        if(QuestManager.instance.weeklyAchieve >= _clearNum &&
            !QuestManager.instance.isWeeklyRecieved[(_clearNum / 100)-1])
        {
            //���� ����
            BackEndGameData.Instance.UserGameData.gem += rewardGemAmount;
            BackEndGameData.Instance.UserGameData.gold += rewardGoldAmount;

            //���� ����
            BackEndGameData.Instance.UserQuestData.weeklyRewardRecieved[(_clearNum / 100) - 1] = true;
            QuestManager.instance.UpdateQuestUI();
            BackEndGameData.Instance.GameDataUpdate();
        }
    }
    public void DailyQuestOnClick(int _clearNum)
    {
        if (QuestManager.instance.dailyAchieve >= _clearNum &&
            !QuestManager.instance.isDailyRecieved[(_clearNum/20)-1])
        {
            BackEndGameData.Instance.UserGameData.gem += rewardGemAmount;
            BackEndGameData.Instance.UserGameData.gold += rewardGoldAmount;

            //���� ����
            BackEndGameData.Instance.UserQuestData.dailyRewardRecieved[(_clearNum / 20) - 1] = true;
            QuestManager.instance.UpdateQuestUI();
            BackEndGameData.Instance.GameDataUpdate();
        }
    }


}
