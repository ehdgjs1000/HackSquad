using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    MonsterSpawner monsterSpawner;

    //Level System
    public int gameLevel;
    float initHp = 0;
    float hp = 100;
    [SerializeField] int level;
    float nowExp;
    float needExp = 50.0f;
    bool isGameOver = false;
    public int heroCount;
    float expRatio = 1.15f;
    public float userExp = 0;
    public float gameSpeed = 1.0f;
    [SerializeField] TextMeshProUGUI gameSpeedText;
    //UI
    [SerializeField] Image hpImage;
    [SerializeField] Image expImage;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject gameOverSet;
    [SerializeField] BossImage bossImage;
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
    public int killMonsterCount = 0;
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
        userExp = 0;
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
                StartCoroutine(GameOver());
            }
        }
        
    }
    public void GameSpeedOnClick()
    {
        if (gameSpeed == 1.0f) gameSpeed = 1.5f;
        else if (gameSpeed == 1.5f) gameSpeed = 5.0f;
        else if (gameSpeed == 5.0f) gameSpeed = 1.0f;
        GameSpeed(gameSpeed);
    }
    public void IsBossDie()
    {
        isBossMode = false;
        monsterSpawner.canSpawn = true;
        sec += 5.0f;
    }
    IEnumerator SpawnBoss()
    {
        isBossMode = true;
        bossImage.gameObject.SetActive(true);
        monsterSpawner.canSpawn = false;
        StartCoroutine(bossImage.BossImageFade());
        yield return new WaitForSeconds(5.0f);

        //���� ����
        monsterSpawner.SpawnBoss();
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

        //5�� 10�� ���� ����
        if((min == 5 && sec <= 1.0f) || min == 10)
        {
            StartCoroutine(SpawnBoss());
        }


    }
    public void GetExp(float _gainExp)
    {
        nowExp += _gainExp;
        UpdateUI();
        if (nowExp >= needExp) LevelUp();
    }
    private void Reborn()
    {
        rebornCount--;
        hp = initHp * (rebornAmount/100);
        UpdateUI();
    }
    private void SetHeros()
    {
        //��Ȱ Ƚ��
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
        //Ability ü�� ����
        hp += BackEndGameData.Instance.UserAbilityData.abilityLevel[1] * 2;
        hp += BackEndGameData.Instance.UserAbilityData.abilityLevel[4] * 4;
        hp += BackEndGameData.Instance.UserAbilityData.abilityLevel[7] * 10;

        initHp = hp;
    }
    //���� ���۽� �ʱ� ��ų���� �����ص�
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
        expImage.fillAmount = nowExp / needExp;
        levelText.text = level.ToString() + "����";
    }
    public void GetDamage(float _damage)
    {
        _damage -= (BackEndGameData.Instance.UserAbilityData.abilityLevel[5]) +
            (BackEndGameData.Instance.UserAbilityData.abilityLevel[8] * 3);
        if(_damage >= 0)
        {
            hp -= _damage;
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
            //��Ȱ�� ������ ��� ��Ȱ
            if(rebornCount >= 1)
            {
                Reborn();
            }
            else StartCoroutine(GameOver());
        }
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
    public void LevelUp()
    {
        level++;
        nowExp -= needExp;
        needExp *= expRatio;

        SkillChoose();
        UpdateUI();
    }
    public void BossLevelUp()
    {
        int levelUpAmount = Random.Range(1,3);
        for (int i = 0; i <= levelUpAmount; i++)
        {
            SkillChoose();
            UpdateUI();
        }
    }
    public void SkillChoose()
    {
        // ��ų �� �� �ִ� �ִ� ����
        if (level < heroCount * 10)
        {
            upgradePanel.transform.localScale = Vector3.one;
            //upgradePanel.transform.DOScale(Vector3.one, 0f);
            for (int a = 0; a < upgradeSkills.Length; a++)
            {
                upgradeSkills[a].SkillChoose();
            }
            GameSpeed(0.0f);
        }
    }
    public void DieOnClick()
    {
        StartCoroutine(GameOver());
    }

}
