using UnityEngine;
using System.Collections.Generic;

public class StableManager : MonoBehaviour
{
    public static StableManager Instance { get; private set; }

    [Header("Farm Data")]
    public List<StableData> stables = new List<StableData>();
    public int currentCurrency = 1000;

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

    public bool BuyStable(StableData stableDataTemplate)
    {
        // For simplicity, we create a new instance of the StableData
        StableData newStable = Instantiate(stableDataTemplate);
        stables.Add(newStable);
        return true;
    }

    public bool UpgradeStable(StableData stable)
    {
        if (currentCurrency >= stable.upgradeCost)
        {
            currentCurrency -= stable.upgradeCost;
            stable.Upgrade();
            Debug.Log($"Upgraded stable: {stable.stableName} to level {stable.level}");
            return true;
        }
        return false;
    }

    public void AddHorseToStable(HorseData horse, StableData stable)
    {
        if (!stable.IsFull)
        {
            stable.horses.Add(horse);
        }
    }

    public void AddCurrency(int amount)
    {
        currentCurrency += amount;
    }
}