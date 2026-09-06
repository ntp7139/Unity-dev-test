using UnityEngine;

public class BallAvailableArea : MonoBehaviour
{
    [SerializeField]
    GameObject ballParent;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                if (ballParent != null)
                {
                    playerController.SetAvailableBall(ballParent);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                if (ballParent != null)
                {
                    playerController.RemoveAvailableBall(ballParent);
                }
            }
        }
    }
}
