using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Singleton to create only one GameManager object 
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    //end singleton

    //Menu
    public GameObject panelMenu;
    //Play
    public GameObject panelPlay;
    //LevelCompleted
    public GameObject panelLevelCompleted;
    //Try Again
    public GameObject panelTryAgain;

    public LrBuilder[] vehicles;

    private Vector3[] m_VehiclesPosition =
    {
        new Vector3(0,-1,-7),
        new Vector3(0,-1, -5.5f),
        new Vector3(0, -1, -7),
        new Vector3(0, -1, -7),
        new Vector3(0, 1, -11),
    };

    public GameObject[] environments;

    [SerializeField] private AnimalManager m_AnimalManager;

    [SerializeField] private TextMeshProUGUI m_TextLevel;

    //Current vehicle
    public static LrBuilder m_CurrentVehicle;
    //Current level
    private int m_CurrentLevel;
    public int CurrentLevel
    {
        get { return m_CurrentLevel; }
    }

    //Game states
    public enum State { MENU, INIT, PLAY, LEVELCOMPLETED, LOADLEVEL, TRYAGAIN}
    public State state;

    //switching flag
    private bool m_IsSwitchingState;


    public void StartGameManager()
    {
        m_CurrentLevel = PlayerPrefs.GetInt("level");
        m_TextLevel.SetText("Level " + (m_CurrentLevel+1).ToString());
        m_CurrentVehicle = Instantiate(vehicles[m_CurrentLevel]);
        m_AnimalManager.transform.position = new Vector3(0, 0, 0);
        m_CurrentVehicle.transform.SetParent(m_AnimalManager.transform);
        m_CurrentVehicle.transform.position = m_VehiclesPosition[m_CurrentLevel];
        SwitchState(State.MENU);
    }

    public void SwitchState(State newState, float delay = 0)
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }

    IEnumerator SwitchDelay(State newState, float delay)
    {
        m_IsSwitchingState = true;
        yield return new WaitForSeconds(delay);
        EndState();
        state = newState;
        BeginState(newState);
        m_IsSwitchingState = false;
    }

    void BeginState(State newState)
    {
        switch (newState)
        {
            case State.MENU:       
                if (m_CurrentLevel < 2)
                    environments[0].SetActive(true);
                else
                {
                    environments[0].SetActive(false);
                    environments[1].SetActive(true);
                }
                //highscoreText.text = "HIGHSCORE: " + PlayerPrefs.GetInt("highscore");
                panelMenu.SetActive(true);
                break;
            case State.INIT:
                panelPlay.SetActive(true);
                SwitchState(State.LOADLEVEL);
                break;
            case State.PLAY:               
                break;
            case State.LEVELCOMPLETED:
                m_CurrentLevel++;
                m_TextLevel.SetText("Level " + (m_CurrentLevel + 1).ToString());
                PlayerPrefs.SetInt("level", m_CurrentLevel);
                panelLevelCompleted.SetActive(true);
                break;
            case State.LOADLEVEL:
                SwitchState(State.PLAY);
                break;
            case State.TRYAGAIN:
                panelTryAgain.SetActive(true);
                break;
        }
    }

    void EndState()
    {
        switch (state)
        {
            case State.MENU:
                panelMenu.SetActive(false);
                break;
            case State.INIT:
                break;
            case State.PLAY:
                panelPlay.SetActive(false);
                break;
            case State.LEVELCOMPLETED:
                m_CurrentVehicle.DestroyVehicle();  //it must be improved
                m_CurrentVehicle = Instantiate(vehicles[m_CurrentLevel]);
                m_AnimalManager.transform.position = new Vector3(0, 0, 0);
                m_CurrentVehicle.transform.SetParent(m_AnimalManager.transform);
                m_CurrentVehicle.transform.position = m_VehiclesPosition[m_CurrentLevel];//new Vector3(0, -1f, -5f);
                m_AnimalManager.NewVehicleRope();
                panelLevelCompleted.SetActive(false);
                break;
            case State.LOADLEVEL:
                break;
            case State.TRYAGAIN:
                m_AnimalManager.transform.position = new Vector3(0, 0, 0);
                m_CurrentVehicle.transform.SetParent(m_AnimalManager.transform);
                m_CurrentVehicle.transform.position = m_VehiclesPosition[m_CurrentLevel];
                panelTryAgain.SetActive(false);
                break;
        }
    }


  /*  // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                break;
            case State.LOADLEVEL:
                break;
            case State.TRYAGAIN:
                break;
        }
    }*/

    public void PlayClicked()
    {
        SwitchState(State.INIT);
    }

    public void ReturnMenu()
    {
        SwitchState(State.MENU);
    }
}
