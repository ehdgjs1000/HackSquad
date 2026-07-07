using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghillie : Hero
{
    [HideInInspector] public int extraShotCount;

    [Header("스킬 3 최종: 지원사격")]
    public GameObject supportFireBulletPrefab;

    const float OffscreenSpawnDistance = 25f; // 화면 밖으로 취급할 스폰 거리

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

    // 지원사격: 최종 승급 즉시 1회 발사 후, data.loopInterval초마다 재발사
    // 화면 밖 랜덤 위치에서 서로 다른 적 data.bombCount마리를 향해 일자로 날아오는 무한관통 고데미지 탄 발사
    IEnumerator SupportFireLoop(SkillDataSO data)
    {
        while (true)
        {
            FireSupportVolley(data);
            yield return new WaitForSeconds(data.loopInterval);
        }
    }

    void FireSupportVolley(SkillDataSO data)
    {
        if (supportFireBulletPrefab == null) return;

        var monsters = FindObjectsByType<Monster>(FindObjectsSortMode.None);
        if (monsters.Length == 0) return;

        // 서로 다른 적을 무작위로 뽑기 위해 셔플 (Fisher-Yates)
        for (int i = monsters.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (monsters[i], monsters[j]) = (monsters[j], monsters[i]);
        }

        float damage = stats.attackDamage * data.damageMultiplier;
        int shotCount = Mathf.Min(data.bombCount, monsters.Length);

        for (int i = 0; i < shotCount; i++)
        {
            Vector3 targetPos = monsters[i].transform.position;

            Vector3 offDir = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f) * Vector3.forward;
            Vector3 spawnPos = targetPos + offDir * OffscreenSpawnDistance;
            spawnPos.y = targetPos.y;

            Vector3 dir = (targetPos - spawnPos).normalized;

            var go = Instantiate(supportFireBulletPrefab, spawnPos, Quaternion.LookRotation(dir));
            if (go.TryGetComponent(out Bullet bullet))
                bullet.Init(damage, dir, -1, hitVFX); // pierceCount -1 = 무한관통
        }
    }
}
