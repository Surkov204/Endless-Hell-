using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class Letterboxer : MonoBehaviour
{
    [Range(0.1f, 4f)]
    public float targetAspect = 2.39f; 

    private Camera cam;

    void OnDisable()
    {
        if (cam != null)
        {
            cam.rect = new Rect(0f, 0f, 1f, 1f);
        }
    }
    void Awake()
    {
        cam = GetComponent<Camera>();
        UpdateLetterbox();
    }

    void Update()
    {
        UpdateLetterbox();
    }

    void UpdateLetterbox()
    {
        float screenAspect = (float)Screen.width / Screen.height;
        float scale = screenAspect / targetAspect;

        if (scale < 1f)
        {
            Rect rect = cam.rect;
            rect.width = 1f;
            rect.height = scale;
            rect.x = 0;
            rect.y = (1f - scale) / 2f;
            cam.rect = rect;
        }
        else
        {
            float scaleWidth = 1f / scale;
            Rect rect = cam.rect;
            rect.width = scaleWidth;
            rect.height = 1f;
            rect.x = (1f - scaleWidth) / 2f;
            rect.y = 0;
            cam.rect = rect;
        }
    }
}
