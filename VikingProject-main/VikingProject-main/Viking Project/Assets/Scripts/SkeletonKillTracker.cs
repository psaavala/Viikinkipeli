using UnityEngine;

public class SkeletonKillTracker : MonoBehaviour
{
    public static SkeletonKillTracker Instance;

    [Header("Quest & Objective")]
    public QuestSO quest;                 // Skeleton-tehtävä
    public ObjectiveSO skeletonObjective; // Skeleton objective
    public int requiredSkeletons = 3;

    private int skeletonCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void SkeletonDied(GameObject skeleton)
    {
        skeletonCount++;
        Debug.Log("Skeleton died: " + skeletonCount + "/" + requiredSkeletons);

        if (skeletonCount >= requiredSkeletons)
        {
            skeletonObjective.Completed = true;
            Debug.Log("Skeleton objective completed!");

            quest.TryEndQuest();
        }
    }
}
