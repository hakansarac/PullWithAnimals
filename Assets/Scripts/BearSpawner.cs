using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSpawner : MonoBehaviour
{
    //ObjectPooler
    ObjectPooler m_ObjectPooler;

    [SerializeField] AnimalManager m_AnimalManager;

    /*
     * Start is called before the first frame update
     */
    public void StartBearSpawner()
    {
        m_ObjectPooler = ObjectPooler.Instance;
    }

    /*
     * Summary:
     *     create new horse from pool
     * Parameters: 
     *     _localPositionX: local x position of animal
     * Returns:
     *     a bear object
     */
    public GameObject AddNewBear(Vector3 _localPosition)
    {
        if(m_AnimalManager.m_Animals.Count < 4)
            return m_ObjectPooler.SpawnFromPool("Bear", _localPosition);
        else if(m_AnimalManager.m_Animals.Count < 8)
            return m_ObjectPooler.SpawnFromPool("Bear", _localPosition+Vector3.forward);
        else
            return m_ObjectPooler.SpawnFromPool("Bear", _localPosition + (Vector3.forward * 2));
    }

    /*
     * Summary:
     *     remove horses from game
     * Parameters:
     *     bears which will be removed
     */
    public void RemoveBear(GameObject _obj)
    {
        m_ObjectPooler.RemoveAnimalFromGame("Bear", _obj);
    }
}
