using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer nightOverlay;
    [SerializeField] private Transform sun;
    [SerializeField] private Transform moon;

    [Header("Settings")]
    [SerializeField] private float dayDuration = 30f;       
    [SerializeField] private float transitionDuration = 30f;
    [SerializeField] private float nightDuration = 30f;     
    [SerializeField] private float yMin = -4f;
    [SerializeField] private float yMax = 4f;

    private float timer;
    private float cycleDuration;

    private void Start()
    {
        cycleDuration = dayDuration + transitionDuration + nightDuration + transitionDuration;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        float t = timer % cycleDuration;
        Color c = nightOverlay.color;
        if (t < dayDuration)
        {
            c.a = 0f;
            nightOverlay.color = c;

            sun.localPosition = new Vector3(sun.localPosition.x, yMax, sun.localPosition.z);
            moon.localPosition = new Vector3(moon.localPosition.x, yMin, moon.localPosition.z);
        }
        else if (t < dayDuration + transitionDuration)
        {
            float progress = (t - dayDuration) / transitionDuration; 
            c.a = Mathf.Lerp(0f, 1f, progress);
            nightOverlay.color = c;

            if (progress <= 0.5f)
            {
                float ySun = Mathf.Lerp(yMax, yMin, progress * 2f);
                sun.localPosition = new Vector3(sun.localPosition.x, ySun, sun.localPosition.z);
                moon.localPosition = new Vector3(moon.localPosition.x, yMin, moon.localPosition.z);
            }
            else
            {
                float yMoon = Mathf.Lerp(yMin, yMax, (progress - 0.5f) * 2f);
                moon.localPosition = new Vector3(moon.localPosition.x, yMoon, moon.localPosition.z);
                sun.localPosition = new Vector3(sun.localPosition.x, yMin, sun.localPosition.z);
            }
        }
        else if (t < dayDuration + transitionDuration + nightDuration)
        {
            c.a = 1f;
            nightOverlay.color = c;

            moon.localPosition = new Vector3(moon.localPosition.x, yMax, moon.localPosition.z);
            sun.localPosition = new Vector3(sun.localPosition.x, yMin, sun.localPosition.z);
        }
        else
        {
            float progress = (t - (dayDuration + transitionDuration + nightDuration)) / transitionDuration; 
            c.a = Mathf.Lerp(1f, 0f, progress);
            nightOverlay.color = c;

            if (progress <= 0.5f)
            {
                float yMoon = Mathf.Lerp(yMax, yMin, progress * 2f);
                moon.localPosition = new Vector3(moon.localPosition.x, yMoon, moon.localPosition.z);
                sun.localPosition = new Vector3(sun.localPosition.x, yMin, sun.localPosition.z);
            }
            else
            {
                float ySun = Mathf.Lerp(yMin, yMax, (progress - 0.5f) * 2f);
                sun.localPosition = new Vector3(sun.localPosition.x, ySun, sun.localPosition.z);
                moon.localPosition = new Vector3(moon.localPosition.x, yMin, moon.localPosition.z);
            }
        }
    }
}
