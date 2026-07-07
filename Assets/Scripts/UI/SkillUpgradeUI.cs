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

        var existing = hero.skills.Find(s => s.skillName == skill.skillName);
        bool aboutToFinalize = existing != null && existing.level == SkillBase.MaxLevel - 1;

        skillNameText.text = GetNameLabel(skill, hero, aboutToFinalize);
        skillDescText.text = GetDescription(skill, existing, aboutToFinalize);

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

    string GetNameLabel(SkillBase skill, Hero hero, bool aboutToFinalize)
    {
        var existing = hero.skills.Find(s => s.skillName == skill.skillName);
        if (existing == null)      return $"[NEW] {skill.skillName}";
        if (aboutToFinalize)       return $"[FINAL] {skill.skillName}";
        return $"[Lv.{existing.level}→{existing.level + 1}] {skill.skillName}";
    }

    // 신규 습득: initDescription / 최종 승급 직전: finalDescription / 그 외 업그레이드: description
    string GetDescription(SkillBase skill, SkillBase existing, bool aboutToFinalize)
    {
        if (existing == null)
            return !string.IsNullOrEmpty(skill.initDescription) ? skill.initDescription : skill.description;

        if (aboutToFinalize && !string.IsNullOrEmpty(skill.finalDescription))
            return skill.finalDescription;

        return existing.description;
    }
}
