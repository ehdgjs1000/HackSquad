using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] monsters;
    [SerializeField] Transform[] monsterSpawnPoses;

    List<Spawn> spawnList;
    int spawnIndex = 0;
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
    private void SpawnMonster()
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
        Instantiate(monsters[monsterType], monsterSpawnPoses[spawnIndex].position, Quaternion.identity);

        spawnIndex++;
        if (spawnIndex == spawnList.Count)
        {
            canSpawn = false;
            return;
        }
        nextSpawnDelay = spawnList[spawnIndex].sapwnDelay;
    }
    private void ReadSpawnFile()
    {
        spawnList.Clear();
        spawnIndex = 0;
        canSpawn = false;

        //추후 chpater이름 바꾸기 동적으로
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
