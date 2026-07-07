using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghillie : Hero
{
    [HideInInspector] public int extraShotCount;

    [Header("Skill Data")]
    public SkillDataSO pierceSkillData;
    public SkillDataSO headshotSkillData;
    public SkillDataSO weakpointSkillData;

    protected override void Init()
    {
        attackBehavior = new AutoAttackBehavior();
    }

    public override List<SkillBase> GetSkillCandidates() => new()
    {
        new GhilliePierceSkill(pierceSkillData),
        new GhillieHeadshotSkill(headshotSkillData),
        new GhillieWeakpointSkill(weakpointSkillData)
    };

    public void StartSupportFireLoop(SkillDataSO data) => StartCoroutine(SupportFireLoop(data));

    // 지원사격: data.loopInterval초마다 무작위 적에게 강력한 단발 공격
    IEnumerator SupportFireLoop(SkillDataSO data)
    {
        while (true)
        {
            yield return new WaitForSeconds(data.loopInterval);

            var monsters = FindObjectsByType<Monster>(FindObjectsSortMode.None);
            if (monsters.Length == 0) continue;

            var target = monsters[Random.Range(0, monsters.Length)];
            target.TakeDamage(stats.attackDamage * data.damageMultiplier);
        }
    }
}
