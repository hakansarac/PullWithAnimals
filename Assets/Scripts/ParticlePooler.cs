using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePooler : MonoBehaviour
{
    [SerializeField] ParticleSystem m_DustWolf;
    [SerializeField] ParticleSystem m_CrackWolf;
    [SerializeField] ParticleSystem m_SmokeWolf;
    [SerializeField] ParticleSystem m_DustHorse;
    [SerializeField] ParticleSystem m_CrackHorse;
    [SerializeField] ParticleSystem m_SmokeHorse;
    [SerializeField] ParticleSystem m_DustBear;
    [SerializeField] ParticleSystem m_CrackBear;
    [SerializeField] ParticleSystem m_SmokeBear;



    public List<ParticleSystem> dustPoolWolf = new List<ParticleSystem>();
    public List<ParticleSystem> crackPoolWolf = new List<ParticleSystem>();
    public List<ParticleSystem> smokePoolWolf = new List<ParticleSystem>();

    public List<ParticleSystem> dustPoolHorse = new List<ParticleSystem>();
    public List<ParticleSystem> crackPoolHorse = new List<ParticleSystem>();
    public List<ParticleSystem> smokePoolHorse = new List<ParticleSystem>();

    public List<ParticleSystem> dustPoolBear = new List<ParticleSystem>();
    public List<ParticleSystem> crackPoolBear = new List<ParticleSystem>();
    public List<ParticleSystem> smokePoolBear = new List<ParticleSystem>();

    //Singleton to create only one ObjectPooler object 
    #region Singleton
    public static ParticlePooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    //end singleton

    private void Start()
    {
        for (int i = 0; i < 300; i++)
        {
            ParticleSystem obj = Instantiate(m_DustWolf);
            obj.gameObject.SetActive(true);
            dustPoolWolf.Add(obj);
        }

        for (int i = 0; i < 300; i++)
        {
            ParticleSystem obj = Instantiate(m_CrackWolf);
            obj.gameObject.SetActive(true);
            crackPoolWolf.Add(obj);
        }

        for (int i = 0; i < 150; i++)
        {
            ParticleSystem obj = Instantiate(m_SmokeWolf);
            obj.gameObject.SetActive(true);
            smokePoolWolf.Add(obj);
        }

        for (int i = 0; i < 300; i++)
        {
            ParticleSystem obj = Instantiate(m_DustHorse);
            obj.gameObject.SetActive(true);
            dustPoolHorse.Add(obj);
        }

        for (int i = 0; i < 300; i++)
        {
            ParticleSystem obj = Instantiate(m_CrackHorse);
            obj.gameObject.SetActive(true);
            crackPoolHorse.Add(obj);
        }

        for (int i = 0; i < 150; i++)
        {
            ParticleSystem obj = Instantiate(m_SmokeHorse);
            obj.gameObject.SetActive(true);
            smokePoolHorse.Add(obj);
        }

        for (int i = 0; i < 300; i++)
        {
            ParticleSystem obj = Instantiate(m_DustBear);
            obj.gameObject.SetActive(true);
            dustPoolBear.Add(obj);
        }

        for (int i = 0; i < 300; i++)
        {
            ParticleSystem obj = Instantiate(m_CrackBear);
            obj.gameObject.SetActive(true);
            crackPoolBear.Add(obj);
        }

        for (int i = 0; i < 150; i++)
        {
            ParticleSystem obj = Instantiate(m_SmokeBear);
            obj.gameObject.SetActive(true);
            smokePoolBear.Add(obj);
        }
    }
}
