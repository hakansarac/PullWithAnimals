using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    [SerializeField] Transform[] transformsDust;
    [SerializeField] Transform[] transformsCrack;
    [SerializeField] Transform transformSmoke;
    [SerializeField] ParticleSystem m_Sweltering;


    private void Update()
    {
        if (m_Sweltering.isPlaying && StaminaBar.Instance.m_CurStamina > 40)
        {
            m_Sweltering.Stop();
        }
    }


    public void RightFootStart()
    {
        for(int i=0; i < 2; i++)
        {
            PlayParticle(i+2);
        }
    }

    public void LeftFootStart()
    {
        for (int i = 0; i < 2; i++)
        {
            PlayParticle(i);
        }
    }

    public void SmokeStart()
    {
        if(StaminaBar.Instance.m_CurStamina < 70)
        {
            if (AnimalManager.Instance.m_TypeAnimal == AnimalManager.State.WOLF)
            {
                ParticleSystem smoke = ParticlePooler.Instance.smokePoolWolf[ParticlePooler.Instance.smokePoolWolf.Count - 1];
                ParticlePooler.Instance.smokePoolWolf.RemoveAt(ParticlePooler.Instance.smokePoolWolf.Count - 1);

                smoke.transform.position = transformSmoke.position;
                smoke.transform.rotation = transformSmoke.rotation;

                smoke.Play();
            }

            else if (AnimalManager.Instance.m_TypeAnimal == AnimalManager.State.HORSE)
            {
                ParticleSystem smoke = ParticlePooler.Instance.smokePoolHorse[ParticlePooler.Instance.smokePoolHorse.Count - 1];
                ParticlePooler.Instance.smokePoolHorse.RemoveAt(ParticlePooler.Instance.smokePoolHorse.Count - 1);

                smoke.transform.position = transformSmoke.position;
                smoke.transform.rotation = transformSmoke.rotation;

                smoke.Play();
            }

            else if (AnimalManager.Instance.m_TypeAnimal == AnimalManager.State.BEAR)
            {
                ParticleSystem smoke = ParticlePooler.Instance.smokePoolBear[ParticlePooler.Instance.smokePoolBear.Count - 1];
                ParticlePooler.Instance.smokePoolBear.RemoveAt(ParticlePooler.Instance.smokePoolBear.Count - 1);

                smoke.transform.position = transformSmoke.position;
                smoke.transform.rotation = transformSmoke.rotation;

                smoke.Play();
            }
        }
        
    }

    public void SwelteringStart()
    {
        if (!m_Sweltering.isPlaying && StaminaBar.Instance.m_CurStamina <50)
        {
            m_Sweltering.Play();
        } 

        
    }
    

    private void PlayParticle(int _index)
    {
        if(AnimalManager.Instance.m_TypeAnimal == AnimalManager.State.WOLF)
        {
            ParticleSystem dust = ParticlePooler.Instance.dustPoolWolf[ParticlePooler.Instance.dustPoolWolf.Count - 1];
            ParticlePooler.Instance.dustPoolWolf.RemoveAt(ParticlePooler.Instance.dustPoolWolf.Count - 1);

            ParticleSystem crack = ParticlePooler.Instance.crackPoolWolf[ParticlePooler.Instance.crackPoolWolf.Count - 1];
            ParticlePooler.Instance.crackPoolWolf.RemoveAt(ParticlePooler.Instance.crackPoolWolf.Count - 1);                      

            dust.transform.position = transformsDust[_index].position;
            dust.transform.rotation = transformsDust[_index].rotation;

            crack.transform.position = transformsCrack[_index].position;
            crack.transform.rotation = transformsCrack[_index].rotation;          

            dust.Play();
            crack.Play();
        }
        
        else if(AnimalManager.Instance.m_TypeAnimal == AnimalManager.State.HORSE)
        {
            ParticleSystem dust = ParticlePooler.Instance.dustPoolHorse[ParticlePooler.Instance.dustPoolHorse.Count - 1];
            ParticlePooler.Instance.dustPoolHorse.RemoveAt(ParticlePooler.Instance.dustPoolHorse.Count - 1);

            ParticleSystem crack = ParticlePooler.Instance.crackPoolHorse[ParticlePooler.Instance.crackPoolHorse.Count - 1];
            ParticlePooler.Instance.crackPoolHorse.RemoveAt(ParticlePooler.Instance.crackPoolHorse.Count - 1);

            dust.transform.position = transformsDust[_index].position;
            dust.transform.rotation = transformsDust[_index].rotation;

            crack.transform.position = transformsCrack[_index].position;
            crack.transform.rotation = transformsCrack[_index].rotation;

            dust.Play();
            crack.Play();
        }

        else if (AnimalManager.Instance.m_TypeAnimal == AnimalManager.State.BEAR)
        {
            ParticleSystem dust = ParticlePooler.Instance.dustPoolBear[ParticlePooler.Instance.dustPoolBear.Count - 1];
            ParticlePooler.Instance.dustPoolBear.RemoveAt(ParticlePooler.Instance.dustPoolBear.Count - 1);

            ParticleSystem crack = ParticlePooler.Instance.crackPoolBear[ParticlePooler.Instance.crackPoolBear.Count - 1];
            ParticlePooler.Instance.crackPoolBear.RemoveAt(ParticlePooler.Instance.crackPoolBear.Count - 1);

            
            dust.transform.position = transformsDust[_index].position;
            dust.transform.rotation = transformsDust[_index].rotation;

            crack.transform.position = transformsCrack[_index].position;
            crack.transform.rotation = transformsCrack[_index].rotation;

            dust.Play();
            crack.Play();
        }
    }

}
