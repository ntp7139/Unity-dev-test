using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongratsGoalParticlePools : MonoBehaviour
{
    public static CongratsGoalParticlePools Instance;
    public Queue<GameObject> pools;
    public GameObject defaultParticle;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(this);
    }

    void Start()
    {
        pools = new Queue<GameObject>();
    }

    public void GetParticleOnPos(Vector3 pos)
    {
        if (pools == null)
        {
            pools = new Queue<GameObject>();
        }
        GameObject particle = null;
        if (pools.Count > 0)
        {
            particle = pools.Dequeue();
        }
        if (particle == null)
        {
            particle = Instantiate(defaultParticle, this.transform);
        }
        if (particle != null)
        {
            particle.transform.position = pos;
            ParticleSystem particleSystem = particle.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Clear();
                particleSystem.Play();
                StartCoroutine(ReturnToPools(particleSystem));
            }
        }
    }

    public IEnumerator ReturnToPools(ParticleSystem particleSystem)
    {
        yield return new WaitUntil(() => particleSystem == null || !particleSystem.IsAlive(true));

        if (particleSystem != null)
        {
            pools.Enqueue(particleSystem.gameObject);
        }
    }
}
