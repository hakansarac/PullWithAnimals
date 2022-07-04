using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimalManager : MonoBehaviour
{

    //HorseSpawner
    [SerializeField] private HorseSpawner m_HorseSpawner;

    //WolfSpawner
    [SerializeField] private WolfSpawner m_WolfSpawner;

    //BearSpawner
    [SerializeField] private BearSpawner m_BearSpawner;

    [SerializeField] private CameraFollow m_CameraFollow;

    [SerializeField] private StaminaBar m_StaminaBar;

    //List of active animals
    public List<GameObject> m_Animals = new List<GameObject>();

    //A line renderer object

    private Vector3[] m_AnimalPositions = {
        new Vector3(0,-1,0),
        new Vector3(-1,-1, 0),
        new Vector3(3, -1, 0),
        new Vector3(-3, -1, 0),
        new Vector3(0, -1, 3),
        new Vector3(-1, -1, 3),
        new Vector3(3, -1, 3),
        new Vector3(-3, -1, 3),
        new Vector3(1, -1, 6),
        new Vector3(-1, -1, 6),
    };

    //Types of animals
    public enum State { WOLF, HORSE, BEAR};
    public State m_TypeAnimal;
        
    //Number of active animals
    private int m_NumOfAnimals;

    //Power of active animals
    public float m_PowerAnimals;

    //pref number
    private int m_PrefNum;

    private float[] m_VehicleWeight = { 2f,15f,40f,120f,450f};



    //Speed of animals
    [SerializeField] private float m_SpeedAnimals;

    [SerializeField] private TextMeshProUGUI m_TextAnimalCount;

    //Singleton to create only one ObjectPooler object 
    #region Singleton
    public static AnimalManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    //end singleton


    // Start is called before the first frame update
    public void StartAnimalManager()
    {
        string type = PlayerPrefs.GetString("typeAnimal");
        switch (type)
        {
            case "WOLF":
                m_TypeAnimal = State.WOLF;
                break;
            case "HORSE":
                m_TypeAnimal = State.HORSE;
                break;
            case "BEAR":
                m_TypeAnimal = State.BEAR;
                break;
        }

        m_PrefNum = PlayerPrefs.GetInt("animalCount");
        if (m_PrefNum == 0) m_PrefNum = 1;
        m_PowerAnimals = 0;
        //m_TypeAnimal = State.BEAR;
        //m_PrefNum = 5;
        for (int i= 0; i<m_PrefNum; i++) AddNewAnimal(true);
        m_TextAnimalCount.SetText(m_Animals.Count.ToString() + " / 10");
    }

    private void Update()
    {
        if (m_PrefNum == 0) m_PrefNum = 1;
    }

    /* 
    * LateUpdate is called every frame, if the Behaviour is enabled
    * Summary:
    *     calls Move and SetSpeedVehicles methods
    */
    private void LateUpdate()
    {
        SetSpeed();
        if(GameManager.Instance.state == GameManager.State.PLAY)
            Move();
        else
        {
            for (int i = 0; i < m_Animals.Count; i++)
                m_Animals[i].GetComponent<Animator>().Play("Idle");
        }
    }

    /*
    * Summary:
    *     returns number of active animals
    */
    public int GetAnimalCount()
    {
        return m_NumOfAnimals;
    }

    /*
     * Summary:
     *     is called to set speed of animals and the vehicle
     */
    private void SetSpeed()
    {
        int level = GameManager.Instance.CurrentLevel;
        m_SpeedAnimals = Mathf.Clamp(m_PowerAnimals / m_VehicleWeight[level], 0.3f, 1.2f);
        //m_SpeedAnimals = 1f;
    }

    /*
     * Summary:
     *     returns speed of animals and the vehicle
     */
    public float GetSpeed()
    {
        return m_SpeedAnimals;
    }


    /*
     * Summary:
     *     movement of vehicles
     */
    private void Move()
    {
        if (Input.touchCount > 0)
        {
            Touch first = Input.GetTouch(0);
            if (first.phase == TouchPhase.Stationary || first.phase == TouchPhase.Moved || first.phase == TouchPhase.Began)
            {
                for (int i = 0; i < m_Animals.Count; i++)
                {
                    if (m_TypeAnimal == State.WOLF)
                    {
                        m_Animals[i].GetComponent<Animator>().Play("rig_wolf_walk");
                        m_Animals[i].GetComponent<Animator>().SetFloat("walkSpeed", Time.deltaTime * 120 * m_SpeedAnimals);
                    }
                        
                    else if (m_TypeAnimal == State.HORSE)
                    {
                        m_Animals[i].GetComponent<Animator>().Play("rig_001_horse_walk");
                        m_Animals[i].GetComponent<Animator>().SetFloat("walkSpeed", Time.deltaTime * 100 * m_SpeedAnimals);
                    }
                        
                    else if (m_TypeAnimal == State.BEAR)
                    {
                        m_Animals[i].GetComponent<Animator>().Play("rig_bear_walk");
                        m_Animals[i].GetComponent<Animator>().SetFloat("walkSpeed", Time.deltaTime * 120 * m_SpeedAnimals);
                    }                        
                }

                transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward, m_SpeedAnimals * Time.deltaTime);
                RotateWheels();
                StaminaUsage();
                
                

                if(transform.position.z > 100f)
                {
                    GameManager.Instance.SwitchState(GameManager.State.LEVELCOMPLETED);
                }
            }
            
            
        }
        else
        {
            if (StaminaBar.Instance.m_CurStamina < 60)
            {
                for (int i = 0; i < m_Animals.Count; i++)
                {
                    m_Animals[i].GetComponent<Animator>().Play("tired");
                }
            }
            else
            {
                for (int i = 0; i < m_Animals.Count; i++)
                {
                    m_Animals[i].GetComponent<Animator>().Play("Idle");
                }
            }

        }
    }
    

    /*
     * Summary:
     *     is called to create new animal from pool
     */     
    public void AddNewAnimal(bool _isStart)
    {
        
        switch (m_TypeAnimal)
        {
            case State.WOLF:
                m_Animals.Add(m_WolfSpawner.AddNewWolf(m_AnimalPositions[m_NumOfAnimals]));
                PlayerPrefs.SetString("typeAnimal", "WOLF");
                m_PowerAnimals += 0.4f;
                break;
            case State.HORSE:
                m_Animals.Add(m_HorseSpawner.AddNewHorse(m_AnimalPositions[m_NumOfAnimals]));
                m_PowerAnimals += 5f;
                break;
            case State.BEAR:
                m_Animals.Add(m_BearSpawner.AddNewBear(m_AnimalPositions[m_NumOfAnimals]));
                m_PowerAnimals += 55f;
                break;
        }

        if(!_isStart)
            StartCoroutine(m_CameraFollow.ChangeCamera());

        GameManager.m_CurrentVehicle.Build(m_Animals[m_Animals.Count-1]);
        m_NumOfAnimals++;
        PlayerPrefs.SetInt("animalCount", m_NumOfAnimals);
        m_TextAnimalCount.SetText(m_Animals.Count.ToString() + " / 10");
    } 

    /*
     * Summary:
     *     is called to remove all animals and create new upgraded animal
     */     
    public void EvolveAnimal()
    {
        for(int i=m_NumOfAnimals-1; i>=0; i--)
        {
            switch (m_TypeAnimal)
            {
                case State.WOLF:
                    m_WolfSpawner.RemoveWolf(m_Animals[i]);
                    break;
                case State.HORSE:
                    m_HorseSpawner.RemoveHorse(m_Animals[i]);
                    break;
                case State.BEAR:
                    return;
            }
            m_Animals.RemoveAt(i);
        }
        GameManager.m_CurrentVehicle.Remove();
        m_NumOfAnimals = 0;
        m_PrefNum = 0;
        m_PowerAnimals = 0;
        ChangeAnimalState();
    }


    /*
     * Summary:
     *     is called to change type of the animal
     */     
    private void ChangeAnimalState()
    {
        switch (m_TypeAnimal)
        {
            case State.WOLF:
                m_TypeAnimal = State.HORSE;
                PlayerPrefs.SetString("typeAnimal", "HORSE");
                AddNewAnimal(false);
                break;
            case State.HORSE:
                m_TypeAnimal = State.BEAR;
                PlayerPrefs.SetString("typeAnimal", "BEAR");
                AddNewAnimal(false);
                break;
        }
    }


    /*
     * Summary:
     *     Rotation speed of wheels
     */
    private void RotateWheels()
    {
        GameObject vehicle = FindVehicle.FindGameObjectInChildWithTag(this.gameObject,"Vehicle");
        foreach (Transform eachChild in vehicle.transform)
        {
            if (eachChild.name == "Wheels")
            {
                if(GameManager.Instance.CurrentLevel == 4)
                {
                    eachChild.GetChild(0).GetChild(0).GetChild(0).transform.RotateAround(eachChild.GetChild(0).GetChild(0).GetChild(0).transform.position, transform.right, Time.deltaTime * 360f / 5 * m_SpeedAnimals);
                }
                else
                {
                    eachChild.GetChild(0).GetChild(0).transform.RotateAround(eachChild.GetChild(0).GetChild(0).transform.position, transform.right, Time.deltaTime * 360f / 5 * m_SpeedAnimals);
                    eachChild.GetChild(0).GetChild(1).transform.RotateAround(eachChild.GetChild(0).GetChild(1).transform.position, transform.right, Time.deltaTime * 360f / 5 * m_SpeedAnimals);
                }
                
            }
        }

    }

    /*
     * Summary:
     *     is called to create new ropes for new vehicle
     */     
    public void NewVehicleRope()
    {
        for(int i= 0; i<m_Animals.Count; i++)
            GameManager.m_CurrentVehicle.Build(m_Animals[i]);
    }

    private void StaminaUsage()
    {
        float stamina = 0.08f - (0.004f * (m_Animals.Count-1));

        StaminaBar.Instance.UseStamina(stamina);
    }
}