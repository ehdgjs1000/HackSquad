using System.Collections.Generic;
using UnityEngine;

public class SkillUpgradeManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject skillUpgradePanel;
    public SkillUpgradeUI[] skillButtons;  // SkillButton_0, 1, 2

    Hero _hero;

    void Start()
    {
        _hero = FindAnyObjectByType<Hero>();
        SquadManager.Instance.OnLevelUp += _ => OpenPanel();
        skillUpgradePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            OpenPanel();
    }

    void OpenPanel()
    {
        if (_hero == null) return;

        // 이미 최종형에 도달한 스킬 제외
        var candidates = _hero.GetSkillCandidates()
            .FindAll(c =>
            {
                var existing = _hero.skills.Find(s => s.skillName == c.skillName);
                return existing == null || !existing.IsMaxLevel;
            });

        if (candidates.Count == 0) return;

        skillUpgradePanel.SetActive(true);
        Time.timeScale = 0f;

        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (i < candidates.Count)
                skillButtons[i].Setup(candidates[i], _hero, OnSkillSelected);
            else
                skillButtons[i].Hide();
        }
    }

    void OnSkillSelected(SkillBase skill)
    {
        var existing = _hero.skills.Find(s => s.skillName == skill.skillName);
        if (existing != null)
            existing.Upgrade();
        else
        {
            skill.OnEquip(_hero);
            _hero.skills.Add(skill);
        }

        skillUpgradePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
