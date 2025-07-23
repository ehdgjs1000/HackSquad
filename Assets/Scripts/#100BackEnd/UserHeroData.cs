[System.Serializable]
public class UserHeroData
{
    public int[] heroLevel = new int[14];
    public int[] heroCount = new int[14];
    public int[] heroChooseNum = new int[4] {99,99,99,99};

    public void Reset()
    {
        for (int i = 0; i < heroLevel.Length; i++)
        {
            if(i == 3 || i == 6 || i == 9) heroLevel[i] = 1;
            else heroLevel[i] = 0;
            heroCount[i] = 0;

        }
        for (int i = 0; i < heroChooseNum.Length; i++) heroChooseNum[i] = 99;
    }


}
