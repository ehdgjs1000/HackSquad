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
    public PlayerCtrl[] players;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        InitSkillInfo();
    }
    //���� ���۽� �ʱ� ��ų���� �����ص�
    private void InitSkillInfo() 
    {
        int skillCount = 0;
        for (int a = 0; a < players.Length; a++)
        {
            for(int b = 0; b < 3; b++)
            {
                SkillManager.instance.skillDatas[skillCount] = players[a].characterSkills[b];
                skillCount++;
            }
            SkillManager.instance.finalSkill[a] = players[a].finalSkill;
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
        levelText.text = level.ToString() + "�� ��";
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
