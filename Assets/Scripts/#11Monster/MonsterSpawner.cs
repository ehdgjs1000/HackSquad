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
    bool canSpawn = true;
    float nextSpawnDelay;
    float curSpawnDelay = 0.0f;


    private void Awake()
    {
        spawnList = new List<Spawn>();    
    }
    private void Start()
    {
        ReadSpawnFile();
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
        nextSpawnDelay = Random.Range(2.5f, 3.5f);

        int stage = (GameManager.instance.min/2)+1;
        int spawnMonsterCount = Random.Range(stage, stage+3);
        Debug.Log("Stage : " +stage + "/SpawnMonsterConut : " + spawnMonsterCount);

        //추후 몬스터 index 넣기
        for (int i = 0; i < spawnMonsterCount; i++)
        {
            int randomSapwnPos = Random.Range(0, monsterSpawnPoses.Length);
            Instantiate(monsters[0], monsterSpawnPoses[randomSapwnPos].position, Quaternion.identity);
        }
    }







    /*private void SpawnMonster()
    {
        curSpawnDelay = 0;
        int monsterType = 0; ;
        switch (spawnList[spawnIndex].monsterType)
        {
            case "0":
                monsterType = 0;
                break;
            case "1":
                monsterType = 1;
                break;
            case "2":
                monsterType = 2;
                break;
            case "3":
                monsterType = 3;
                break;
            case "4":
                monsterType = 4;
                break;
            case "5":
                monsterType = 5;
                break;
        }
        Instantiate(monsters[monsterType], monsterSpawnPoses[spawnList[spawnIndex].spawnPos].position, Quaternion.identity);

        spawnIndex++;
        if (spawnIndex == spawnList.Count)
        {
            canSpawn = false;
            return;
        }
        nextSpawnDelay = spawnList[spawnIndex].sapwnDelay;
    }*/
    private void ReadSpawnFile()
    {
        spawnList.Clear();
        spawnIndex = 0;
        canSpawn = false;

        //씬 이름의 Text파일을 폴더에서 읽어옴
        //TextAsset textFile = Resources.Load(SceneManager.GetActiveScene().name) as TextAsset;
        TextAsset textFile = Resources.Load("Chapter1") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
            if (line == null) break;

            Spawn spawnData = new Spawn();
            spawnData.sapwnDelay = float.Parse(line.Split(',')[0]);
            spawnData.monsterType = line.Split(',')[1];
            spawnData.spawnPos = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        stringReader.Close();
        nextSpawnDelay = spawnList[0].sapwnDelay;
        canSpawn = true;
    }

}
