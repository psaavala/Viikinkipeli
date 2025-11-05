using Cinemachine;
using UnityEngine;

public class PlayerSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    public Transform playerSpawnPoint;

    private static PlayerSceneManager instance;
    private static GameObject playerInstance;

    void Awake()
    {
        // make this manager a singleton
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // destroy duplicates of this manager
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // survive across scenes
    }

    private void Start()
    {
        if (playerInstance != null)
        {
            // player already exists → just move him to new spawn point
            playerInstance.transform.position = playerSpawnPoint.position;
            playerInstance.transform.rotation = playerSpawnPoint.rotation;

            Camera.main.GetComponent<CinemachineVirtualCamera>().Follow = playerInstance.transform;
            return;
        }

        // first time → create the player
        playerInstance = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);

        Camera.main.GetComponent<CinemachineVirtualCamera>().Follow = playerInstance.transform;
    }
}
