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
        ResetSkillInfo();
    }
    //게임 시작시 초기 스킬들을 저장해둠
    private void ResetSkillInfo() 
    {
        int skillCount = 0;
        for (int a = 0; a < players.Length; a++)
        {
            for(int b = 0; b < 3; b++)
            {
                SkillManager.instance.skillDatas[skillCount] = players[a].characterSkills[b];
                skillCount++;
            }
        }
    }
    public void UpdateGameSpeed(int _speed)
    {
        Time.timeScale = _speed;
    }
    private void UpdateUI()
    {
        hpImage.fillAmount = hp / 100;
        expImage.fillAmount = nowExp / needExp;
        levelText.text = level.ToString();
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
