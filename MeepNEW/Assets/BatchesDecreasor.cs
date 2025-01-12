using UnityEngine;

public class BatchesOptimizer : MonoBehaviour
{
    // Settings for optimization
    public bool optimizeStaticBatching = true;
    public bool optimizeMaterialBatching = true;
    public bool optimizeLOD = true;
    public bool optimizeTextureAtlasing = true;

    // Thresholds for optimization
    public int targetBatchCount = 35; // Target number of batches

    void Start()
    {
        OptimizeScene();
    }

    void OptimizeScene()
    {
        if (optimizeStaticBatching)
        {
            OptimizeStaticBatching();
        }

        if (optimizeMaterialBatching)
        {
            OptimizeMaterialBatching();
        }

        if (optimizeLOD)
        {
            OptimizeLOD();
        }

        if (optimizeTextureAtlasing)
        {
            OptimizeTextureAtlasing();
        }

        // Validate if optimization achieved the target batch count
        ValidateBatchCount();
    }

    void OptimizeStaticBatching()
    {
        // Combine all static GameObjects into a single batch
        StaticBatchingUtility.Combine(null, gameObject);
    }

    void OptimizeMaterialBatching()
    {
        Renderer[] renderers = FindObjectsOfType<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.sharedMaterials;
            renderer.materials = materials; // Ensure materials are using shared materials
        }
    }

    void OptimizeLOD()
    {
        LODGroup[] lodGroups = FindObjectsOfType<LODGroup>();
        foreach (LODGroup lodGroup in lodGroups)
        {
            // Example: Set LOD settings based on performance metrics
            LOD[] lods = lodGroup.GetLODs();
            // Adjust LOD settings as needed
            lodGroup.SetLODs(lods);
        }
    }

    void OptimizeTextureAtlasing()
    {
        // Implement texture atlasing optimization here
        // Example: Combine textures into atlases
    }

    void ValidateBatchCount()
    {
        int currentBatchCount = GetBatchCount();
        if (currentBatchCount > targetBatchCount)
        {
            Debug.LogWarning($"Current batch count ({currentBatchCount}) is higher than target ({targetBatchCount}). Further optimizations may be required.");
        }
        else
        {
            Debug.Log($"Optimization successful. Current batch count: {currentBatchCount}");
        }
    }

    int GetBatchCount()
    {
        // Calculate and return the current batch count
        int batchCount = 0;

        // Example: Calculate batch count based on Renderer information
        Renderer[] renderers = FindObjectsOfType<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            batchCount += renderer.isVisible ? 1 : 0; // Increment batch count for each visible renderer
        }

        return batchCount;
    }
}
