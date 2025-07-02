using UnityEngine;

public class QuestReward : MonoBehaviour
{
    [SerializeField] int rewardGemAmount;
    [SerializeField] int rewardGoldAmount;

    public void WeeklyQuestOnClick(int _clearNum)
    {
        //퀘스트 클리어 보상을 아직 수령하지 않았을경우
        if(QuestManager.instance.weeklyAchieve >= _clearNum &&
            !QuestManager.instance.isWeeklyRecieved[(_clearNum / 100)-1])
        {
            //보상 수령
            BackEndGameData.Instance.UserGameData.gem += rewardGemAmount;
            BackEndGameData.Instance.UserGameData.gold += rewardGoldAmount;

            //서버 연동
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

            //서버 연동
            BackEndGameData.Instance.UserQuestData.dailyRewardRecieved[(_clearNum / 20) - 1] = true;
            QuestManager.instance.UpdateQuestUI();
            BackEndGameData.Instance.GameDataUpdate();
        }
    }


}
