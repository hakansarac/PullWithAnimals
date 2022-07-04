using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesSmoke : MonoBehaviour
{
    private void Start()
    {
        var _ps = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = _ps.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    private void OnParticleSystemStopped()
    {
        Debug.Log("onParticleSystemStopped");
        if (AnimalManager.Instance.m_TypeAnimal == AnimalManager.State.WOLF)
            ParticlePooler.Instance.smokePoolWolf.Add(this.gameObject.GetComponent<ParticleSystem>());
        else if (AnimalManager.Instance.m_TypeAnimal == AnimalManager.State.HORSE)
            ParticlePooler.Instance.smokePoolHorse.Add(this.gameObject.GetComponent<ParticleSystem>());
        else if (AnimalManager.Instance.m_TypeAnimal == AnimalManager.State.BEAR)
            ParticlePooler.Instance.smokePoolBear.Add(this.gameObject.GetComponent<ParticleSystem>());

    }
}
