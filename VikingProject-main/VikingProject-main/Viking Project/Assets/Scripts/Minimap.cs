using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MinimapFollow : MonoBehaviour
{
    public Transform player;
    public float height = 30f;
    public bool rotateWithPlayer = true;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        StartCoroutine(FindPlayerRoutine());
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FindPlayerRoutine());
    }

    IEnumerator FindPlayerRoutine()
    {
        // try until found (this fixes timing issues)
        while (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
            {
                player = p.transform;
                yield break;
            }

            yield return null; // wait 1 frame
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 newPosition = player.position;
        newPosition.y += height;
        transform.position = newPosition;

        if (rotateWithPlayer)
            transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        else
            transform.rotation = Quaternion.Euler(90f, 45f, 0f);
    }
}
