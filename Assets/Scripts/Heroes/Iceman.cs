using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceman : Hero
{
    [Header("얼음 장판")]
    public GameObject iceVfxPrefab;
    [HideInInspector] public float slowMultiplier = 0.5f; // 장판 위 이동속도 배율 (1=정상, 0=완전정지)
    [HideInInspector] public float iceDuration = 3f;       // 장판 유지시간(초)
    [HideInInspector] public bool hasIcicleBurst;          // 스킬 3 최종: 장판 생성 시 고드름 4방향 발사 여부

    [Header("스킬 2 최종: 연쇄 빙결(토네이도)")]
    public GameObject tornadoVfxPrefab;

    [Header("스킬 3 최종: 결빙 폭풍(고드름)")]
    public GameObject icicleVfxPrefab;

    [Header("Skill Data")]
    public SkillDataSO chillSkillData;
    public SkillDataSO radiusSkillData;
    public SkillDataSO shatterSkillData;

    protected override void Init()
    {
        attackBehavior = new IceAttackBehavior();
    }

    public override List<SkillBase> GetSkillCandidates() => new()
    {
        new IcemanChillSkill(chillSkillData),
        new IcemanRadiusSkill(radiusSkillData),
        new IcemanShatterSkill(shatterSkillData)
    };

    public void StartTornadoLoop(SkillDataSO data) => StartCoroutine(TornadoLoop(data));

    // 연쇄 빙결: 승급 즉시 토네이도 생성 후, data.loopInterval초 쿨마다 재생성 (공격과 무관한 독립 루프)
    IEnumerator TornadoLoop(SkillDataSO data)
    {
        while (true)
        {
            SpawnTornado(data);
            yield return new WaitForSeconds(data.loopInterval);
        }
    }

    void SpawnTornado(SkillDataSO data)
    {
        var monsters = FindObjectsByType<Monster>(FindObjectsSortMode.None);
        Vector3 pos = monsters.Length > 0
            ? monsters[Random.Range(0, monsters.Length)].transform.position
            : transform.position;
        pos.y = 0f;

        Tornado.Spawn(tornadoVfxPrefab, pos, stats.attackDamage, data.lifetime);
    }
}
