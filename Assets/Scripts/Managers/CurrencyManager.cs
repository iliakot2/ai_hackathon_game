using UnityEngine;
using System;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    [SerializeField] private int coins = 5000;
    [SerializeField] private int gems = 1000;

    public int Coins => coins;
    public int Gems => gems;

    public static event Action OnCurrencyChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            OnCurrencyChanged?.Invoke();
            return true;
        }
        return false;
    }

    public bool SpendGems(int amount)
    {
        if (gems >= amount)
        {
            gems -= amount;
            OnCurrencyChanged?.Invoke();
            return true;
        }
        return false;
    }
}