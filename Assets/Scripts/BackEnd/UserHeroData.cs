[System.Serializable]
public class UserHeroData
{
    public int[] heroLevel = new int[11];
    public int[] heroCount = new int[11];

    public void Reset()
    {
        for (int i = 0; i < heroLevel.Length; i++)
        {
            heroLevel[i] = 0;
            heroCount[i] = 0;
        }
    }


}
