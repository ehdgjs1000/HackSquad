using UnityEngine;

// 독(Poison) 적용에 필요한 수치를 한 번에 묶은 데이터
// 최초 부여 후 explodeDelay초 뒤, 그동안 누적된 스택(최대 maxStacks)만큼 한 번에 폭발
public struct PoisonApplication
{
    public float damagePerStack;
    public int maxStacks;
    public float explodeDelay;
    public GameObject explosionVfxPrefab;
    public GameObject poisonUiPrefab; // 몬스터 위에 뜨는 스택 UI

    public bool IsActive => maxStacks > 0;
}
