
[System.Serializable]
public class UserCashData
{
    public bool isBuyFirstPackage;
    public bool isBuyGameSpeedPackage;

    public void Reset()
    {
        isBuyFirstPackage = false;
        isBuyGameSpeedPackage = false;
    }
}
