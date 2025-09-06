using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PhaseType
{
    Normal,
    Ice,
    Cannon
}

public class SpawnManager : MonoBehaviour
{
    [Header("Spawners Normal")]
    [SerializeField] private MonoBehaviour[] normalSpawners;

    [Header("Spawners Lighting")]
    [SerializeField] private MonoBehaviour[] lightSpawners;

    [Header("Spawners Animal")]
    [SerializeField] private MonoBehaviour[] aniamlSpawners;

    [Header("Ice Spawner")]
    [SerializeField] private MonoBehaviour iceSpawner;
    [Header("Bomb Spawner")]
    [SerializeField] private MonoBehaviour bombSpawner;

    [Header("Canon Settings")]
    [SerializeField] private GameObject canonObject;       
    [SerializeField] private MonoBehaviour canonScript;
    [SerializeField] private Health canonHealth;
    [SerializeField] private float canonUpY = -2f;         
    [SerializeField] private float canonDownY = -4f;       
    [SerializeField] private float canonMoveSpeed = 2f;    

    [Header("Timing")]
    [SerializeField] private float normalPhaseDuration = 15f;
    [SerializeField] private float icePhaseDuration = 30f;
    [SerializeField] private float canonEnableDuration = 30f;

    [Header("Title")]
    [SerializeField] private QuestTitleUI questTitleUI;

    private void Start()
    {
        StartCoroutine(PhaseLoop());
    }

    private IEnumerator PhaseLoop()
    {
        while (true)
        {
            yield return RunPhase(PhaseType.Normal, normalPhaseDuration);
            yield return RunPhase(PhaseType.Ice, icePhaseDuration);
            yield return RunPhase(PhaseType.Cannon, canonEnableDuration, true);
        }
    }

    private IEnumerator RunPhase(PhaseType phase, float duration, bool isCanon = false)
    {
        Debug.Log($"{phase} Phase ON");
        yield return new WaitForSeconds(0.5f);
        questTitleUI.ShowQuestTitle($"{phase} Phase");
        switch (phase)
        {
            case PhaseType.Normal:
                SetSpawnersActive(normalSpawners, true);
                SetSpawnersActive(aniamlSpawners, true);
                SetSpawnersActive(lightSpawners, true);
                if (iceSpawner) iceSpawner.enabled = false;
                if (bombSpawner) bombSpawner.enabled = false;
                break;

            case PhaseType.Ice:
                SetSpawnersActive(normalSpawners, false);
                SetSpawnersActive(lightSpawners, false);
                if (iceSpawner) iceSpawner.enabled = true;
                break;

            case PhaseType.Cannon:
                if (iceSpawner) iceSpawner.enabled = false;
                SetSpawnersActive(aniamlSpawners, false);
                SetSpawnersActive(lightSpawners, true);
                if (bombSpawner) bombSpawner.enabled = true;
                break;
        }

        if (isCanon)
        {
            yield return StartCoroutine(ActivateCanon());
        }
        else
        {
            yield return new WaitForSeconds(duration);
        }

        questTitleUI.HideQuestTitle();
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

        if (canonHealth.CurrentHealth <= 0f) ScoreManager.Instance.ScoreApply(ScoreType.Hard);

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

    private void SetSpawnersActive(MonoBehaviour[] spawners, bool active)
    {
        foreach (var spawner in spawners)
            if (spawner != null) spawner.enabled = active;
    }
}
