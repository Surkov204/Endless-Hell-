using UnityEngine;
using System.Collections;
using TMPro;
public class QuestTitleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private TypeWordEffect typeWordEffect;
    [SerializeField] private Vector2 targetAnchoredPosition;
    [SerializeField] private Vector2 startPos;
    [SerializeField] private float moveDuration = 2f;
    [SerializeField] private float endSize = 50f;
    [SerializeField] private float startSize = 150f;
    private float timer;

    private Coroutine runningCoroutine;
    private bool isPlaying;

    void Awake()
    {
        questText.gameObject.SetActive(false);
    }

    public void ShowQuestTitle(string text)
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }

        questText.gameObject.SetActive(true);
        timer = 0f;
        questText.text = text;
        questText.fontSize = startSize; 
        questText.rectTransform.anchoredPosition = startPos;
        typeWordEffect.Play(questText.text);
        isPlaying = true;
        runningCoroutine = StartCoroutine(PlayTitleEffect());

    }

    public void HideQuestTitle()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }

        isPlaying = false;
        questText.gameObject.SetActive(false);
    }

    private IEnumerator PlayTitleEffect(){
        yield return new WaitForSeconds(1f);

        Vector2 fromPos = startPos;
        float fromSize = startSize;

        while (isPlaying)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / moveDuration);

            questText.rectTransform.anchoredPosition = Vector2.Lerp(fromPos, targetAnchoredPosition, t);
            questText.fontSize = Mathf.Lerp(fromSize, endSize, t);
            yield return null; 
        }

        questText.rectTransform.anchoredPosition = targetAnchoredPosition;
        questText.fontSize = endSize;

        runningCoroutine = null;
    }
}
