using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//manages the platforms, their setup, pooling
public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    GameObject platformPrefab;

    [SerializeField]
    GameObject finishPrefab;


    //starting position of the first platform
    [SerializeField]
    Vector3 firstPlatformPosition;
    List<Transform> platforms;
    Stack<GameObject> inactivePlatforms;
    Level currentLevel;


    int emptyPlatformCount = 1;
    int currentPlatformIndex = 0;
    GameObject finishPlatform;

    public static PlatformManager Instance;


    private void Awake() {
        if(Instance != null && Instance != this){
            Destroy(this.gameObject);
        }else{
            Instance = this;
        }
        inactivePlatforms = new Stack<GameObject>();
        

    }

    private void Start() {
        GameManager.Instance.OnGameStart.AddListener(OnGameStart);
        GameManager.Instance.OnGameRestart.AddListener(OnGameRestart);
    }

    //creates platforms based on level data
    private void OnGameStart()
    {
        currentLevel = GameManager.Instance.GetCurrentLevel();
        List<PlatformSingle> individualPlatforms = currentLevel.platforms;
        platforms = new List<Transform>();
        currentPlatformIndex = 0;
        Vector3 platformPosition = firstPlatformPosition;
        Vector3 platformLength = platformPrefab.GetComponent<Platform>().getLength();


        //create empty platforms at the beginning
        for (int i = 0; i < emptyPlatformCount; i++)
        {
            GameObject newPlatform = GetNewPlatform();
            newPlatform.GetComponent<Platform>().SetPlatform(null, null, 0, false);
            newPlatform.transform.position = platformPosition;
            platforms.Add(newPlatform.GetComponent<Transform>());
            platformPosition += platformLength;
        }
        //create platforms with calculations and enemies
        foreach (PlatformSingle platform in individualPlatforms)
        {
            GameObject newPlatform = GetNewPlatform();
            newPlatform.GetComponent<Platform>().SetPlatform(platform.calculationLeft, platform.calculationRight, platform.enemyCount, platform.gateEnable);
            newPlatform.transform.position = platformPosition;
            platforms.Add(newPlatform.GetComponent<Transform>());
            platformPosition += platformLength;
        }
        //create finish platform
        GameObject finishPlatform = GetFinishPlatform();
        finishPlatform.transform.position = platformPosition;
        

    }

    // returns a platform from the pool or creates a new one
    private GameObject GetNewPlatform(){
        if(inactivePlatforms.Count == 0){
            GameObject newPlatform = Instantiate(platformPrefab);
            newPlatform.transform.parent = this.transform;
            return newPlatform;
        }else{
            GameObject platform = inactivePlatforms.Pop();
            platform.SetActive(true);
            return platform;
        }
    }

    //returns all platforms to the pool
    private void ReturnPlatforms(){
        foreach (Transform platform in platforms)
        {
            platform.GetComponent<Platform>().DisableEnemies();
            platform.gameObject.SetActive(false);
            inactivePlatforms.Push(platform.gameObject);
        }
        platforms.Clear();
        finishPlatform.SetActive(false);
    }


    //returns the finish platform from the pool or creates a new one
    private GameObject GetFinishPlatform(){
        if(finishPlatform == null){
            finishPlatform = Instantiate(finishPrefab);
            finishPlatform.transform.parent = this.transform;
        }
        finishPlatform.SetActive(true);
        return finishPlatform;
    }



    private void OnGameRestart()
    {
        ReturnPlatforms();
    }

    //triggered by gate when player enters, recalculates health of the player
    public void OnCalculationEnter(bool left){
        Calculation selectedCalculation;
        if (left){
            selectedCalculation =  currentLevel.platforms[currentPlatformIndex].calculationLeft;
        }else{
            selectedCalculation =  currentLevel.platforms[currentPlatformIndex].calculationRight;
        }
        PlayerGroupManager.Instance.GateEntered(selectedCalculation);
    }

    //triggered by gate when player enters, disables enemies on the platform if the player survived
    public void OnEnemyGroupEnter(){
        bool playerSurvived = PlayerGroupManager.Instance.EnemyGateEntered(currentLevel.platforms[currentPlatformIndex].enemyCount);
        if(playerSurvived){
            platforms[currentPlatformIndex + emptyPlatformCount].gameObject.GetComponent<Platform>().DisableEnemies();

        }

        currentPlatformIndex++;
    }

}
