using TMPro;
using UnityEngine;
using static SoundFXLibrary;

public class GunFireAction : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float speedBullet;
    [SerializeField] private Transform firePoint;

    [Header("Ammo Settings")]
    [SerializeField] private float maxAmmo = 10;     
    [SerializeField] private float reloadTime = 2f;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject reloadingText;
    private float currentAmmo;
    public float CurrentAmmo => currentAmmo;
    public float MaxAmmo => maxAmmo;
    private bool isReloading = false;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI ammoText;

    private void Start()
    {
        currentAmmo = maxAmmo; 
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;

        UpdateAmmoUI();

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButtonDown(0) && !isReloading)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (currentAmmo <= 0)
        {
            reloadingText.SetActive(true);
            return;
        }

        currentAmmo--;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.localScale.x * Vector2.right * speedBullet;

        AudioManager.Instance.PlaySoundFX(SoundFXName.GunShot);
        Destroy(bullet, 2f);
    }

    private System.Collections.IEnumerator Reload()
    {
        isReloading = true;

        if (anim != null)
        {
            anim.SetBool("Reload", true);
        }

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        reloadingText.SetActive(false);
        if (anim != null)
        {
            anim.SetBool("Reload", false);
        }
    }

    private void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"{currentAmmo}";
        }
    }
}
