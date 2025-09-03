using UnityEngine;
using static SoundFXLibrary;

public class GunFireAction : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float speedBullet;
    [SerializeField] private Transform firePoint;

    public void PlayerShooting() {
        if (Time.timeScale == 0) return;
        if (Input.GetMouseButtonDown(0)) {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = transform.localScale.x * Vector2.right * speedBullet;
            AudioManager.Instance.PlaySoundFX(SoundFXName.GunShot);
            Destroy(bullet, 2f);
        }
    }

}
