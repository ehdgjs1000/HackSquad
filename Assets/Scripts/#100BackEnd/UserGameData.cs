
[System.Serializable]
public class UserGameData
{
    public int level;
    public float exp;
    public int gold;
    public int gem;
    public int energy;
    public float highestChapter;
    public float highestRewardChapter;

    public void Reset()
    {
        level = 1;
        exp = 0;
        gold = 1000;
        gem = 300;
        energy = 30;
        highestChapter = 0;
        highestRewardChapter = -1;
    }


}
