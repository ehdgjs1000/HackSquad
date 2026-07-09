// 스킬 3: 관통 강화 — 장착 시 활성화, 총알이 관통할 때마다 데미지가 누적 증가
// 증가율은 MaskHero.damageIncreasePerPierce (인스펙터에서 조절)
// 최종: 이온폭풍 — 승급 즉시 생성되고, 이후 Data.loopInterval초 쿨마다 재생성(지속시간 Data.lifetime)
public class MaskPierceDamageSkill : SkillBase
{
    public MaskPierceDamageSkill(SkillDataSO data) : base(data) { }

    public override void OnEquip(Hero hero)
    {
        base.OnEquip(hero);
        if (hero is MaskHero mask)
            mask.hasPierceDamageBonus = true;
    }

    protected override void OnFinalize()
    {
        if (Owner is MaskHero mask)
            mask.StartIonStormLoop(Data);
    }
}
