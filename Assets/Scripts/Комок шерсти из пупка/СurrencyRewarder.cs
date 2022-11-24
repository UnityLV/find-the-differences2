using UnityEngine;

public class СurrencyRewarder : MonoBehaviour
{
    [SerializeField] private PlayerCurrency _playerCurrency;
    [SerializeField] private LevelFinalizer _levelFinalizer;

    [SerializeField] private DifferenceButtonPromt _buttonPromt;
    [SerializeField] private int[] _rewards;

    private void OnEnable() => _levelFinalizer.LevelSolved += OnLevelSolved;

    private void OnDisable() => _levelFinalizer.LevelSolved -= OnLevelSolved;

    private void OnLevelSolved(int levelIndex) => _playerCurrency.Add(CalculateReward());

    private int CalculateReward() => _rewards[_buttonPromt.PromtUsedThisLevel];

}

