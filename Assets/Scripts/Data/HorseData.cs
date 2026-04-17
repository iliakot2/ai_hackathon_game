using UnityEngine;

[CreateAssetMenu(fileName = "NewHorseData", menuName = "HorseFarm/HorseData")]
public class HorseData : ScriptableObject
{
    [Header("Basic Info")]
    public string breed;
    public string color;

    [Header("Stats")]
    public float speed = 5f;
    public float jumpForce = 7f;
    
    [Header("Care Status")]
    [Range(0, 1)]
    public float hunger = 0.5f;
    
    [Range(0, 1)]
    public float cleanliness = 0.5f;

    public bool NeedsFeeding => hunger < 0.3f;
    public bool NeedsWashing => cleanliness < 0.3f;
}