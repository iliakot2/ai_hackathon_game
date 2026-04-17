using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewStableData", menuName = "HorseFarm/StableData")]
public class StableData : ScriptableObject
{
    [Header("Management")]
    public string stableName;
    public int level = 1;
    public int capacity = 2;
    public int upgradeCost = 500;

    [Header("Inhabitants")]
    public List<HorseData> horses = new List<HorseData>();

    public bool IsFull => horses.Count >= capacity;

    public void Upgrade()
    {
        level++;
        capacity += 2;
        upgradeCost = Mathf.RoundToInt(upgradeCost * 1.5f);
    }
}