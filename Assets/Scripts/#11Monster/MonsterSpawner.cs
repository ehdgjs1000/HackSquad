using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] monsters;
    [SerializeField] Transform[] monsterSpawnPoses;
    [SerializeField] GameObject[] bossGO;
    int bossLevel = 0;


    List<Spawn> spawnList;
    int spawnIndex = 0;
    int spawnPosNum = 0;
    public bool canSpawn = true;
    float nextSpawnDelay;
    float curSpawnDelay = 0.0f;
    

    private void Awake()
    {
        spawnList = new List<Spawn>();
        bossLevel = 0;
    }
    private void Update()
    {
        curSpawnDelay += Time.deltaTime;
        if(curSpawnDelay >= nextSpawnDelay && canSpawn)
        {
            SpawnMonster();
        }
        
    }
    public void SpawnBoss()
    {
        GameObject monster = Instantiate(bossGO[bossLevel], monsterSpawnPoses[8].position,Quaternion.identity);
        bossLevel++;
    }
    private void SpawnMonster()
    {
        curSpawnDelay = 0;
        nextSpawnDelay = Random.Range(2f, 3f);

        int stage = GameManager.instance.min;
        int spawnMonsterCount = Random.Range(stage+3, stage+5);

        //추후 몬스터 index 넣기
        for (int i = 0; i < spawnMonsterCount; i++)
        {
            int randomSapwnPos = Random.Range(0, monsterSpawnPoses.Length);
            int ranMonster = Random.Range(0,5);
            string monsterType = "monster" + ranMonster;
            
            //몬스터 Pool 재사용
            GameObject monster = PoolManager.instance.MakeObj("monster1");
            monster.transform.position = monsterSpawnPoses[randomSapwnPos].position;
            monster.GetComponent<MonsterCtrl>().InitMonster();

            Instantiate(monsters[0].GetComponent<MonsterCtrl>().summonVFX,
                new Vector3(monsterSpawnPoses[randomSapwnPos].position.x, monsterSpawnPoses[randomSapwnPos].position.y + 0.2f, monsterSpawnPoses[randomSapwnPos].position.z),
                Quaternion.identity);
        }
    }

}
