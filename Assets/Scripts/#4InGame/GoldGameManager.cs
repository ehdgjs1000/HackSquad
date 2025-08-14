using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GoldGameManager : MonoBehaviour
{
#if UNITY_EDITOR
    float highestSpeed = 5.0f;
#else
    float highestSpeed = 2.0f;
#endif
    public static GoldGameManager instance;
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
    [SerializeField] Image expImage;
    [SerializeField] GameObject gameOverSet;
    [SerializeField] TextMeshProUGUI goldText;
    //In Game Data 
    [SerializeField] Skill[] upgradeSkills;
    //[SerializeField] GameObject[] heroGos;
    [SerializeField] Transform[] heroPos;
    public PlayerCtrl[] players;
    public bool isBossMode = false;

    [SerializeField] private GameObject damagePopUpTr;
    [SerializeField] Image gamePlayTimeProgress;
    [SerializeField] TextMeshProUGUI gamePlayTimeText;
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
        gameLevel = ChangeScene.instance.gameLevel;
        SetHeros();
        UpdateUI();
        InitSkillInfo();
    }
    private void Update()
    {
        if (!isBossMode)
        {
            sec += Time.deltaTime;
            GamePlayProgressUpdate();
            if(min >= 10 && sec >= 1.0f)
            {
                StartCoroutine(GameWin());
            }
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
            gameSpeed = highestSpeed;
        }
        else if (gameSpeed == 1.5f && !BackEndGameData.Instance.UserCashData.isBuyGameSpeedPackage)
        {
            gameSpeed = 1.0f;
        }
        else if (gameSpeed == highestSpeed) gameSpeed = 1.0f;
        GameSpeed(gameSpeed);
    }
    public void IsBossDie()
    {
        isBossMode = false;
        monsterSpawner.canSpawn = true;
        sec += 3.0f;
    }
    private void GamePlayProgressUpdate()
    {
        if (sec >= 60.0f)
        {
            sec -= 60;
            min++;
        }
        gamePlayTimeProgress.fillAmount = ((sec + min*60) / 600);
        gamePlayTimeText.text = min.ToString() + ":" + sec.ToString("F0");

        //5분 10분 보스 스폰
        if(min == 5)
        {
            //게임 오버

        }


    }
    private void Reborn()
    {
        rebornCount--;
        hp = initHp * (rebornAmount/100);
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
    private void InitSkillInfo() 
    {
        int skillCount = 0;
        for (int a = 0; a < players.Length; a++)
        {
            if (players[a] != null)
            {
                for (int b = 0; b < 3; b++)
                {
                    SkillManager.instance.skillDatas[skillCount] = players[a].characterSkills[b];
                    skillCount++;
                }
                SkillManager.instance.finalSkill[a] = players[a].finalSkill;
            }
            
        }
        ResetSkillLevel();
    }
    private void UpdateUI()
    {
        goldText.text = getGold.ToString();
        hpImage.fillAmount = hp / initHp;
    }
    public void GetDamage(float _damage)
    {
        _damage -= (BackEndGameData.Instance.UserAbilityData.abilityLevel[5]) +
            (BackEndGameData.Instance.UserAbilityData.abilityLevel[8] * 3);
        if(_damage  + min>= 0)
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
            if(rebornCount >= 1)
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
        ResetSkillLevel();
        gameOverSet.SetActive(true);
        GameOverManager.instance.isWin = true;

        StartCoroutine(GameOverManager.instance.GameOver());

        gameOverSet.transform.DOScale(Vector3.one, 0.2f);

        yield return null;
    }
    IEnumerator GameOver()
    {
        isGameOver = true;
        ResetSkillLevel();
        gameOverSet.SetActive(true);

        if (hp <= 0)
        {
            GameOverManager.instance.isWin = false;
        }
        else
        {
            GameOverManager.instance.isWin = true;
        }

        StartCoroutine(GameOverManager.instance.GameOver());
        
        gameOverSet.transform.DOScale(Vector3.one, 0.2f);

        yield return null;
    }

    private void ResetSkillLevel()
    {
        SkillManager.instance.ResetSkillLevel();
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
