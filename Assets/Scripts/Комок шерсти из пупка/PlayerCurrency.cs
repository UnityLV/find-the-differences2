using UnityEngine.Events;
using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    public event UnityAction<int> AmountUppdate;    

    public int CurrentAmount { get ; private set; }

    public void Add(int amount)
    {
        CurrentAmount += amount;
        AmountUppdate?.Invoke(CurrentAmount);
    }

    public bool TryRemove(int amount)
    {
        if (CurrentAmount >= amount)
        {
            Remove(amount);
            return true;
        }
        return false;
    }

    private void Remove(int amount)
    {
        CurrentAmount -= amount;
        AmountUppdate?.Invoke(CurrentAmount);
    }
}
