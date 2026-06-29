using UnityEngine;

[CreateAssetMenu(fileName = "HeroData_", menuName = "HackSquad/Hero Data")]
public class HeroDataSO : ScriptableObject
{
    public string heroName;
    public Grade grade;
    public HeroStatsSO statsSO;
}
