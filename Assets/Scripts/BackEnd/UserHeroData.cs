[System.Serializable]
public class UserHeroData
{
    public int[] heroLevel = new int[11];
    public int[] heroCount = new int[11];
    public int[] heroChooseNum = new int[4] {99,99,99,99};

    public void Reset()
    {
        for (int i = 0; i < heroLevel.Length; i++)
        {
            heroLevel[i] = 0;
            heroCount[i] = 0;
        }
        foreach (int i in heroChooseNum) heroChooseNum[i] = 99;
    }


}
