using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    Rigidbody ballRb;

    void Start() { }

    public void OnKick(Vector3 direction, float force)
    {
        if (ballRb != null)
        {
            ballRb.AddForce(direction.normalized * force);
        }
    }
}
