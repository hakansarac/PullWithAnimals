using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    //An object of Line Renderer
    private LineRenderer m_LineRenderer;

    //Transforms of two ends
    private Transform[] m_Points;


    //Awake is called when the script instance is being loaded
    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }



    /*
     * Summary: 
     *     SetUpLine is called to set up line according to selected points
     * Parameters:
     *     _points: transform array of LineRenderer points
     */
    public void SetUpLine(Transform[] _points)
    {        
        m_LineRenderer.positionCount = _points.Length;
        this.m_Points = _points;
    }



    /* 
     * Update is called once per frame
     * Summary:
     *     update line positions per frame
     */
    private void Update()
    {
        if(m_Points == null)
        {
            return;
        }
        for(int i=0; i < m_Points.Length; i++)
        {
            m_LineRenderer.SetPosition(i, m_Points[i].position);
        }
    }

    /*
     * Summary:
     *     is called to remove line renderer
     */     
    public void RemoveLineRenderer()
    {
        m_Points = null;
        m_LineRenderer.positionCount = 0;
    } 
}
