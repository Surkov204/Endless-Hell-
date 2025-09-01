using UnityEngine;

public class BulletCanon : MonoBehaviour
{
    [SerializeField] private float damage = 1f;                
    [SerializeField] private GameObject hitEffectPrefab;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            AnimationManager.Instance.Play(AnimationClipName.ShakingByBullet, AnimatorTarget.CameraShaking);
            Debug.Log("play");
            SpawnEffect();
            Destroy(gameObject);
        }
    }

    private void SpawnEffect()
    {
        if (hitEffectPrefab != null)
        {
            GameObject effect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 1.5f);
        }
    }

}
