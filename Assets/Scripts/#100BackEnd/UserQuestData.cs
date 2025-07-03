[System.Serializable]
public class UserQuestData
{
    public float dailyClearAmount;
    public float weeklyClearAmount;
    public float[] questProgress = new float[10];
    public bool[] dailyCleared = new bool[10];
    public bool[] dailyRewardRecieved = new bool[5];
    public bool[] weeklyRewardRecieved = new bool[5];

    public void Reset()
    {
        dailyClearAmount = 0;
        weeklyClearAmount = 0;
        for (int i = 0; i< dailyCleared.Length; i++)
        {
            if(i == 0) questProgress[i] = 1;
            else questProgress[i] = 0;
            dailyCleared[i] = false;
        }
        for (int i = 0; i<5; i++)
        {
            dailyRewardRecieved[i] = false;
            weeklyRewardRecieved[i] = false;
        }
    }
    public void ResetDaily()
    {
        dailyClearAmount = 0;
        for (int i = 0; i < dailyCleared.Length; i++)
        {
            if (i == 0) questProgress[i] = 1;
            else questProgress[i] = 0;
            dailyCleared[i] = false;
        }
        for (int i = 0; i < 5; i++)
        {
            dailyRewardRecieved[i] = false;
        }
    }
    public void ResetWeekly()
    {
        weeklyClearAmount = 0;
        for (int i = 0; i < 5; i++)
        {
            weeklyRewardRecieved[i] = false;
        }
    }
}
