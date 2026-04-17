using UnityEngine;
using UnityEngine.InputSystem;

public class HorseCareTrigger : MonoBehaviour
{
    public enum CareType { Feed, Wash }
    public CareType careType;
    public float careAmount = 0.2f;

    [Header("Visuals")]
    public GameObject highlightEffect;

    private bool playerInRange = false;
    private HorseController currentHorse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<HorseController>(out var horse))
        {
            currentHorse = horse;
            playerInRange = true;
            if (highlightEffect != null) highlightEffect.SetActive(true);
            Debug.Log($"Near {careType} station. Press E to interact.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<HorseController>(out var horse))
        {
            if (currentHorse == horse)
            {
                currentHorse = null;
                playerInRange = false;
                if (highlightEffect != null) highlightEffect.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (playerInRange && currentHorse != null)
        {
            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                ApplyCare();
            }
        }
    }

    private void ApplyCare()
    {
        if (currentHorse.horseData == null)
        {
            Debug.LogWarning("Current horse has no HorseData assigned!");
            return;
        }

        switch (careType)
        {
            case CareType.Feed:
                currentHorse.horseData.hunger = Mathf.Clamp01(currentHorse.horseData.hunger + careAmount);
                Debug.Log($"Horse fed! Hunger: {currentHorse.horseData.hunger}");
                break;
            case CareType.Wash:
                currentHorse.horseData.cleanliness = Mathf.Clamp01(currentHorse.horseData.cleanliness + careAmount);
                Debug.Log($"Horse washed! Cleanliness: {currentHorse.horseData.cleanliness}");
                break;
        }
    }
}