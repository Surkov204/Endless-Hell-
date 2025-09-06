using UnityEngine;
using UnityEngine.UI;
public class BulletBar : MonoBehaviour
{
    [SerializeField] private GunFireAction gunFireAction;
    [SerializeField] private Image totalBulletBar;
    [SerializeField] private Image currentBulletBar;

    private void Start()
    {
        totalBulletBar.fillAmount = gunFireAction.CurrentAmmo / gunFireAction.MaxAmmo;
    }
    private void Update()
    {
        currentBulletBar.fillAmount = gunFireAction.CurrentAmmo / gunFireAction.MaxAmmo;
    }
}
