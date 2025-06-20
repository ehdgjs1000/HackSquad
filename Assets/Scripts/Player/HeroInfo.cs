using UnityEngine;
using UnityEngine.UI;

public class HeroInfo : MonoBehaviour
{
    [SerializeField] Sprite heroProfileImage;
    [SerializeField] int heroLevel;
    [SerializeField] int heroGrade; // 0 = normal / 1 = Special / 2 = Epic / 3 = Legendary

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
