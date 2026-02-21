using UnityEngine;
using System;

public class PlayerProgression : MonoBehaviour
{
    [Header("========== Player Stats ==========")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private float currentXP = 0f;
    [SerializeField] private float baseEXToNextLevel = 100f;
    [SerializeField] private float EXGrowthFactor = 1.5f;

    [Header("========== Add to Stats on Level Up ==========")]
    [SerializeField] private float healthBonusPerLevel = 10f;
    [SerializeField] private float manaBonusPerLevel = 5f;

    public int CurrentLevel => currentLevel;
    public float CurrentXP => currentXP;

    public event Action<int> OnLevelUp;
    public event Action<float, float> OnXPChanged; // current XP, XP needed for next level


    private void Awake()
    {
        if (playerStats == null)
            Debug.LogError("PlayerProgression: PlayerStats reference is missing!", this);
        
        float _required = GetRequiredXPForNextLevel();
        OnXPChanged?.Invoke(currentXP, _required);
    }


    private float GetRequiredXPForNextLevel()
    {
        float _required = baseEXToNextLevel;
        
        int _power = Mathf.Max(0, currentLevel - 1); // Level 1 requires baseEXToNextLevel, Level 2 requires baseEXToNextLevel * EXGrowthFactor, etc.
        _required *= Mathf.Pow(EXGrowthFactor, _power); // This will exponentially increase the required XP for each level.

        return _required;
    }


    public void AddXP(float amount)
    {
        if (amount <= 0f) return; // Ignore non-positive XP gains
        
        currentXP += amount;

        bool _lvlUpAtLeastOnce = false;

        while (true)
        {
            float _required = GetRequiredXPForNextLevel();

            if (currentXP < _required) break;

            currentXP -= _required; // Subtract the required XP for the level up
            LevelUpInternal();
            _lvlUpAtLeastOnce = true;
        }

        float _nextRequired = GetRequiredXPForNextLevel();
        OnXPChanged?.Invoke(currentXP, _nextRequired);

        if (_lvlUpAtLeastOnce)
            Debug.Log($"Player leveled up to level {currentLevel}! Remaining XP: {currentXP}/{_nextRequired}", this);
    }


    private void LevelUpInternal()
    {
        currentLevel++;
        OnLevelUp?.Invoke(currentLevel);
        playerStats?.ApplyLevelUpBonuses(healthBonusPerLevel, manaBonusPerLevel);
    }
}
