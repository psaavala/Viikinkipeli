using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerIconFollow : MonoBehaviour
{
    private Transform player;
    private RectTransform rectTransform;
    [SerializeField] private float rotationSpeed = 5f;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        StartCoroutine(WaitForPlayer());
    }

    void LateUpdate()
    {
        if (player == null) return;

        rectTransform.anchoredPosition = Vector2.zero;

        float playerYRotation = player.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(0, 0, -playerYRotation);
        rectTransform.rotation = Quaternion.Lerp(rectTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(WaitForPlayer());
    }

    private IEnumerator WaitForPlayer()
    {
        // Wait until a player object exists
        while (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;

            yield return null; // wait a frame
        }
    }
}
