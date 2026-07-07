using UnityEngine;

// 스킬 밸런스 수치 데이터 — 스킬 클래스의 로직(어떤 스탯을 어떻게 바꾸는지)은 코드에 남기고,
// 크기(얼마나)만 여기서 인스펙터로 튜닝한다. 필드 의미는 각 스킬 클래스의 주석 참고.
[CreateAssetMenu(fileName = "SkillData_", menuName = "HackSquad/Skill Data")]
public class SkillDataSO : ScriptableObject
{
    [Header("Text")]
    public string skillName;
    [TextArea] public string initDescription;
    [TextArea] public string description;
    [TextArea] public string finalDescription;

    [Header("Equip / Upgrade / Finalize 수치")]
    public float equipValue;
    public float upgradeValue;
    public float finalValue;

    [Header("최종 스킬 반복 루프 튜닝 (해당 스킬만 사용)")]
    public float loopInterval;
    public float warningDuration;
    public float radiusMultiplier;
    public float damageMultiplier;
    public float lifetime;
    public float spawnRange;
    public int bombCount;
    public float bombSpacing;
    public float bombDelay;
}
