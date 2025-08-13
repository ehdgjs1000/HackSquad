using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    public GameObject[] bulletPrefabs;
    public GameObject monster1Prefab;
    public GameObject monster2Prefab;
    public GameObject monster3Prefab;
    public GameObject monster4Prefab;
    public GameObject monster5Prefab;

    GameObject[] alienBullets;
    GameObject[] bazookaBullets;
    GameObject[] cowboyBullets;
    GameObject[] hoodieBullets;
    GameObject[] icemanBullets;
    GameObject[] jesterBullets;
    GameObject[] pistolBullets;
    GameObject[] rifleBullets;
    GameObject[] samuraiBullets;
    GameObject[] sniperBullets;
    GameObject[] veteranBullets;
    GameObject[] robotBullets;
    GameObject[] ninjaBullet;
    GameObject[] knightBullet;
    GameObject[] terroristBullet;
    GameObject[] thunderBullet;

    GameObject[] monster1;
    GameObject[] monster2;
    GameObject[] monster3;
    GameObject[] monster4;
    GameObject[] monster5;

    GameObject[] targetPool;

    private void Awake()
    {
        if (instance == null) instance = this;

        alienBullets = new GameObject[50];
        bazookaBullets = new GameObject[10];
        cowboyBullets = new GameObject[100];
        hoodieBullets = new GameObject[50];
        icemanBullets = new GameObject[10];
        jesterBullets = new GameObject[30];
        pistolBullets = new GameObject[20];
        rifleBullets = new GameObject[10];
        samuraiBullets = new GameObject[20];
        sniperBullets = new GameObject[10];
        veteranBullets = new GameObject[100];
        robotBullets = new GameObject[50];
        ninjaBullet = new GameObject[20];
        knightBullet = new GameObject[50];
        terroristBullet = new GameObject[20];
        thunderBullet = new GameObject[30];

        monster1 = new GameObject[30];
        monster2 = new GameObject[30];
        monster3 = new GameObject[30];
        monster4 = new GameObject[30];
        monster5 = new GameObject[30];

        Generate();
    }
    private void Generate()
    {
        int bulletIndex = 0;
        for (int index = 0; index < alienBullets.Length; index++)
        {
            alienBullets[index] = Instantiate(bulletPrefabs[bulletIndex]);
            alienBullets[index].SetActive(false);
        }
        bulletIndex++;
        for (int index = 0; index < bazookaBullets.Length; index++)
        {
            bazookaBullets[index] = Instantiate(bulletPrefabs[bulletIndex]);
            bazookaBullets[index].SetActive(false);
        }
        bulletIndex++;
        for (int index = 0; index < cowboyBullets.Length; index++)
        {
            cowboyBullets[index] = Instantiate(bulletPrefabs[bulletIndex]);
            cowboyBullets[index].SetActive(false);
        }
        bulletIndex++;
        for (int index = 0; index < hoodieBullets.Length; index++)
        {
            hoodieBullets[index] = Instantiate(bulletPrefabs[bulletIndex]);
            hoodieBullets[index].SetActive(false);
        }
        bulletIndex++;
        for (int index = 0; index < icemanBullets.Length; index++)
        {
            icemanBullets[index] = Instantiate(bulletPrefabs[bulletIndex]);
            icemanBullets[index].SetActive(false);
        }
        bulletIndex++;
        for (int index = 0; index < jesterBullets.Length; index++)
        {
            jesterBullets[index] = Instantiate(bulletPrefabs[bulletIndex]);
            jesterBullets[index].SetActive(false);
        }
        bulletIndex++;
        for (int index = 0; index < pistolBullets.Length; index++)
        {
            pistolBullets[index] = Instantiate(bulletPrefabs[bulletIndex]);
            pistolBullets[index].SetActive(false);
        }
        bulletIndex++;
        for (int index = 0; index < rifleBullets.Length; index++)
        {
            rifleBullets[index] = Instantiate(bulletPrefabs[bulletIndex]);
            rifleBullets[index].SetActive(false);
        }
        bulletIndex++;
        for (int index = 0; index < samuraiBullets.Length; index++)
        {
            samuraiBullets[index] = Instantiate(bulletPrefabs[bulletIndex]);
            samuraiBullets[index].SetActive(false);
        }
        bulletIndex++;
        for (int index = 0; index < sniperBullets.Length; index++)
        {
            sniperBullets[index] = Instantiate(bulletPrefabs[bulletIndex]);
            sniperBullets[index].SetActive(false);
        }
        bulletIndex++;
        for (int index = 0; index < veteranBullets.Length; index++)
        {
            veteranBullets[index] = Instantiate(bulletPrefabs[bulletIndex]);
            veteranBullets[index].SetActive(false);
        }
        bulletIndex++;
        for (int index = 0; index < robotBullets.Length; index++)
        {
            robotBullets[index] = Instantiate(bulletPrefabs[bulletIndex]);
            robotBullets[index].SetActive(false);
        }
        bulletIndex++;
        for (int index = 0; index < ninjaBullet.Length; index++)
        {
            ninjaBullet[index] = Instantiate(bulletPrefabs[bulletIndex]);
            ninjaBullet[index].SetActive(false);
        }
        bulletIndex++;
        for (int index = 0; index < knightBullet.Length; index++)
        {
            knightBullet[index] = Instantiate(bulletPrefabs[bulletIndex]);
            knightBullet[index].SetActive(false);
        }
        bulletIndex++;
        for (int index = 0; index < terroristBullet.Length; index++)
        {
            terroristBullet[index] = Instantiate(bulletPrefabs[bulletIndex]);
            terroristBullet[index].SetActive(false);
        }
        bulletIndex++;
        for (int index = 0; index < thunderBullet.Length; index++)
        {
            thunderBullet[index] = Instantiate(bulletPrefabs[bulletIndex]);
            thunderBullet[index].SetActive(false);
        }

        //monster pool
        for (int index = 0; index < monster1.Length; index++)
        {
            monster1[index] = Instantiate(monster1Prefab);
            monster1[index].SetActive(false);
        }
        for (int index = 0; index < monster2.Length; index++)
        {
            monster2[index] = Instantiate(monster2Prefab);
            monster2[index].SetActive(false);
        }
        for (int index = 0; index < monster3.Length; index++)
        {
            monster3[index] = Instantiate(monster3Prefab);
            monster3[index].SetActive(false);
        }
        for (int index = 0; index < monster4.Length; index++)
        {
            monster4[index] = Instantiate(monster4Prefab);
            monster4[index].SetActive(false);
        }
        for (int index = 0; index < monster5.Length; index++)
        {
            monster5[index] = Instantiate(monster5Prefab);
            monster5[index].SetActive(false);
        }
    }
    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "alienBullet":
                targetPool = alienBullets;
                break;
            case "bazookaBullet":
                targetPool = bazookaBullets;
                break;
            case "cowboyBullet":
                targetPool = cowboyBullets;
                break;
            case "hoodieBullet":
                targetPool = hoodieBullets;
                break;
            case "icemanBullet":
                targetPool = icemanBullets;
                break;
            case "jesterBullet":
                targetPool = jesterBullets;
                break;
            case "pistolBullet":
                targetPool = pistolBullets;
                break;
            case "rifleBullet":
                targetPool = rifleBullets;
                break;
            case "samuraiBullet":
                targetPool = samuraiBullets;
                break;
            case "sniperBullet":
                targetPool = sniperBullets;
                break;
            case "veteranBullet":
                targetPool = veteranBullets;
                break;
            case "robotBullet":
                targetPool = robotBullets;
                break;
            case "ninjaBullet":
                targetPool = ninjaBullet;
                break;
            case "knightBullet":
                targetPool = knightBullet;
                break;
            case "terroristBullet":
                targetPool = terroristBullet;
                break;
            case "thunderBullet":
                targetPool = thunderBullet;
                break;
            case "monster1":
                targetPool = monster1;
                break;
            case "monster2":
                targetPool = monster2;
                break;
            case "monster3":
                targetPool = monster3;
                break;
            case "monster4":
                targetPool = monster4;
                break;
            case "monster5":
                targetPool = monster5;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }
        return null;
    }
    public IEnumerator DeActive(float time, GameObject go)
    {
        yield return new WaitForSeconds(time);
        go.transform.position = Vector3.zero;
        go.gameObject.SetActive(false);
    }
}
