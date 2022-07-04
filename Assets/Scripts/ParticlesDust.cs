using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesDust : MonoBehaviour
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
        if(AnimalManager.Instance.m_TypeAnimal == AnimalManager.State.WOLF)
            ParticlePooler.Instance.dustPoolWolf.Add(this.gameObject.GetComponent<ParticleSystem>());
        else if (AnimalManager.Instance.m_TypeAnimal == AnimalManager.State.HORSE)
            ParticlePooler.Instance.dustPoolHorse.Add(this.gameObject.GetComponent<ParticleSystem>());
        else if(AnimalManager.Instance.m_TypeAnimal == AnimalManager.State.BEAR)
            ParticlePooler.Instance.dustPoolBear.Add(this.gameObject.GetComponent<ParticleSystem>());

    }    
        
}
