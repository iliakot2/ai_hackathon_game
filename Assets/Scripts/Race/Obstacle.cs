using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool hasBeenJumped = false;

    private void OnTriggerExit(Collider other)
    {
        if (!hasBeenJumped && other.GetComponent<HorseController>())
        {
            // Simple logic: if horse is above a certain height while passing through, it's a success
            if (other.transform.position.y > transform.position.y + 0.5f)
            {
                hasBeenJumped = true;
                RaceSystem.Instance.OnSuccessfulJump();
                Debug.Log("Successful Jump!");
            }
        }
    }

    public void ResetObstacle()
    {
        hasBeenJumped = false;
    }
}