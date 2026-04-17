using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HorseWorldUI : MonoBehaviour
{
    [Header("References")]
    public HorseController horseController;
    public Slider hungerSlider;
    public Slider staminaSlider;
    public Button feedButton;
    public Button rideButton;
    public GameObject rewardPopup; // Screen space popup
    public TextMeshProUGUI rewardText;

    [Header("Settings")]
    public float statIncreaseRate = 0.01f; // How fast hunger/stamina increases/decreases over time

    private void Start()
    {
        if (feedButton != null) feedButton.onClick.AddListener(FeedHorse);
        if (rideButton != null) rideButton.onClick.AddListener(RideHorse);
        if (rewardPopup != null) rewardPopup.SetActive(false);
    }

    private void Update()
    {
        if (horseController == null || horseController.horseData == null) return;

        // 1. Increase hunger and stamina over time (hunger increasing means getting hungrier)
        horseController.horseData.hunger = Mathf.Clamp01(horseController.horseData.hunger - statIncreaseRate * Time.deltaTime);
        horseController.horseData.stamina = Mathf.Clamp01(horseController.horseData.stamina + statIncreaseRate * Time.deltaTime);

        // 2. Update UI bars
        if (hungerSlider != null) hungerSlider.value = horseController.horseData.hunger;
        if (staminaSlider != null) staminaSlider.value = horseController.horseData.stamina;

        // 3. Billboard effect: face the camera
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }

    public void FeedHorse()
    {
        if (horseController.horseData == null) return;

        if (CurrencyManager.Instance != null && CurrencyManager.Instance.SpendCoins(5))
        {
            horseController.horseData.hunger = Mathf.Clamp01(horseController.horseData.hunger + 0.2f);
            Debug.Log("Fed horse! -5 coins.");
        }
        else
        {
            Debug.Log("Not enough coins to feed!");
        }
    }

    public void RideHorse()
    {
        if (horseController.horseData == null) return;

        if (horseController.horseData.stamina > 0.2f)
        {
            horseController.horseData.stamina = Mathf.Clamp01(horseController.horseData.stamina - 0.3f);
            if (CurrencyManager.Instance != null)
            {
                // Add coins (assuming AddCurrency exists or using a dummy method for now)
                // Let's use reflection or check CurrencyManager.cs
                // I previously added AddCurrency to StableManager, but let's add it to CurrencyManager
                CurrencyManager.Instance.AddCoins(50);
            }
            ShowRewardPopup();
        }
        else
        {
            Debug.Log("Horse is too tired to ride!");
        }
    }

    private void ShowRewardPopup()
    {
        if (rewardPopup != null)
        {
            rewardPopup.SetActive(true);
        }
    }

    public void ClosePopup()
    {
        if (rewardPopup != null) rewardPopup.SetActive(false);
    }
}