using UnityEngine.Events;
using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    public event UnityAction<int> AmountUppdate;    

    public int Amount { get ; private set; }

    public void Add(int amount)
    {
        Amount += amount;
        AmountUppdate?.Invoke(Amount);
    }

    public bool TryRemove(int amount)
    {
        if (Amount >= amount)
        {
            Remove(amount);
            return true;
        }
        return false;
    }

    private void Remove(int amount)
    {
        Amount -= amount;
        AmountUppdate?.Invoke(Amount);
    }

    public void SetAmount(int amount)
    {
        Amount = amount;
        AmountUppdate?.Invoke(Amount);
    }
}
