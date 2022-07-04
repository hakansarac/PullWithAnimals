using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseSpawner : MonoBehaviour
{
    //ObjectPooler
    ObjectPooler m_ObjectPooler;

    /*
     * Start is called before the first frame update
     */
    public void StartHorseSpawner()
    {
        m_ObjectPooler = ObjectPooler.Instance;
    }

    /*
     * Summary:
     *     create new horse from pool
     * Parameters:
     *     _localPositionX: local x position of animal
     * Returns:
     *     a horse object
     */
    public GameObject AddNewHorse(Vector3 _localPosition)
    {
        return m_ObjectPooler.SpawnFromPool("Horse",_localPosition);
    }

    /*
     * Summary:
     *     remove horses from game
     * Parameters:
     *     horses which will be removed
     */
    public void RemoveHorse(GameObject _obj)
    {
        m_ObjectPooler.RemoveAnimalFromGame("Horse",_obj);
    }
}
