using UnityEngine;

public class PlayerIconFollow : MonoBehaviour
{
    public Transform player; // your current field

    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player"); // finds player in scene
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Keep icon in center
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            // Rotate icon if you added rotation
            float playerYRotation = player.eulerAngles.y;
            GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -playerYRotation);
        }
    }
}
