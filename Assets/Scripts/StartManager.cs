using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    [SerializeField] AnimalManager animalManager;
    [SerializeField] ObjectPooler objectPooler;
    [SerializeField] HorseSpawner horseSpawner;
    [SerializeField] WolfSpawner wolfSpawner;
    [SerializeField] BearSpawner bearSpawner;
    [SerializeField] GameManager gameManager;
    [SerializeField] CameraFollow cameraFollow;
    [SerializeField] StaminaBar staminaBar;
     
    // Start is called before the first frame update
    void Start()
    {
        cameraFollow.StartCamera();
        objectPooler.StartObjectPooler();
        gameManager.StartGameManager();
        wolfSpawner.StartWolfSpawner();
        horseSpawner.StartHorseSpawner();
        bearSpawner.StartBearSpawner();
        staminaBar.StartStaminaBar();
        animalManager.StartAnimalManager();  
    }
}
