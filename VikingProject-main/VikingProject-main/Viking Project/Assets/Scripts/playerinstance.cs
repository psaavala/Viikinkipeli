using UnityEngine;

public class PersistentPlayer : MonoBehaviour
{
    private static PersistentPlayer instance;

    void Awake()
    {
        // If no instance exists, keep this one and make it persist across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // If an instance already exists, destroy this duplicate
        else
        {
            Destroy(gameObject);
        }
    }
}
