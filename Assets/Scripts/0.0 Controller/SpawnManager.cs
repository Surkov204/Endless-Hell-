using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawners Normal")]
    [SerializeField] private MonoBehaviour[] normalSpawners;

    [Header("Spawners Animal")]
    [SerializeField] private MonoBehaviour[] aniamlSpawners;

    [Header("Ice Spawner")]
    [SerializeField] private MonoBehaviour iceSpawner;

    [Header("Canon Settings")]
    [SerializeField] private GameObject canonObject;       
    [SerializeField] private MonoBehaviour canonScript;
    [SerializeField] private Health canonHealth;
    [SerializeField] private float canonUpY = -2f;         
    [SerializeField] private float canonDownY = -4f;       
    [SerializeField] private float canonEnableDuration = 30f; 
    [SerializeField] private float canonMoveSpeed = 2f;    


    [Header("Timing")]
    [SerializeField] private float normalPhaseDuration = 15f;
    [SerializeField] private float icePhaseDuration = 30f;

    private void Start()
    {
        StartCoroutine(PhaseLoop());
    }

    private IEnumerator PhaseLoop()
    {
        while (true)
        {

            Debug.Log("Normal Phase ON");

            foreach (var spawner in normalSpawners)
                if (spawner != null) spawner.enabled = true;
            foreach (var spawner in aniamlSpawners)
                if (spawner != null) spawner.enabled = true;

            if (iceSpawner != null) iceSpawner.enabled = false;

            yield return new WaitForSeconds(normalPhaseDuration);

            Debug.Log("Ice Phase ON");

            foreach (var spawner in normalSpawners)
                if (spawner != null) spawner.enabled = false;

            if (iceSpawner != null) iceSpawner.enabled = true;

            yield return new WaitForSeconds(icePhaseDuration);

            if (iceSpawner != null) iceSpawner.enabled = false;
            foreach (var spawner in aniamlSpawners)
                if (spawner != null) spawner.enabled = false;

            Debug.Log("Canon Phase ON");
            yield return StartCoroutine(ActivateCanon());
        }
    }

    private IEnumerator ActivateCanon()
    {
        if (canonObject == null || canonScript == null) yield break;

        canonObject.SetActive(true);
        canonScript.enabled = true;
        Vector3 startPos = canonObject.transform.localPosition;
        Vector3 upPos = new Vector3(startPos.x, canonUpY, startPos.z);

        while (Vector3.Distance(canonObject.transform.localPosition, upPos) > 0.01f)
        {
            canonObject.transform.localPosition = Vector3.MoveTowards(
                canonObject.transform.localPosition,
                upPos,
                canonMoveSpeed * Time.deltaTime
            );
            yield return null;
        }
        canonObject.transform.localPosition = upPos;

        float elapsed = 0f;
        while (elapsed < canonEnableDuration && canonHealth.CurrentHealth > 0f)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Canon Phase OFF");
        canonScript.enabled = false;
        Vector3 downPos = new Vector3(startPos.x, canonDownY, startPos.z);

        while (Vector3.Distance(canonObject.transform.localPosition, downPos) > 0.01f)
        {
            canonObject.transform.localPosition = Vector3.MoveTowards(
                canonObject.transform.localPosition,
                downPos,
                canonMoveSpeed * Time.deltaTime
            );
            yield return null;
        }
        canonObject.transform.localPosition = downPos;
        canonHealth.SetHealh(2000f);
        canonObject.SetActive(false);
    }
}
