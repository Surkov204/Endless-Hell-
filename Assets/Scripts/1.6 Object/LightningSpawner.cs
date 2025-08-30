using UnityEngine;
using System.Collections;
public class LightningSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject warningPrefab;
    [SerializeField] private GameObject lightningPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private Transform lightningPoint;  
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float warningDuration = 1f;
    [SerializeField] private float startSpawn = 1f;

    private void Start()
    {
        Vector3 spawnPos = lightningPoint.position;
        InvokeRepeating(nameof(SpawnLightning), startSpawn, spawnInterval);
    }

    private void SpawnLightning()
    {
        Vector3 spawnPos = lightningPoint.position;
        spawnPos.z = 0.96f;
        spawnPos.y = -2f;
        GameObject warning = Instantiate(
               warningPrefab,
               spawnPos,
               Quaternion.identity,
               lightningPoint   
           );

        StartCoroutine(SpawnLightningAfterDelay(warning));
    }

    private IEnumerator SpawnLightningAfterDelay(GameObject warning)
    {
        yield return new WaitForSeconds(warningDuration);

        Destroy(warning);
        Vector3 spawnPos = lightningPoint.position;
        spawnPos.z = 0.96f;
        GameObject lightning = Instantiate(
            lightningPrefab,
            spawnPos,
            Quaternion.identity,
            lightningPoint  
        );

        Destroy(lightning, 0.5f); 
    }
}
