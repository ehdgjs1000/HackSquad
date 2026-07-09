using UnityEngine;

// 스킬 설명 텍스트 데이터 — 밸런스 수치는 각 히어로 클래스에 의미 있는 이름의 필드로 직접 선언한다.
[CreateAssetMenu(fileName = "SkillData_", menuName = "HackSquad/Skill Data")]
public class SkillDataSO : ScriptableObject
{
    public string skillName;
    [TextArea] public string initDescription;
    [TextArea] public string description;
    [TextArea] public string finalDescription;
}
