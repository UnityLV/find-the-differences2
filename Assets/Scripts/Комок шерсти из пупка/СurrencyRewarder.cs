using UnityEngine;
using UnityEngine.Events;

public class СurrencyRewarder : MonoBehaviour
{
    [SerializeField] private PlayerCurrency _playerCurrency;
    [SerializeField] private LevelFinalizer _levelFinalizer;

    [SerializeField] private DifferenceButtonPromt _buttonPromt;
    [SerializeField] private int[] _rewards;

    public event UnityAction<int> RewardSended;

    private void OnEnable() => _levelFinalizer.LevelSolved += OnLevelSolved;

    private void OnDisable() => _levelFinalizer.LevelSolved -= OnLevelSolved;

    private void OnLevelSolved(int levelIndex)
    {
        int reward = CalculateReward();
        _playerCurrency.Add(reward);
        RewardSended?.Invoke(reward);
    }

    private int CalculateReward() => _rewards[_buttonPromt.PromtUsedThisLevel];

}

