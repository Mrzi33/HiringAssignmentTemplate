using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    GameObject platformPrefab;

    [SerializeField]
    GameObject finishPrefab;

    Level currentLevel;

    [SerializeField]
    int emptyPlatformCount = 1;

    [SerializeField]
    Vector3 firstPlatformPosition;
    List<Transform> platforms;
    Stack<GameObject> inactivePlatforms;

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

    private void OnGameStart()
    {
        currentLevel = GameManager.Instance.GetCurrentLevel();
        List<PlatformSingle> individualPlatforms = currentLevel.platforms;
        platforms = new List<Transform>();
        currentPlatformIndex = 0;
        Vector3 platformPosition = firstPlatformPosition;
        Vector3 platformLength = platformPrefab.GetComponent<Platform>().getLength();



        for (int i = 0; i < emptyPlatformCount; i++)
        {
            GameObject newPlatform = GetNewPlatform();
            newPlatform.GetComponent<Platform>().SetPlatform(null, null, 0, false);
            newPlatform.transform.position = platformPosition;
            platforms.Add(newPlatform.GetComponent<Transform>());
            platformPosition += platformLength;
        }
        foreach (PlatformSingle platform in individualPlatforms)
        {
            GameObject newPlatform = GetNewPlatform();
            newPlatform.GetComponent<Platform>().SetPlatform(platform.calculationLeft, platform.calculationRight, platform.enemyCount, platform.gateEnable);
            newPlatform.transform.position = platformPosition;
            platforms.Add(newPlatform.GetComponent<Transform>());
            platformPosition += platformLength;
        }

        GameObject finishPlatform = GetFinishPlatform();
        finishPlatform.transform.position = platformPosition;
        

    }

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


    public void OnCalculationEnter(bool left){
        Calculation selectedCalculation;
        if (left){
            selectedCalculation =  currentLevel.platforms[currentPlatformIndex].calculationLeft;
        }else{
            selectedCalculation =  currentLevel.platforms[currentPlatformIndex].calculationRight;
        }
        PlayerGroupManager.Instance.GateEntered(selectedCalculation);


        Debug.Log("Current Platform Index: " + currentPlatformIndex);
    }

    public void OnEnemyGroupEnter(){
        bool playerSurvived = PlayerGroupManager.Instance.EnemyGateEntered(currentLevel.platforms[currentPlatformIndex].enemyCount);
        if(playerSurvived){
            platforms[currentPlatformIndex + emptyPlatformCount].gameObject.GetComponent<Platform>().DisableEnemies();

        }

        currentPlatformIndex++;
    }

}
