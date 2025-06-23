using UnityEngine.UI;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Level System
    float hp = 100;
    [SerializeField] int level;
    float nowExp;
    float needExp = 100.0f;

    //UI
    [SerializeField] Image hpImage;
    [SerializeField] Image expImage;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] GameObject upgradePanel;

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
        InitSkillInfo();
    }
    private void SetHeros()
    {
        for (int a = 0; a < 4; a++)
        {
            if (ChangeScene.instance.heros[a] != null)
            {
                players[a] = ChangeScene.instance.heros[a].GetComponent<PlayerCtrl>();
                PlayerCtrl hero = Instantiate(players[a], heroPos[a].position, Quaternion.identity);
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
        levelText.text = level.ToString() + "레 벨";
    }
    private void GameOver()
    {
        ResetSkillLevel();
    }
    private void ResetSkillLevel()
    {
        SkillManager.instance.ResetSkillLevel();
    }

    public void LevelUp()
    {
        print("Level Up");
        level++;
        nowExp -= needExp;
        needExp *= 1.2f;
        upgradePanel.transform.DOScale(Vector3.one, 0.1f);
        for (int a = 0; a < upgradeSkills.Length; a++)
        {
            upgradeSkills[a].SkillChoose();
        }

        UpdateUI();
    }

}
