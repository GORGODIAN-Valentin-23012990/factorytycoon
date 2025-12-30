using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    public int coins;
    public UnityEvent<int> onCoinsChanged;

    public void Add(int amount)
    {
        coins += amount;
        onCoinsChanged?.Invoke(coins);
    }
    
    public bool TrySpend(int cost){
        if (coins < cost) return false;
        coins -= cost;
        onCoinsChanged?.Invoke(coins);
        return true;
    }
}
