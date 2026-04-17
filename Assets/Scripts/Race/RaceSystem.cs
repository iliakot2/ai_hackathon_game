using UnityEngine;
using System.Collections.Generic;

public class RaceSystem : MonoBehaviour
{
    public static RaceSystem Instance { get; private set; }

    [Header("Race Configuration")]
    public List<Transform> checkpoints = new List<Transform>();
    public float raceTimeLimit = 120f;
    public int basePoints = 1000;

    private int nextCheckpointIndex = 0;
    private float currentRaceTime = 0f;
    private bool isRaceActive = false;
    private int successfulJumps = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void StartRace()
    {
        nextCheckpointIndex = 0;
        currentRaceTime = 0f;
        isRaceActive = true;
        successfulJumps = 0;
        Debug.Log("Race Started!");
    }

    private void Update()
    {
        if (isRaceActive)
        {
            currentRaceTime += Time.deltaTime;
            if (currentRaceTime >= raceTimeLimit)
            {
                EndRace(false);
            }
        }
    }

    public void OnCheckpointReached(int index)
    {
        if (isRaceActive && index == nextCheckpointIndex)
        {
            nextCheckpointIndex++;
            Debug.Log($"Checkpoint {nextCheckpointIndex}/{checkpoints.Count} reached!");

            if (nextCheckpointIndex >= checkpoints.Count)
            {
                EndRace(true);
            }
        }
    }

    public void OnSuccessfulJump()
    {
        if (isRaceActive) successfulJumps++;
    }

    private void EndRace(bool success)
    {
        isRaceActive = false;
        if (success)
        {
            int score = CalculateScore();
            StableManager.Instance.AddCurrency(score);
            Debug.Log($"Race Completed! Score: {score}. Currency Added.");
        }
        else
        {
            Debug.Log("Race Failed: Time limit exceeded.");
        }
    }

    private int CalculateScore()
    {
        float timeBonus = Mathf.Max(0, (raceTimeLimit - currentRaceTime) * 10f);
        return Mathf.RoundToInt(basePoints + timeBonus + (successfulJumps * 50));
    }
}