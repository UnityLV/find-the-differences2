using UnityEngine;
using UnityEngine.Events;

public abstract class BaseResource : MonoBehaviour
{
    public event UnityAction<int> AmountUppdate;
    private int _currentAmount;
    public void Add(int amount)
    {
        _currentAmount += amount;
        AmountUppdate?.Invoke(_currentAmount);
    }

    public bool TryRemove(int amount)
    {
        if (_currentAmount >= amount)
        {
            Remove(amount);
            return true;
        }
        return false;
    }

    private void Remove(int amount)
    {
        _currentAmount -= amount;
        AmountUppdate?.Invoke(_currentAmount);
    }
}
