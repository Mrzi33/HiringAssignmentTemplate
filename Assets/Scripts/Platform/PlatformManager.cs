using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class PlatformSingle{
    public Calculation calculationLeft;
    public Calculation calculationRight;
    public int enemyCount;
    public bool gateEnable;
}

public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    GameObject platformPrefab;
    [SerializeField]
    List<PlatformSingle> individualPlatforms;
    [SerializeField]
    int emptyPlatformCount = 1;

    [SerializeField]
    Vector3 firstPlatformPosition;
    List<Platform> platforms = new List<Platform>();

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
        platforms = new List<Platform>();
        currentPlatformIndex = 0;
        Vector3 platformPosition = firstPlatformPosition;
        Vector3 platformLength = platformPrefab.GetComponent<Platform>().getLength();

        for (int i = 0; i < emptyPlatformCount; i++)
        {
            GameObject newPlatform = Instantiate(platformPrefab, platformPosition, Quaternion.identity);
            newPlatform.GetComponent<Platform>().SetPlatform(null, null, 0, false);
            newPlatform.transform.parent = this.transform;
            platforms.Add(newPlatform.GetComponent<Platform>());
            platformPosition += platformLength;
        }
        foreach (PlatformSingle platform in individualPlatforms)
        {
            GameObject newPlatform = Instantiate(platformPrefab, platformPosition, Quaternion.identity);
            newPlatform.GetComponent<Platform>().SetPlatform(platform.calculationLeft, platform.calculationRight, platform.enemyCount, platform.gateEnable);
            newPlatform.transform.parent = this.transform;
            platforms.Add(newPlatform.GetComponent<Platform>());
            platformPosition += platformLength;
        }
    }

    private void OnGameRestart()
    {
        //disable all platforms
        foreach (Platform platform in platforms)
        {
            platform.gameObject.SetActive(false);
        }
    }


    public void OnCalculationEnter(bool left){
        Calculation selectedCalculation;
        if (left){
            selectedCalculation =  individualPlatforms[currentPlatformIndex].calculationLeft;
        }else{
            selectedCalculation =  individualPlatforms[currentPlatformIndex].calculationRight;
        }
        PlayerGroupManager.Instance.GateEntered(selectedCalculation);


        Debug.Log("Current Platform Index: " + currentPlatformIndex);
    }

    public void OnEnemyGroupEnter(){
        bool playerSurvived = PlayerGroupManager.Instance.EnemyGateEntered(individualPlatforms[currentPlatformIndex].enemyCount);
        if(playerSurvived){
            platforms[currentPlatformIndex + emptyPlatformCount].DisableEnemies();
        }

        currentPlatformIndex++;
    }

}
