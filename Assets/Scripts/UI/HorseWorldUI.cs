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

        // 1. Both increase over time
        horseController.horseData.hunger = Mathf.Clamp01(horseController.horseData.hunger + statIncreaseRate * Time.deltaTime);
        horseController.horseData.stamina = Mathf.Clamp01(horseController.horseData.stamina + statIncreaseRate * Time.deltaTime);

        // 2. Update UI bars
        if (hungerSlider != null) hungerSlider.value = horseController.horseData.hunger;
        if (staminaSlider != null) staminaSlider.value = horseController.horseData.stamina;

        // 3. Billboard effect: face the camera
        if (Camera.main != null)
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }
    }

    public void FeedHorse()
    {
        if (horseController.horseData == null) return;

        // Feed decreases hunger
        if (CurrencyManager.Instance != null && CurrencyManager.Instance.SpendCoins(5))
        {
            horseController.horseData.hunger = Mathf.Clamp01(horseController.horseData.hunger - 0.2f);
            Debug.Log("Fed horse! -5 coins.");
        }
    }

    public void RideHorse()
    {
        if (horseController.horseData == null) return;

        // Ride decreases stamina
        if (horseController.horseData.stamina > 0.1f)
        {
            horseController.horseData.stamina = Mathf.Clamp01(horseController.horseData.stamina - 0.3f);
            if (CurrencyManager.Instance != null)
            {
                CurrencyManager.Instance.AddCoins(50);
            }
            ShowRewardPopup();
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