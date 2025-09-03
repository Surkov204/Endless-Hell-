using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Target")]
    [SerializeField] private Image buttonImage;
    [SerializeField] private TMP_Text buttonText;

    [Header("Colors")]
    [SerializeField] private Color normalTextColor = Color.white;
    [SerializeField] private Color hoverTextColor = Color.black;

    [Range(0f, 1f)][SerializeField] private float normalAlpha = 0.7f;  
    private const float hoverAlpha = 1f;             

    private void Awake()
    {
        if (buttonImage == null) buttonImage = GetComponent<Image>();
        if (buttonText == null) buttonText = GetComponentInChildren<TMP_Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            var colorChanger = buttonImage.color;
            colorChanger.a = hoverAlpha;
            buttonImage.color = colorChanger;
        }
        if (buttonText != null)
            buttonText.color = hoverTextColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            var colorChanger = buttonImage.color;
            colorChanger.a = normalAlpha;
            buttonImage.color = colorChanger;
        }
        if (buttonText != null)
            buttonText.color = normalTextColor;
    }
}
