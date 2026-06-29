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
    public Transform[] formationPositions = new Transform[4];

    public event Action<int> OnLevelUp;  // 레벨업 이벤트 → UI가 구독

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        ApplyFormation();
    }

    public void ApplyFormation()
    {
        for (int i = 0; i < heroes.Count && i < formationPositions.Length; i++)
        {
            if (formationPositions[i] == null) continue;
            heroes[i].transform.position = formationPositions[i].position;
        }
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