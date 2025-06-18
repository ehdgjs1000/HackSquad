using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/SkillData")]
public class SkillData : ScriptableObject
{
    [Header("Skill Info")]
    public int ID;
    public string skillName;
    public string skillDescription;
    public Sprite skillImage;
    public int skillLevel;


}
