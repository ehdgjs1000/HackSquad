
[System.Serializable]
public class UserEvolvingData
{
    public int[] evolvingLevel = new int[16];

    public void Reset()
    {
        for (int i = 0; i < evolvingLevel.Length; i++)
        {
            evolvingLevel[i] = 0;
        }
    }
}
