using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Level System
    float hp = 0;
    [SerializeField] int level;
    float nowExp;
    float needExp = 100.0f;
    bool isGameOver = false;

    //UI
    [SerializeField] Image hpImage;
    [SerializeField] Image expImage;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject gameOverSet;
    //In Game Data
    [SerializeField] Skill[] upgradeSkills;
    //[SerializeField] GameObject[] heroGos;
    [SerializeField] Transform[] heroPos;
    public PlayerCtrl[] players;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        SetHeros();
        UpdateUI();
        InitSkillInfo();
    }
    public void GetExp(float _gainExp)
    {
        nowExp += _gainExp;
    }
    private void SetHeros()
    {
        hp = 0;
        for (int a = 0; a < 4; a++)
        {
            if (ChangeScene.instance.heros[a] != null)
            {
                players[a] = ChangeScene.instance.heros[a].GetComponent<PlayerCtrl>();
                PlayerCtrl hero = Instantiate(players[a], heroPos[a].position, Quaternion.identity);
                hp += hero.ReturnInitHp();
            }
        }

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
    public void UpdateGameSpeed(int _speed)
    {
        Time.timeScale = _speed;
    }
    private void UpdateUI()
    {
        hpImage.fillAmount = hp / 100;
        expImage.fillAmount = nowExp / needExp;
        levelText.text = level.ToString() + "레벨";
    }
    public void GetDamage(float _damage)
    {
        hp -= _damage;
        UpdateUI();
        if (hp <= 0.0f && !isGameOver) StartCoroutine(GameOver());
    }
    IEnumerator GameOver()
    {
        isGameOver = true;
        ResetSkillLevel();
        GameOverManager.instance.SetReward();

        if (hp <= 0) GameOverManager.instance.isWin = false;
        else GameOverManager.instance.isWin = true;

        StartCoroutine(GameOverManager.instance.GameOver());
        gameOverSet.SetActive(true);
        gameOverSet.transform.DOScale(Vector3.one, 0.2f);
        GameSpeed(0.0f);

        yield return null;
    }
    public void GameExitOnClicl()
    {
        GameSpeed(1);
        SceneManager.LoadScene("LobbyScene");
    }
    private void ResetSkillLevel()
    {
        SkillManager.instance.ResetSkillLevel();
    }
    public void GameSpeed(float _speed)
    {
        Time.timeScale = _speed;
    }
    public void LevelUp()
    {
        print("Level Up");
        level++;
        nowExp -= needExp;
        needExp *= 1.2f;
        upgradePanel.transform.DOScale(Vector3.one, 0f);
        for (int a = 0; a < upgradeSkills.Length; a++)
        {
            upgradeSkills[a].SkillChoose();
        }
        UpdateUI();

        GameSpeed(0.0f);
    }

}
