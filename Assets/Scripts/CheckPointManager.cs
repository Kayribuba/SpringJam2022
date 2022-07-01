using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointManager : MonoBehaviour
{
    static Vector3 SpawnPoint;
    static CheckPointManager instance;

    [HideInInspector] public int levelCreatedAt = -1;
    [HideInInspector] public Vector3 lastCheckpointPosition = new Vector3(1000001, 1000001, 1000001);

    [Header("Order is important. Top is the first CP. CPs must be child of this object")]
    public List<GameObject> checkpoints;

    public GameObject PlayerPrefab;

    void Start()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        CheckPointManager[] allCPMs = FindObjectsOfType<CheckPointManager>();

        EstablishCPMInstance(level, allCPMs);

        foreach (CheckPointManager cpManager in allCPMs)
        {
            if (cpManager != instance)
                Destroy(cpManager.gameObject);
        }
        if (instance.levelCreatedAt == -1)
        {
            instance.levelCreatedAt = level;
            SpawnPoint = new Vector3(1000001, 1000001, 1000001);
            DontDestroyOnLoad(instance);
        }

        SpawnPoint = instance.lastCheckpointPosition;

        if (SpawnPoint.x < 1000000 && SpawnPoint.y < 1000000 && SpawnPoint.z < 1000000)
        {
            GameObject player = FindObjectOfType<PlayerMovementScript>().gameObject;
            Destroy(player);
            Instantiate(PlayerPrefab, SpawnPoint, Quaternion.identity);
        }
    }

    private static void EstablishCPMInstance(int level, CheckPointManager[] allCPMs)
    {
        if (instance != null)
        {
            foreach (CheckPointManager cpManager in allCPMs)
            {
                if (cpManager == instance && cpManager.levelCreatedAt == level)
                    return;
                else
                    Destroy(cpManager.gameObject);
            }
        }
        else
        {
            foreach (CheckPointManager cpManager in allCPMs)
            {
                if (cpManager.levelCreatedAt == level)
                {
                    instance = cpManager;
                    return;
                }
            }
            foreach (CheckPointManager cpManager in allCPMs)
            {
                if (cpManager.levelCreatedAt == -1)
                {
                    instance = cpManager;
                    return;
                }
            }
        }
    }

    public void SetCheckpoint(GameObject checkpointTrigger)
    {
        int index;
        index = checkpoints.FindIndex(CheckP => CheckP.Equals(checkpointTrigger)); //returns -1 if failed

        if (index == -1)
        {
            Debug.Log("Baþaramadýk abi : Object cannot be found in checkpoints list.");
            return;
        }
        else
        {
            lastCheckpointPosition = checkpointTrigger.transform.position;
            SpawnPoint = lastCheckpointPosition;
            GameObject[] DestroyArray = new GameObject[index];
            for(int i = 0; i < index; i++)
            {
                DestroyArray[i] = checkpoints[i];
            }
            checkpoints.RemoveRange(0, index + 1);
            foreach (GameObject DGO in DestroyArray)
                Destroy(DGO);
        }
    }
}
