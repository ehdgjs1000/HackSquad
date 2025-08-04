using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public static ChangeScene instance;
    public string chapterName;
    public GameObject[] heros = new GameObject[4];
    public int gameLevel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void SetHeros(int _gameLevel)
    {
        gameLevel = _gameLevel;
        for (int a = 0; a < heros.Length; a++)
        {
            heros[a] = HeroSetManager.instance.ReturnHeroInfo(a).GetComponent<SquadHero>().hero;
        }
    }

}
