using UnityEngine;
using UnityEngine.UI;

public class HeroInfo : MonoBehaviour
{
    [SerializeField] Sprite heroProfileImage;
    [SerializeField] int heroGrade; // 0 = normal / 1 = Special / 2 = Epic / 3 = Legendary
    [SerializeField] int heroNum;
    public int heroLevel;
    public int heroCount;
    public string heroJob;

    private void Start()
    {
        UpdateHeroInfo();
    }
    public void UpdateHeroInfo()
    {
        heroLevel = BackEndGameData.Instance.UserHeroData.heroLevel[heroNum];
        heroCount = BackEndGameData.Instance.UserHeroData.heroCount[heroNum];
    }


    public int ReturnHeroCount()
    {
        return heroCount;
    }
    public int ReturnHeroNum()
    {
        return heroNum;
    }
    public int ReturnHeroGrade()
    {
        return heroGrade;
    }
    public int ReturnHeroLevel()
    {
        return heroLevel;
    }
    public Sprite ReturnHeroProfile()
    {
        return heroProfileImage;
    }


}
