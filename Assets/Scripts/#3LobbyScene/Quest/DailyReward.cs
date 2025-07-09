using UnityEngine;
using TMPro;

public class DailyReward : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI targetPointText;
    [SerializeField] int targetPoint;
    [SerializeField] int gemAmount;
    [SerializeField] int goldAmount;
    [SerializeField] int energyAmonut;

    private void Start()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        targetPointText.text = targetPoint.ToString();
    }
    public void DailyRewardOnClick()
    {
        //���������� ��ġ�� �� �������
        if (BackEndGameData.Instance.UserQuestData.dailyClearAmount >= targetPoint &&
            !BackEndGameData.Instance.UserQuestData.dailyRewardRecieved[(targetPoint / 20) - 1])
        {
            BackEndGameData.Instance.UserQuestData.dailyRewardRecieved[(targetPoint/20)-1] = true;

            //���� ����
            RewardDaily();
            QuestManager.instance.rewardPanel.UpdateRewardUI(goldAmount,gemAmount);
            QuestManager.instance.UpdateQuestUI();
            MainManager.instance.UpdateMainUI();
            BackEndGameData.Instance.GameDataUpdate();
        }
    }
    private void RewardDaily()
    {
        BackEndGameData.Instance.UserGameData.gem += gemAmount;
        BackEndGameData.Instance.UserGameData.gold += goldAmount;
        BackEndGameData.Instance.UserGameData.energy += energyAmonut;

    }

}
