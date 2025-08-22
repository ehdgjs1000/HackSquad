[System.Serializable]
public class UserInvenData
{
    public int juiceLevel1ItemCount;
    public int juiceLevel2ItemCount;
    public int juiceLevel3ItemCount;
    public int juiceLevel4ItemCount;

    public void Reset()
    {
        juiceLevel1ItemCount = 1;
        juiceLevel2ItemCount = 1;
        juiceLevel3ItemCount = 1;
        juiceLevel4ItemCount = 1;

    }

}
