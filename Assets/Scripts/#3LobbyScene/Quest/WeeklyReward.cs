using UnityEngine;
using TMPro;

public class WeeklyReward : MonoBehaviour
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
    public void WeeklyRewardOnClick()
    {
        //���������� ��ġ�� �� �������
        if (BackEndGameData.Instance.UserQuestData.weeklyClearAmount >= targetPoint &&
            !BackEndGameData.Instance.UserQuestData.weeklyRewardRecieved[(targetPoint / 100) - 1])
        {
            BackEndGameData.Instance.UserQuestData.weeklyRewardRecieved[(targetPoint / 100) - 1] = true;

            //���� ����
            RewardDaily();

            QuestManager.instance.rewardPanel.UpdateRewardUI(goldAmount, gemAmount);
            MainManager.instance.UpdateMainUI();
            QuestManager.instance.UpdateQuestUI();
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
