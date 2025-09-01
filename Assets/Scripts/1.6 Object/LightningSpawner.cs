using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

    private List<GameObject> activeWarnings = new List<GameObject>();

    private void OnEnable()
    {
        InvokeRepeating(nameof(SpawnLightning), startSpawn, spawnInterval);
    }

    private void OnDisable()
    {
        CancelInvoke();
        StopAllCoroutines();

        foreach (var w in activeWarnings)
        {
            if (w != null) Destroy(w);
        }
        activeWarnings.Clear();
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
        activeWarnings.Add(warning);
        StartCoroutine(SpawnLightningAfterDelay(warning));
    }

    private IEnumerator SpawnLightningAfterDelay(GameObject warning)
    {
        yield return new WaitForSeconds(warningDuration);

        if(warning != null) Destroy(warning);
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
