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

    int currentPlatformIndex = 0;

    public static PlatformManager Instance;


    private void Awake() {
        if(Instance != null && Instance != this){
            Destroy(this.gameObject);
        }else{
            Instance = this;
        }
        

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
            GameObject newPlatform = Instantiate(platformPrefab, platformPosition, Quaternion.identity);
            newPlatform.GetComponent<Platform>().SetPlatform(null, null, 0, false);
            newPlatform.transform.parent = this.transform;
            platforms.Add(newPlatform.GetComponent<Transform>());
            platformPosition += platformLength;
        }
        foreach (PlatformSingle platform in individualPlatforms)
        {
            GameObject newPlatform = Instantiate(platformPrefab, platformPosition, Quaternion.identity);
            newPlatform.GetComponent<Platform>().SetPlatform(platform.calculationLeft, platform.calculationRight, platform.enemyCount, platform.gateEnable);
            newPlatform.transform.parent = this.transform;
            platforms.Add(newPlatform.GetComponent<Transform>());
            platformPosition += platformLength;
        }

        GameObject finishPlatform = Instantiate(finishPrefab, platformPosition, Quaternion.identity);
        finishPlatform.transform.parent = this.transform;
        platforms.Add(finishPlatform.GetComponent<Transform>());
        

    }

    private void OnGameRestart()
    {
        //disable all platforms
        foreach (Transform platform in platforms)
        {
            Destroy(platform.gameObject);
        }
        platforms = new List<Transform>();
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
