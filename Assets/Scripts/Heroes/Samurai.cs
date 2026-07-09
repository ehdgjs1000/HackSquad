using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사무라이 — 관통형 화염구체를 발사하는 히어로. 명중 시 화상 적용 (Hero.md 6번 항목)
// 화상은 1틱=1초(burnMaxTicks만큼 지속). 기본 공격의 관통은 stats.pierceCount(HeroStatsSO)로 설정
public class Samurai : Hero
{
    [Header("화상 (기본 공격 명중 시 적용, 피해는 그 발의 데미지 대비 비율)")]
    public float burnDamagePerTickRatio = 0.2f; // 틱당 화상 피해 = 명중 데미지 * 비율
    public int burnMaxTicks = 3;                // 화상 최대 지속 틱(1틱=1초)

    [Header("스킬 1: 고온연소 (화상 지속시간/피해 증가)")]
    public int burnMaxTicksIncreasePerLevel = 1;         // 레벨당 화상 최대 지속 틱 증가량
    public float burnDamageIncreasePerLevel = 0.05f;     // 레벨당 화상 피해 비율 증가량

    [Header("스킬 1 최종: 메테오 (호출 시 하늘에서 낙하 후 착지 지점에 광역 피해)")]
    public GameObject meteorVfxPrefab;          // 낙하하는 메테오 본체 VFX
    public GameObject meteorExplosionVfxPrefab; // 착지 시 폭발 VFX
    public float meteorLoopInterval = 10f;      // 호출 주기(초)
    public float meteorFallHeight = 15f;        // 낙하 시작 높이
    public float meteorFallDuration = 0.8f;     // 낙하에 걸리는 시간(초)
    public float meteorDamageRatio = 3f;        // 착지 피해 = attackDamage * 비율
    public float meteorRadius = 3f;

    [Header("스킬 2: 화염구체 강화 (크기/데미지 증가)")]
    public float fireballSizeIncreasePerLevel = 0.15f;   // 레벨당 총알 크기 증가율(0.15=15%)
    public float fireballDamageIncreasePerLevel = 0.1f;  // 레벨당 데미지 증가율(0.1=10%)
    [HideInInspector] public float bulletScaleMultiplier = 1f;

    [Header("스킬 2 최종: 슬래시 (n초마다 무작위 3~6마리에게 즉시 피해)")]
    public GameObject slashVfxPrefab;
    public float slashLoopInterval = 6f;   // 호출 주기(초)
    public int slashMinTargets = 3;
    public int slashMaxTargets = 6;
    public float slashDamageRatio = 0.8f;  // 피해 = attackDamage * 비율

    [Header("스킬 3: 잿불 (화상 지속시간 증가)")]
    public int ashMaxTicksIncreasePerLevel = 1; // 레벨당 화상 최대 지속 틱 증가량

    [Header("스킬 3 최종: 연소폭발 (화상 n틱마다 큰 데미지)")]
    public GameObject combustionExplosionVfxPrefab;
    public int burnExplodeEveryTicks = 3;         // 몇 틱마다 연소폭발이 발생하는지
    public float burnExplosionDamageRatio = 1.5f; // 폭발 피해 = 명중 데미지 * 비율
    public float burnExplosionRadius = 2.5f;
    [HideInInspector] public bool hasCombustionExplosion;

    [Header("Skill Data")]
    public SkillDataSO burnSkillData;
    public SkillDataSO fireballSkillData;
    public SkillDataSO ashSkillData;

    protected override void Init()
    {
        attackBehavior = new AutoAttackBehavior();
    }

    public override List<SkillBase> GetSkillCandidates() => new()
    {
        new SamuraiBurnSkill(burnSkillData),
        new SamuraiFireballSkill(fireballSkillData),
        new SamuraiAshSkill(ashSkillData)
    };

    public void StartMeteorLoop() => StartCoroutine(MeteorLoop());
    public void StartSlashLoop() => StartCoroutine(SlashLoop());

    // 메테오: 호출 즉시 낙하 후, meteorLoopInterval초마다 재호출
    IEnumerator MeteorLoop()
    {
        while (true)
        {
            CastMeteor();
            yield return new WaitForSeconds(meteorLoopInterval);
        }
    }

    void CastMeteor()
    {
        var monsters = FindObjectsByType<Monster>(FindObjectsSortMode.None);
        Vector3 pos = monsters.Length > 0
            ? monsters[Random.Range(0, monsters.Length)].transform.position
            : transform.position;
        pos.y = 0f;

        Meteor.Spawn(meteorVfxPrefab, meteorExplosionVfxPrefab, pos,
            meteorFallHeight, meteorFallDuration, stats.attackDamage * meteorDamageRatio, meteorRadius);
    }

    // 슬래시: 호출 즉시 발동 후, slashLoopInterval초마다 재호출
    IEnumerator SlashLoop()
    {
        while (true)
        {
            CastSlash();
            yield return new WaitForSeconds(slashLoopInterval);
        }
    }

    void CastSlash()
    {
        var monsters = FindObjectsByType<Monster>(FindObjectsSortMode.None);
        if (monsters.Length == 0) return;

        // 서로 다른 적을 무작위로 뽑기 위해 셔플 (Fisher-Yates)
        for (int i = monsters.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (monsters[i], monsters[j]) = (monsters[j], monsters[i]);
        }

        int targetCount = Mathf.Min(Random.Range(slashMinTargets, slashMaxTargets + 1), monsters.Length);
        float damage = stats.attackDamage * slashDamageRatio;

        for (int i = 0; i < targetCount; i++)
        {
            Monster m = monsters[i];

            if (slashVfxPrefab != null)
            {
                var vfx = Instantiate(slashVfxPrefab, m.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
                if (!vfx.TryGetComponent<ParticleSystem>(out var ps))
                    Destroy(vfx, 3f);
                else if (ps.main.stopAction != ParticleSystemStopAction.Destroy)
                    Destroy(vfx, ps.main.duration + ps.main.startLifetime.constantMax);
            }

            m.TakeDamage(damage);
        }
    }
}
