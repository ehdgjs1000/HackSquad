using UnityEngine;

public class GoldMonsterSpawner : MonoBehaviour
{
    //[SerializeField] GameObject[] monsters;
    [SerializeField] Transform[] monsterSpawnPoses;
    [SerializeField] AudioClip gameBGM;
    public bool canSpawn = true;
    float nextSpawnDelay;
    float curSpawnDelay = 0.0f;

    private void Start()
    {
        SoundManager.instance.PlayBGM(gameBGM);
    }
    private void Update()
    {
        curSpawnDelay += Time.deltaTime;
        if (curSpawnDelay >= nextSpawnDelay && canSpawn)
        {
            SpawnMonster();
        }

    }
    private void SpawnMonster()
    {
        curSpawnDelay = 0;
        nextSpawnDelay = Random.Range(2f, 3f);

        int spawnMonsterCount = Random.Range(7, 10);

        //추후 몬스터 index 넣기
        for (int i = 0; i < spawnMonsterCount; i++)
        {
            int randomSapwnPos = Random.Range(0, monsterSpawnPoses.Length - 1);

            GameObject monster = PoolManager.instance.MakeObj("monster1");
            monster.transform.position = monsterSpawnPoses[randomSapwnPos].position;
            monster.GetComponent<GoldMonsterCtrl>().InitMonster();
            monster.GetComponent<GoldMonsterCtrl>().DoFade();

            Instantiate(monster.GetComponent<GoldMonsterCtrl>().summonVFX,
                new Vector3(monsterSpawnPoses[randomSapwnPos].position.x, monsterSpawnPoses[randomSapwnPos].position.y + 0.2f, monsterSpawnPoses[randomSapwnPos].position.z),
                Quaternion.identity);
        }
    }
}
