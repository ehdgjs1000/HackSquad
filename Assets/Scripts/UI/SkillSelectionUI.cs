using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 레벨업 시 스킬 후보 3개 표시 → 유저 선택
public class SkillSelectionUI : MonoBehaviour
{
    public GameObject panel;
    public Button[] optionButtons;   // 3개 버튼
    public TextMeshProUGUI[] optionTexts;

    List<SkillBase> _candidates;

    void Start()
    {
        SquadManager.Instance.OnLevelUp += ShowSkillSelection;
        panel.SetActive(false);
    }

    void ShowSkillSelection(int newLevel)
    {
        Time.timeScale = 0f;  // 일시정지
        _candidates = BuildCandidates();
        panel.SetActive(true);

        for (int i = 0; i < optionButtons.Length; i++)
        {
            int idx = i;
            if (i < _candidates.Count)
            {
                optionTexts[i].text = $"[{_candidates[i].skillName}]\n{_candidates[i].description}";
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => SelectSkill(idx));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void SelectSkill(int idx)
    {
        // 선택한 스킬을 히어로에 적용 (첫 번째 히어로 기준)
        var hero = SquadManager.Instance.heroes.Count > 0
            ? SquadManager.Instance.heroes[0] : null;
        if (hero != null && idx < _candidates.Count)
        {
            var skill = _candidates[idx];
            var existing = hero.skills.Find(s => s.skillName == skill.skillName);
            if (existing != null)
                existing.Upgrade();
            else
                hero.skills.Add(skill);
        }
        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    List<SkillBase> BuildCandidates()
    {
        // 프로토타입: 더미 스킬 후보 반환 (추후 히어로별 풀에서 랜덤 선택)
        return new List<SkillBase>
        {
            new AttackSpeedSkill(),
            new DamageSkill(),
            new AmmoSkill()
        };
    }
}