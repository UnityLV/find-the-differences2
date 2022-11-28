using TMPro;
using UnityEngine;

public class PlayerCurrencyView : MonoBehaviour
{
    [SerializeField] private PlayerCurrency _playerCurrency;

    [SerializeField] private TMP_Text _amountText;

    private void OnEnable()
    {
        _playerCurrency.AmountUppdate += OnAmountUppdate;
        OnAmountUppdate(_playerCurrency.Amount);
    }

    private void OnDisable()
    {
        _playerCurrency.AmountUppdate -= OnAmountUppdate;
    }

    private void OnAmountUppdate(int amount)
    {
        _amountText.text = amount.ToString();
    }
}
