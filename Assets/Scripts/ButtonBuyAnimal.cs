using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ButtonBuyAnimal : MonoBehaviour
{
    //A button object
    private Button m_Button;

    public int m_PriceNew;

    //Animal Manager
    [SerializeField] private AnimalManager m_AnimalManager;
    [SerializeField] private TextMeshProUGUI m_TextBuyPrice;

    private void Awake()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(ButtonClick);
    }

    private void Start()
    {
        m_PriceNew = PlayerPrefs.GetInt("addAnimal");
        if (m_PriceNew < 15) m_PriceNew = 15;
        m_TextBuyPrice.SetText(m_PriceNew.ToString());
    }

    private void LateUpdate()
    {
        if (m_AnimalManager.GetAnimalCount() == 10 || m_PriceNew > GoldManager.Instance.TotalGold)
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
     *     is called to buy animal by button
     */
    private void ButtonClick()
    {
        GoldManager.Instance.TotalGold -= m_PriceNew;
        PlayerPrefs.SetFloat("totalGold", GoldManager.Instance.TotalGold);
        m_PriceNew += (m_PriceNew / 10) + 2;
        m_TextBuyPrice.SetText(m_PriceNew.ToString());
        PlayerPrefs.SetInt("addAnimal",m_PriceNew);
        m_AnimalManager.AddNewAnimal(false);
    }

}
