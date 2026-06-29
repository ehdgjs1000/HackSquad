using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillUpgradeUI : MonoBehaviour
{
    [Header("References")]
    public Button button;
    public TextMeshProUGUI skillNameText;
    public TextMeshProUGUI skillDescText;

    SkillBase _skill;
    Action<SkillBase> _onSelect;

    public void Setup(SkillBase skill, Hero hero, Action<SkillBase> onSelect)
    {
        _skill = skill;
        _onSelect = onSelect;

        skillNameText.text = GetNameLabel(skill, hero);
        skillDescText.text = skill.description;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void OnClick()
    {
        _onSelect?.Invoke(_skill);
    }

    string GetNameLabel(SkillBase skill, Hero hero)
    {
        var existing = hero.skills.Find(s => s.skillName == skill.skillName);
        if (existing == null)      return $"[NEW] {skill.skillName}";
        if (existing.IsMaxLevel)   return $"[MAX] {skill.skillName}";
        return $"[Lv.{existing.level}→{existing.level + 1}] {skill.skillName}";
    }
}
