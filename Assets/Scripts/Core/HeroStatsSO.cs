using UnityEngine;

[CreateAssetMenu(fileName = "HeroStats_", menuName = "HackSquad/Hero Stats")]
public class HeroStatsSO : ScriptableObject
{
    public HeroStats stats = HeroStats.Default;
}
