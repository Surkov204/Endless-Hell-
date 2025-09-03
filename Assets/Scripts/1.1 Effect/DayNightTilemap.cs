using UnityEngine;
using UnityEngine.Tilemaps;

public class DayNightTilemap : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Tilemap[] tilemaps; 

    [Header("Colors")]
    [SerializeField] private Color dayColor = Color.white; 
    [SerializeField] private Color nightColor = new Color(0.57f, 0.57f, 0.57f); 

    [Header("Settings")]
    [SerializeField] private float dayDuration = 30f;
    [SerializeField] private float transitionDuration = 30f;
    [SerializeField] private float nightDuration = 30f;

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

        if (t < dayDuration)
        {
            SetTilemapColor(dayColor);
        }
        else if (t < dayDuration + transitionDuration)
        {
            float progress = (t - dayDuration) / transitionDuration;
            Color c = Color.Lerp(dayColor, nightColor, progress);
            SetTilemapColor(c);
        }
        else if (t < dayDuration + transitionDuration + nightDuration)
        {
            SetTilemapColor(nightColor);
        }
        else
        {
            float progress = (t - (dayDuration + transitionDuration + nightDuration)) / transitionDuration;
            Color c = Color.Lerp(nightColor, dayColor, progress);
            SetTilemapColor(c);
        }
    }

    private void SetTilemapColor(Color c)
    {
        foreach (var tilemap in tilemaps)
        {
            if (tilemap != null)
                tilemap.color = c;
        }
    }
}
