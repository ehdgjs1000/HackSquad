[System.Serializable]
public class UserAbilityData
{
    public int[] abilityLevel = new int[12];

    public void Reset()
    {
        for (int i = 0; i < abilityLevel.Length; i++)
        {
            abilityLevel[i] = 0;
        }

    }
}
