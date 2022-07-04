using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSpawner : MonoBehaviour
{
    //ObjectPooler
    ObjectPooler m_ObjectPooler;

    /*
     * Start is called before the first frame update
     */
    public void StartWolfSpawner()
    {
        m_ObjectPooler = ObjectPooler.Instance;
    }

    /*
     * Summary:
     *     create new horse from pool
     * Parameters: 
     *     _localPositionX: local x position of animal
     * Returns:
     *     a wolf object
     */
    public GameObject AddNewWolf(Vector3 _localPosition)
    {
        return m_ObjectPooler.SpawnFromPool("Wolf", _localPosition);
    }

    /*
     * Summary:
     *     remove horses from game
     * Parameters:
     *     wolfs which will be removed
     */
    public void RemoveWolf(GameObject _obj)
    {
        m_ObjectPooler.RemoveAnimalFromGame("Wolf", _obj);
    }
}
