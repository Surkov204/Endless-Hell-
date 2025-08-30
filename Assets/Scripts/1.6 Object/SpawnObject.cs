using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [Header("Obstacle Setup")]
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform spawnPoint;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnStartingTime = 1f;
    [SerializeField] private float zPivot = -0.1f;
    [SerializeField] private float minY = -2f;
    [SerializeField] private float maxY = 2f;               

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), spawnStartingTime, spawnInterval);
    }

    void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);

        Vector3 spawnPos = spawnPoint.position;
        // spawnPos.y = Random.Range(minY, maxY);
        spawnPos.z = zPivot;

        Instantiate(obstaclePrefabs[randomIndex], spawnPos, Quaternion.identity);
    }
}
