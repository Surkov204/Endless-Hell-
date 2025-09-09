using UnityEngine;

public class AutoHideImage : MonoBehaviour
{
    [SerializeField] private GameObject targetImage;
    [SerializeField] private float showDuration = 3f; 

    private void OnEnable()
    {
        StartCoroutine(HideAfterSeconds());
    }

    private System.Collections.IEnumerator HideAfterSeconds()
    {
        if (targetImage != null)
        {
            targetImage.SetActive(true); 
            yield return new WaitForSeconds(showDuration);
            targetImage.SetActive(false); 
        }
    }
}
