using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //Pool class
    [System.Serializable]
    public class Pool
    {
        //tag is a key
        public string tag;

        //prefab of the relevant object
        public GameObject prefab;

        //size of object pool
        public int size;
    }
    //end pool class


    //Singleton to create only one ObjectPooler object 
    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    //end singleton


    //list of object pools which have different or same type
    public List<Pool> pools;

    //poolDictionary
    public Dictionary<string, List<GameObject>> poolDictionary;

    //Animal Manager to operate pools
    [SerializeField] private AnimalManager m_AnimalManager;



    /* 
     * Start is called before the first frame update
     * Summary:
     *     creating pools
     */     
    public void StartObjectPooler()
    {

        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach(Pool pool in pools)
        {
            List<GameObject> objectPool = new List<GameObject>();

            for(int i=0; i<pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Add(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

    }



    /*
     * Summary:
     *     is called to spawn animals in game
     * Parameters:
     *     _tag: to indicate GameObject type
     *     _localPositionX: local x value of new animal
     * Returns:
     *     an animal object
     */     
    public GameObject SpawnFromPool(string _tag,Vector3 _localPosition)
    {
        if (!poolDictionary.ContainsKey(_tag))
        {
            Debug.LogWarning("Pool with tag " + _tag + " does not exist.");
            return null;
        }
        if (poolDictionary[_tag].Count == 0)
        {
            return null;
        }

        GameObject objectToSpawn = poolDictionary[_tag][poolDictionary[_tag].Count-1];
        poolDictionary[_tag].RemoveAt(poolDictionary[_tag].Count - 1);

        Transform objectTransform = objectToSpawn.transform;

        objectTransform.SetParent(m_AnimalManager.transform);



        if (m_AnimalManager.m_Animals.Count == 1)
            m_AnimalManager.m_Animals[m_AnimalManager.m_Animals.Count - 1].transform.localPosition = new Vector3(1, -1, 0);
        else if (m_AnimalManager.m_Animals.Count == 5 && m_AnimalManager.m_TypeAnimal != AnimalManager.State.BEAR)
            m_AnimalManager.m_Animals[m_AnimalManager.m_Animals.Count - 1].transform.localPosition = new Vector3(1, -1, 3);
        else if(m_AnimalManager.m_Animals.Count == 5 && m_AnimalManager.m_TypeAnimal == AnimalManager.State.BEAR)
            m_AnimalManager.m_Animals[m_AnimalManager.m_Animals.Count - 1].transform.localPosition = new Vector3(1, -1, 3) + Vector3.forward;

        objectToSpawn.transform.localPosition =_localPosition;
        
        objectToSpawn.SetActive(true);

        return objectToSpawn;
    }


    /*
     * Summary:
     *     is called to remove animals from game
     * Parameters:
     *     _tag: animal type
     *     _obj: animal object
     */
    public void RemoveAnimalFromGame(string _tag,GameObject _obj)
    {
        poolDictionary[_tag].Add(_obj);
        _obj.SetActive(false);
    }

}
