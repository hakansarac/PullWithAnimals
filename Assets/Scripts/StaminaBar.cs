using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    //Slider
    [SerializeField] private Slider m_StaminaBar;

    [SerializeField] private Image m_FillImage;

    public Color m_FullFill = Color.green;
    public Color m_HighFill = Color.green;
    public Color m_MidFill = Color.yellow;
    public Color m_LowFill = Color.red;
    public Color m_EmptyFill = Color.red;

    //Max stamina
    private float m_MaxStamina = 100f;

    //Current stamina
    public float m_CurStamina;

    //Regen stamina bar coroutine
    private Coroutine m_Regen;

    //Regen tick duration
    private WaitForSeconds m_RegenTick = new WaitForSeconds(0.1f);

    public static StaminaBar Instance;

    private void Awake()
    {
        Instance = this;        
    }

    // Start is called before the first frame update
    public void StartStaminaBar()
    {
        m_CurStamina = m_MaxStamina;
        m_StaminaBar.maxValue = m_MaxStamina;
        m_StaminaBar.value = m_MaxStamina;
    }

    private void Update()
    {
        if(GameManager.Instance.state != GameManager.State.PLAY)
        {
            m_CurStamina = m_MaxStamina;
            m_StaminaBar.maxValue = m_MaxStamina;
            m_StaminaBar.value = m_MaxStamina;
            m_FillImage.color = m_FullFill;
        }
    }


    public void UseStamina(float _amount)
    {
        if(m_CurStamina - _amount >= 0f)
        {
            m_CurStamina -= _amount;
            m_StaminaBar.value = m_CurStamina;
            if (m_CurStamina < 20)
            {
                m_FillImage.color = Color.Lerp(m_FillImage.color,m_EmptyFill,0.01f);
            }
            else if (m_CurStamina < 40)
            {
                m_FillImage.color = Color.Lerp(m_FillImage.color, m_LowFill, 0.01f);
            }
            else if (m_CurStamina < 60)
            {
                m_FillImage.color = Color.Lerp(m_FillImage.color, m_MidFill, 0.01f);
            }
            else if ( m_CurStamina < 80)
            {
                m_FillImage.color = Color.Lerp(m_FillImage.color, m_HighFill, 0.01f);
            }

            if (m_Regen != null)
                StopCoroutine(m_Regen);

            m_Regen = StartCoroutine(RegenStamina());
        }
        else
        {
            GameManager.Instance.SwitchState(GameManager.State.TRYAGAIN);
        }
    }


    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(1);

        while(m_CurStamina < m_MaxStamina)
        {
            m_CurStamina += m_MaxStamina / 100;
            m_StaminaBar.value = m_CurStamina;
            if (m_CurStamina > 80)
            {
                m_FillImage.color = Color.Lerp(m_FillImage.color, m_FullFill, 0.02f);
            }
            else if (m_CurStamina > 60)
            {
                m_FillImage.color = Color.Lerp(m_FillImage.color, m_HighFill, 0.02f);
            }
            else if (m_CurStamina > 40)
            {
                m_FillImage.color = Color.Lerp(m_FillImage.color, m_MidFill, 0.02f);
            }
            else if (m_CurStamina > 20)
            {
                m_FillImage.color = Color.Lerp(m_FillImage.color, m_LowFill, 0.02f);
            }
            yield return m_RegenTick;
        }
        m_Regen = null;
    }

  
}
