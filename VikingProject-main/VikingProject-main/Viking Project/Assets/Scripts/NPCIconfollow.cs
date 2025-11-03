using UnityEngine;

public class NPCIconFollow : MonoBehaviour
{
    public Transform npc;       // Assign your NPC GameObject
    public Transform player;    // Assign your Player GameObject
    public RectTransform mapmaskRect; // Assign the RectTransform of mapmask
    public float mapScale = 0.1f; // How much world units scale to minimap

    private RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        if (npc == null || player == null) return;

        // Calculate offset from player
        Vector3 offset = npc.position - player.position;

        // Map world offset to minimap coordinates
        Vector2 minimapPos = new Vector2(offset.x, offset.z) * mapScale;

        // Clamp icon to map edges if needed
        rect.anchoredPosition = minimapPos;
    }
}
