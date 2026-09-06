using UnityEngine;

public class GoalArea : MonoBehaviour, ICanSendEvent
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (CongratsGoalParticlePools.Instance != null)
            {
                CongratsGoalParticlePools.Instance.GetParticleOnPos(this.transform.position);
            }
            this.Publish(new OnGoalEvent());
        }
    }
}
