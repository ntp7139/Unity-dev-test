using UnityEngine;

public class TestCollide : MonoBehaviour
{
    void Update() { }

    void OnCollisionStay(Collision collision)
    {
        Debug.Log("Có");
    }
}
