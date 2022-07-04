using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGold : MonoBehaviour
{
    //A button object
    private Button m_Button;

    //Gold Manager
    [SerializeField] private GoldManager m_GoldManager;

    private void Awake()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(ButtonClick);
    }

    private void LateUpdate()
    {
        if (GoldManager.Instance.PriceIncome > GoldManager.Instance.TotalGold)
        {
            transform.GetComponent<Button>().interactable = false;
        }
        else
        {
            transform.GetComponent<Button>().interactable = true;
        }
    }

    /*
     * Summary:
     *     is called to increase income by button
     */
    private void ButtonClick()
    {
        m_GoldManager.IncreaseGoldPerMeter();
    }
}
