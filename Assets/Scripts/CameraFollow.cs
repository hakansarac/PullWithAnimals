using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //An object of animals
    [SerializeField] private AnimalManager animals;

    public Transform[] m_WolfTransforms;

    public Transform[] m_HorseTransforms;

    public Transform[] m_BearTransforms;

    //Speed of Camera
    public float m_Speed;

    //Camera position according to animals
    private Vector3 m_Offset;

    private int m_StartAnimalNum;


    /*
     * Start is called before the first frame update
     * Summary:
     *     Setting camera position according to the position of animals
     */
    public void StartCamera()
    {
        m_StartAnimalNum = PlayerPrefs.GetInt("animalCount");
        if (m_StartAnimalNum == 0) m_StartAnimalNum = 1;
        switch (PlayerPrefs.GetString("typeAnimal"))
        {
            case "HORSE":
                this.transform.position = m_HorseTransforms[m_StartAnimalNum - 1].position;
                this.transform.rotation = m_HorseTransforms[m_StartAnimalNum - 1].rotation;
                break;
            
            case "BEAR":
                this.transform.position = m_BearTransforms[m_StartAnimalNum - 1].position;
                this.transform.rotation = m_BearTransforms[m_StartAnimalNum - 1].rotation;
                break;

            default:
                this.transform.position = m_WolfTransforms[m_StartAnimalNum - 1].position;
                this.transform.rotation = m_WolfTransforms[m_StartAnimalNum - 1].rotation;
                break;
        }
        m_Offset = this.transform.position - animals.transform.position;
    }


    /*
     * LateUpdate is called every frame, if the Behaviour is enabled
     * Summary:
     *     Movement of camera smoothly
     */     
    private void LateUpdate()
    {
        Vector3 playerPos = animals.transform.position;
        Vector3 desiredPosition = playerPos + m_Offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, m_Speed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    
    public IEnumerator ChangeCamera()
    {
        float timeLapse = 0f;
        float totalTime = 0.5f;
        Vector3 currPos = this.transform.position;

        while (timeLapse <= totalTime)
        {
            switch (animals.m_TypeAnimal)
            {
                case AnimalManager.State.WOLF:
                    this.transform.position = Vector3.Lerp(currPos, m_WolfTransforms[animals.m_Animals.Count - 1].position, timeLapse / totalTime);
                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, m_WolfTransforms[animals.m_Animals.Count - 1].rotation, timeLapse / totalTime);
                    timeLapse += Time.deltaTime;
                    yield return null;
                    break;
                case AnimalManager.State.HORSE:
                    this.transform.position = Vector3.Lerp(currPos, m_HorseTransforms[animals.m_Animals.Count - 1].position, timeLapse / totalTime);
                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, m_HorseTransforms[animals.m_Animals.Count - 1].rotation, timeLapse / totalTime);
                    timeLapse += Time.deltaTime;
                    yield return null;
                    break;
                case AnimalManager.State.BEAR:
                    this.transform.position = Vector3.Lerp(currPos, m_BearTransforms[animals.m_Animals.Count - 1].position, timeLapse / totalTime);
                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, m_BearTransforms[animals.m_Animals.Count - 1].rotation, timeLapse / totalTime);
                    timeLapse += Time.deltaTime;
                    yield return null;
                    break;
            }
            
        }

        switch (animals.m_TypeAnimal)
        {
            case AnimalManager.State.WOLF:
                this.transform.position = m_WolfTransforms[animals.m_Animals.Count - 1].position;
                this.transform.rotation = m_WolfTransforms[animals.m_Animals.Count - 1].rotation;
                break;
            case AnimalManager.State.HORSE:
                this.transform.position = m_HorseTransforms[animals.m_Animals.Count - 1].position;
                this.transform.rotation = m_HorseTransforms[animals.m_Animals.Count - 1].rotation;
                break;
            case AnimalManager.State.BEAR:
                this.transform.position = m_BearTransforms[animals.m_Animals.Count - 1].position;
                this.transform.rotation = m_BearTransforms[animals.m_Animals.Count - 1].rotation;
                break;
        }
        m_Offset = this.transform.position - animals.transform.position;
    }
}
