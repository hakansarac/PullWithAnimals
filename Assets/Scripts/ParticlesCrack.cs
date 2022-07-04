using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesCrack : MonoBehaviour
{
    private void Start()
    {
        var _ps = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = _ps.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    private void OnParticleSystemStopped()
    {
        if (AnimalManager.Instance.m_TypeAnimal == AnimalManager.State.WOLF)
            ParticlePooler.Instance.crackPoolWolf.Add(this.gameObject.GetComponent<ParticleSystem>());
        else if (AnimalManager.Instance.m_TypeAnimal == AnimalManager.State.HORSE)
            ParticlePooler.Instance.crackPoolHorse.Add(this.gameObject.GetComponent<ParticleSystem>());
        else if (AnimalManager.Instance.m_TypeAnimal == AnimalManager.State.BEAR)
            ParticlePooler.Instance.crackPoolBear.Add(this.gameObject.GetComponent<ParticleSystem>());
    }
}
