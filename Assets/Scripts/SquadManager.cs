using System;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : MonoBehaviour
{
    public static SquadManager Instance { get; private set; }

    [Header("EXP / Level")]
    public int currentExp;
    public int currentLevel = 1;
    public int expToNextLevel = 50;

    [Header("Squad")]
    public List<Hero> heroes = new();

    public event Action<int> OnLevelUp;  // 레벨업 이벤트 → UI가 구독

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void AddExp(int amount)
    {
        currentExp += amount;
        while (currentExp >= expToNextLevel)
        {
            currentExp -= expToNextLevel;
            currentLevel++;
            expToNextLevel = Mathf.RoundToInt(expToNextLevel * 1.25f);
            OnLevelUp?.Invoke(currentLevel);
        }
    }
}