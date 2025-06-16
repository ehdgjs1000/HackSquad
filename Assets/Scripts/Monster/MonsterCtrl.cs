using UnityEngine;

public class MonsterCtrl : MonoBehaviour
{

    /// <summary>
    /// Fight System
    /// </summary>
    protected bool isFightMode = false;
    [SerializeField] float fightModeRange;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask monsterLayer;
    Collider[] playerColl = new Collider[3];
    Collider[] monsterColls = new Collider[5];

    private void Update()
    {
        if(!PlayerCtrl.instance.isFightMode) CheckPlayer();
        

    }
    private void CheckPlayer()
    {
        //playerColl = null;
        int isPlayerDetected = Physics.OverlapSphereNonAlloc(this.transform.position, 
            fightModeRange, playerColl, playerLayer);
        print(isPlayerDetected);
        if (isPlayerDetected > 0)
        {
            // todo : FightMode 시작
            CheckMonster();
            PlayerCtrl.instance.isFightMode = true;
            BattleGround.instance.StartFight();
        }
    }
    // 주면 몬스터 탐색 및 Collider 저장
    private void CheckMonster()
    {
        monsterColls = null;
        monsterColls = Physics.OverlapSphere(this.transform.position, fightModeRange,
            monsterLayer);

        foreach (Collider monsters in monsterColls)
        {
            monsters.GetComponent<MonsterCtrl>().isFightMode = true;
        }
    }
    

}
