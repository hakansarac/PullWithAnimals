using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GoldManager : MonoBehaviour
{
    //Singleton to create only one GoldManager object 
    #region Singleton
    public static GoldManager Instance;

    [SerializeField] TextMeshProUGUI m_TextIncomePrice;
    [SerializeField] TextMeshProUGUI m_TextTotalGold;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    //end singleton


    //Total gold
    public float m_TotalGold; //private yapilcak
    public float TotalGold
    {
        get { return m_TotalGold; }
        set
        {
            m_TotalGold = value;
            //totalGoldText.text = "GOLD: " + score;
        }
    }

    //Gold earned per meter
    public float m_GoldPerMeter;

    //Z point where the player will earn gold  
    private int m_TargetZ;

    public int m_PriceIncome; //private yap
    public int PriceIncome
    {
        get { return m_PriceIncome; }
    }

    //Animal Manager
    [SerializeField] private AnimalManager m_AnimalManager;

    // Start is called before the first frame update
    private void Start()
    {
        m_TotalGold = PlayerPrefs.GetFloat("totalGold");
        m_GoldPerMeter = PlayerPrefs.GetFloat("goldPerMeter");
        m_PriceIncome = PlayerPrefs.GetInt("price");
        if (m_PriceIncome == 0) m_PriceIncome = 15;
        if (m_GoldPerMeter == 0) m_GoldPerMeter = 2;
        m_TargetZ = 1;
        m_TextIncomePrice.SetText(m_PriceIncome.ToString());
        m_TextTotalGold.SetText(m_TotalGold.ToString());
    }

     
    //Update is called once per frame    
    private void Update()
    {
        EarnGold();
    }

    /*
     * Summary:
     *     is called to earn gold and change the target point
     */     
    private void EarnGold()
    {        
        if(m_AnimalManager.transform.position.z >= m_TargetZ && GameManager.Instance.state == GameManager.State.PLAY)
        {
            m_TargetZ++;
            m_TotalGold += m_GoldPerMeter;
            m_TextTotalGold.SetText(m_TotalGold.ToString());
            PlayerPrefs.SetFloat("totalGold", m_TotalGold);
        }
        if(GameManager.Instance.state != GameManager.State.PLAY)
        {
            m_TargetZ = 1;
        }
                           
    }

    /*
     * Summary:
     *     is called to increase the income
     */     
    public void IncreaseGoldPerMeter()
    {
        m_TotalGold -= m_PriceIncome;
        PlayerPrefs.SetFloat("totalGold", m_TotalGold);
        m_PriceIncome += (m_PriceIncome / 10) +1;
        m_TextIncomePrice.SetText(m_PriceIncome.ToString());
        PlayerPrefs.SetInt("price", m_PriceIncome);
        m_GoldPerMeter = m_GoldPerMeter += 0.2f;
        PlayerPrefs.SetFloat("goldPerMeter", m_GoldPerMeter);
    }
}
