using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IceSpawn : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject warningPrefab;
    [SerializeField] private GameObject icePrefab;

    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints; 

    [Header("Timing")]
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float warningDuration = 1f;
    [SerializeField] private float startDelay = 1f;
    [SerializeField] private float fallSpeed = 5f;

    private List<GameObject> activeWarnings = new List<GameObject>();
    private int[][] patterns = new int[][]
    {
        new int[] {1, 1, 1, 1, 1, 0}, 
        new int[] {1, 1, 1, 1, 0, 1}, 
        new int[] {1, 1, 1, 0, 1, 1}, 
        new int[] {1, 1, 0, 1, 1, 1}, 
        new int[] {1, 0, 1, 1, 1, 1}, 
        new int[] {0, 1, 1, 1, 1, 1}, 
    };

    private void OnEnable()
    {
        InvokeRepeating(nameof(SpawnPattern), startDelay, spawnInterval);
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

    private void SpawnPattern()
    {
        int randomIndex = Random.Range(0, patterns.Length);
        int[] pattern = patterns[randomIndex];

        for (int i = 0; i < spawnPoints.Length && i < pattern.Length; i++)
        {
            if (pattern[i] == 1)
            {
                Vector3 pos = spawnPoints[i].position;
                pos.z = 0.96f;
                pos.y = -2.01f;

                GameObject warning = Instantiate(warningPrefab, pos, Quaternion.identity,spawnPoints[i]);
                activeWarnings.Add(warning);
                StartCoroutine(SpawnIceAfterDelay(warning, i));
            }
        }
    }

    private IEnumerator SpawnIceAfterDelay(GameObject warning, int i)
    {
        yield return new WaitForSeconds(warningDuration);

        if (warning != null) Destroy(warning);
        Vector3 posICE = spawnPoints[i].position;
        posICE.z = 0.96f;
        GameObject ice = Instantiate(icePrefab, posICE, Quaternion.identity, spawnPoints[i]);
        Rigidbody2D rb = ice.GetComponent<Rigidbody2D>();
        ice.GetComponent<IceFall>().enabled = true;
    }
}
