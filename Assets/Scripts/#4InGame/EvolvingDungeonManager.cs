using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using TMPro;
using DG.Tweening;

public class EvolvingDungeonManager : MonoBehaviour
{
    public static EvolvingDungeonManager instance;
    MonsterSpawner monsterSpawner;

    //Level System
    public int gameLevel;
    float initHp = 0;
    float hp = 100;
    public bool isGameOver = false;
    public int heroCount;
    public float gameSpeed = 1.0f;
    [SerializeField] TextMeshProUGUI gameSpeedText;
    [SerializeField] InGameSetting inGameSetting;
    //UI
    [SerializeField] Image hpImage;
    [SerializeField] GameObject gameOverSet;
    [SerializeField] TextMeshProUGUI goldText;

    [SerializeField] Transform[] heroPos;
    public PlayerCtrl[] players;

    [SerializeField] private GameObject damagePopUpTr;
    [SerializeField] Image gamePlayTimeProgress;
    [SerializeField] TextMeshProUGUI gamePlayTimeText;
    [SerializeField] TextMeshProUGUI gameNameText;
    public int min = 0;
    float sec = 0;
    public int getGold = 0;
    int rebornCount = 0;
    float rebornAmount = 0;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        monsterSpawner = GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawner>();
        string sceneName = SceneManager.GetSceneAt(1).name;
        string numbersOnly = Regex.Replace(sceneName, "[^0-9]", "");
        gameLevel = int.Parse(numbersOnly);
        gameNameText.text = "진화던전" + gameLevel;
        getGold += gameLevel * 1000;
        Debug.Log($"GameLevel : {gameLevel}");
        SetHeros();
        UpdateUI();
    }
    private void Update()
    {
        sec += Time.deltaTime;
        UpdateUI();
        GamePlayProgressUpdate();
        if (min >= 2 && sec >= 0.0f && !isGameOver)
        {
            StartCoroutine(GameWin());
        }
    }
    public void SettingOnClick()
    {
        Debug.Log("SettingOnClick");
        Time.timeScale = 0.0f;
        inGameSetting.gameObject.SetActive(true);
        inGameSetting.UpdateUI();
    }
    public void GameSpeedOnClick()
    {
        if (gameSpeed == 1.0f) gameSpeed = 1.5f;
        else if (gameSpeed == 1.5f && BackEndGameData.Instance.UserCashData.isBuyGameSpeedPackage)
        {
            gameSpeed = 5.0f;
        }
        else if (gameSpeed == 1.5f && !BackEndGameData.Instance.UserCashData.isBuyGameSpeedPackage)
        {
            gameSpeed = 1.0f;
        }
        else if (gameSpeed == 5.0f) gameSpeed = 1.0f;
        GameSpeed(gameSpeed);
    }
    private void GamePlayProgressUpdate()
    {
        if (sec >= 60.0f)
        {
            sec -= 60;
            min++;
        }
        gamePlayTimeProgress.fillAmount = ((sec + min * 60) / 600);
        gamePlayTimeText.text = min.ToString() + ":" + sec.ToString("F0");
    }
    private void Reborn()
    {
        rebornCount--;
        hp = initHp * (rebornAmount / 100);
        UpdateUI();
    }
    private void SetHeros()
    {
        //부활 횟수
        if (BackEndGameData.Instance.UserAbilityData.abilityLevel[11] != 0)
        {
            rebornCount = 1;
            rebornAmount = (BackEndGameData.Instance.UserAbilityData.abilityLevel[11] * 5.0f);
        }
        hp = 100;
        for (int a = 0; a < 4; a++)
        {
            if (ChangeScene.instance.heros[a] != null)
            {
                players[a] = ChangeScene.instance.heros[a].GetComponent<PlayerCtrl>();
                PlayerCtrl hero = Instantiate(players[a], heroPos[a].position, Quaternion.identity);
                players[a] = hero;
                hp += hero.ReturnInitHp();
                heroCount++;
            }
        }
        //Ability 체력 적용
        hp += BackEndGameData.Instance.UserAbilityData.abilityLevel[1] * 2;
        hp += BackEndGameData.Instance.UserAbilityData.abilityLevel[4] * 4;
        hp += BackEndGameData.Instance.UserAbilityData.abilityLevel[7] * 10;

        initHp = hp;
    }
    //게임 시작시 초기 스킬들을 저장해둠

    private void UpdateUI()
    {
        goldText.text = getGold.ToString();
        hpImage.fillAmount = hp / initHp;
    }
    public void GetDamage(float _damage)
    {
        _damage -= (BackEndGameData.Instance.UserAbilityData.abilityLevel[5]) +
            (BackEndGameData.Instance.UserAbilityData.abilityLevel[8] * 3);
        if (_damage + min >= 0)
        {
            hp -= _damage + min;
        }
        else
        {
            hp -= 1;
        }

        damagePopUpTr.GetComponentInChildren<TextMeshPro>().color = Color.red;
        DamagePopUp.Create(new Vector3(transform.position.x,
                    transform.position.y + 2.0f, transform.position.z), _damage, Color.red);
        UpdateUI();
        if (hp <= 0.0f && !isGameOver)
        {
            //부활이 가능할 경우 부활
            if (rebornCount >= 1)
            {
                Reborn();
            }
            else StartCoroutine(GameOver());
        }
    }
    public void HealHP(float increaseAmount)
    {
        initHp += increaseAmount;
        hp += increaseAmount;
        UpdateUI();
    }
    IEnumerator GameWin()
    {
        isGameOver = true;
        gameOverSet.SetActive(true);
        EvolvingDungeonOverManager.instance.isWin = true;

        StartCoroutine(EvolvingDungeonOverManager.instance.GameOver());

        gameOverSet.transform.DOScale(Vector3.one, 0.2f);

        yield return null;
    }
    IEnumerator GameOver()
    {
        isGameOver = true;
        gameOverSet.SetActive(true);

        if (hp <= 0)
        {
            EvolvingDungeonOverManager.instance.isWin = false;
        }
        else
        {
            EvolvingDungeonOverManager.instance.isWin = true;
        }

        StartCoroutine(EvolvingDungeonOverManager.instance.GameOver());

        gameOverSet.transform.DOScale(Vector3.one, 0.2f);

        yield return null;
    }
    public void GameSpeed(float _speed)
    {
        Time.timeScale = _speed;
        gameSpeedText.text = gameSpeed.ToString() + "x";
    }
    public void DieOnClick()
    {
        StartCoroutine(GameOver());
    }

}
