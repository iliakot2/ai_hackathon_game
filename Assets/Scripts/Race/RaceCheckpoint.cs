using UnityEngine;

public class RaceCheckpoint : MonoBehaviour
{
    public int checkpointIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HorseController>())
        {
            RaceSystem.Instance.OnCheckpointReached(checkpointIndex);
        }
    }
}