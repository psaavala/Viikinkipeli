using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIconsManager : MonoBehaviour
{
    public Transform enemiesParent;    // Your "Enemies" parent object
    public Transform player;           // Your player
    public RectTransform mapmaskRect;  // Your mapmask RectTransform
    public GameObject enemyIconPrefab; // The prefab you created
    public float mapScale = 0.1f;      // Adjust to fit minimap

    private Dictionary<Transform, RectTransform> enemyIcons = new Dictionary<Transform, RectTransform>();

    void Start()
    {
        // Spawn icons for each enemy
        foreach (Transform enemy in enemiesParent)
        {
            GameObject iconGO = Instantiate(enemyIconPrefab, mapmaskRect);
            RectTransform iconRect = iconGO.GetComponent<RectTransform>();
            enemyIcons.Add(enemy, iconRect);
        }
    }

    void LateUpdate()
    {
        foreach (var pair in enemyIcons)
        {
            Transform enemy = pair.Key;
            RectTransform icon = pair.Value;

            Vector3 offset = enemy.position - player.position;
            Vector2 minimapPos = new Vector2(offset.x, offset.z) * mapScale;

            // Keep icon inside minimap circle if desired
            float radius = mapmaskRect.rect.width / 2f;
            if (minimapPos.magnitude > radius)
            {
                minimapPos = minimapPos.normalized * radius;
            }

            icon.anchoredPosition = minimapPos;
        }
    }
}
