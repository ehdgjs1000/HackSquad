using System.Collections;
using UnityEngine;

public class BossMonsterCtrl : MonsterCtrl
{
    protected override IEnumerator Die()
    {
        Debug.Log("BossDie");
        GameManager.instance.IsBossDie();
        GameManager.instance.getGold += gold;
        GameManager.instance.BossLevelUp();
        Destroy(this.gameObject);
        yield return null;
    }

}
