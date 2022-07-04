using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterSlider : MonoBehaviour
{

    [SerializeField] private Slider m_MeterBar;

    private float m_MaxMeter = 100f;

    public float m_CurMeter;
    // Start is called before the first frame update
    void Start()
    {
        m_CurMeter = 0f;
        m_MeterBar.maxValue = m_MaxMeter;
        m_MeterBar.value = m_MeterBar.minValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state != GameManager.State.PLAY)
        {
            m_CurMeter = 0f;
            m_MeterBar.value = 0f;
        }

        else
        {
            m_CurMeter = AnimalManager.Instance.transform.position.z;
            m_MeterBar.value = m_CurMeter;
        }
    }
}
