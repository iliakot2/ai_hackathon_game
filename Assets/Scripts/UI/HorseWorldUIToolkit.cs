using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class HorseWorldUIToolkit : MonoBehaviour
{
    [Header("References")]
    public HorseController horseController;
    public float statIncreaseRate = 0.01f;

    private ProgressBar hungerBar;
    private ProgressBar staminaBar;
    private Button feedButton;
    private Button rideButton;
    private VisualElement root;

    private void OnEnable()
    {
        var doc = GetComponent<UIDocument>();
        if (doc == null) return;

        root = doc.rootVisualElement;
        hungerBar = root.Q<ProgressBar>("hunger-bar");
        staminaBar = root.Q<ProgressBar>("stamina-bar");
        feedButton = root.Q<Button>("feed-button");
        rideButton = root.Q<Button>("ride-button");

        if (feedButton != null) feedButton.clicked += FeedHorse;
        if (rideButton != null) rideButton.clicked += RideHorse;
    }

    private void Update()
    {
        if (horseController == null || horseController.horseData == null) return;

        // 1. Stats logic (Same as World UI version)
        horseController.horseData.hunger = Mathf.Clamp01(horseController.horseData.hunger + statIncreaseRate * Time.deltaTime);
        horseController.horseData.stamina = Mathf.Clamp01(horseController.horseData.stamina + statIncreaseRate * Time.deltaTime);

        // 2. Update UI
        if (hungerBar != null) hungerBar.value = horseController.horseData.hunger * 100f;
        if (staminaBar != null) staminaBar.value = horseController.horseData.stamina * 100f;

        // 3. Billboard effect
        if (Camera.main != null)
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }
    }

    private void FeedHorse()
    {
        if (horseController.horseData == null) return;

        if (CurrencyManager.Instance != null && CurrencyManager.Instance.SpendCoins(5))
        {
            horseController.horseData.hunger = Mathf.Clamp01(horseController.horseData.hunger - 0.2f);
            Debug.Log("UIT Fed horse! -5 coins.");
        }
    }

    private void RideHorse()
    {
        if (horseController.horseData == null) return;

        if (horseController.horseData.stamina > 0.1f)
        {
            horseController.horseData.stamina = Mathf.Clamp01(horseController.horseData.stamina - 0.3f);
            if (CurrencyManager.Instance != null)
            {
                CurrencyManager.Instance.AddCoins(50);
            }
            ShowRewardPopup();
        }
        else
        {
            Debug.Log("UIT Horse too tired!");
        }
    }

    private void ShowRewardPopup()
    {
        // For UIToolkit, we can show an overlay or use the existing uGUI popup
        // Let's use the uGUI popup I created earlier for consistency in "popups" 
        // or I can create a UIToolkit one. The user said "comes popup window".
        // Let's find the existing reward popup and enable it.
        var popup = GameObject.Find("RewardPopup");
        if (popup != null)
        {
            popup.SetActive(true);
        }
        else
        {
            Debug.Log("Ride complete! You reward is 50 coins.");
        }
    }
    }