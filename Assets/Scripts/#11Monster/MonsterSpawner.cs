using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] monsters;
    [SerializeField] Transform[] monsterSpawnPoses;


    List<Spawn> spawnList;
    int spawnIndex = 0;
    int spawnPosNum = 0;
    public bool canSpawn = true;
    float nextSpawnDelay;
    float curSpawnDelay = 0.0f;
    

    private void Awake()
    {
        spawnList = new List<Spawn>();    
    }
    private void Update()
    {
        curSpawnDelay += Time.deltaTime;
        if(curSpawnDelay >= nextSpawnDelay && canSpawn)
        {
            SpawnMonster();
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 12);

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
            Instantiate(monsters[0], monsterSpawnPoses[randomSapwnPos].position, Quaternion.identity);
        }
    }

}
