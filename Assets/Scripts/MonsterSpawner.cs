using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public float spawnRadius = 12f;
    public float spawnInterval = 2f;
    public bool spawning = true;

    void Start() => StartCoroutine(SpawnLoop());

IEnumerator SpawnLoop()
    {
        while (spawning)
        {
            SpawnOne();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnOne()
    {
        if (monsterPrefab == null) return;
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 pos = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * spawnRadius;
        Instantiate(monsterPrefab, pos, Quaternion.identity);
    }

    public void StopSpawning() => spawning = false;
}