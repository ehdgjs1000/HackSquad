using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/SkillData")]
public class SkillData : ScriptableObject
{
    [Header("Skill Info")]
    public int ID;
    public string skillName;
    public string skillDescription;
    public Sprite skillImage;
    public Sprite skillCharacterImage;
    public int skillLevel;
    public int skillGrade;
}
