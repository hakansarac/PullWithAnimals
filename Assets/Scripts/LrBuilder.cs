using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LrBuilder : MonoBehaviour
{

    //A line object
    [SerializeField] private LineController m_Line;

    //A list of all lines
    public List<LineController> m_LineControllerList = new List<LineController>();


    /*
     * Summary:
     *     is called to generate a new line between vehicle and new animal
     * Parameters:
     *     _newAnimal: new generated animal 
     */     
    public void Build(GameObject _newAnimal)
    {        
        LineController newLine = Instantiate(m_Line);
        m_LineControllerList.Add(newLine);
        Transform[] points = { this.transform, _newAnimal.transform};
        newLine.SetUpLine(points);
    }

    /*
     * Summary:
     *     is called to remove all of ropes
     */     
    public void Remove()
    {
        //for(int i = 0; i<10; i++)
        for (int i = 0; i < m_LineControllerList.Count; i++)
        {
            m_LineControllerList[i].RemoveLineRenderer();
            m_LineControllerList.RemoveAt(i);
        }
            
    }

    public void DestroyVehicle()
    {
        Remove();
        
        Destroy(this.gameObject);
    }

}
