using UnityEngine;
using UnityEngine.UI;

public class HeroInfo : MonoBehaviour
{
    [SerializeField] Sprite heroProfileImage;
    [SerializeField] int heroLevel;
    [SerializeField] int heroGrade; // 0 = normal / 1 = Special / 2 = Epic / 3 = Legendary
    [SerializeField] int heroNum;

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
