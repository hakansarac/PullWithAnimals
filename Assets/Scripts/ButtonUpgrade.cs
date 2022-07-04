using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonUpgrade : MonoBehaviour
{
    //A button object
    private Button m_Button;

    public int m_PriceEvolve;

    //Animal Manager
    [SerializeField] private AnimalManager m_AnimalManager;

    [SerializeField] private TextMeshProUGUI m_TextEvolvePrice;
    
    private void Awake()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(ButtonClick);
    }

    private void Start()
    {
        m_PriceEvolve = PlayerPrefs.GetInt("evolve");
        if (m_PriceEvolve < 80) m_PriceEvolve = 80;
        m_TextEvolvePrice.SetText(m_PriceEvolve.ToString());
    }

    private void LateUpdate()
    {
        if (PlayerPrefs.GetString("typeAnimal") == "BEAR" || m_AnimalManager.GetAnimalCount() < 10 || m_PriceEvolve > GoldManager.Instance.TotalGold)
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
     *     is called to evolve the animal type by button
     */
    private void ButtonClick()
    {
        GoldManager.Instance.TotalGold -= m_PriceEvolve;
        PlayerPrefs.SetFloat("totalGold", GoldManager.Instance.TotalGold);
        m_PriceEvolve *= 3;
        m_TextEvolvePrice.SetText(m_PriceEvolve.ToString());
        PlayerPrefs.SetInt("evolve", m_PriceEvolve);
        m_AnimalManager.EvolveAnimal();
    }
}
