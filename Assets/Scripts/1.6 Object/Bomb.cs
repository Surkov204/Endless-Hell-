using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] private float damage = 20f;        
    [SerializeField] private float radius = 3f;         
    [SerializeField] private LayerMask targetMask;      

    [Header("Components")]
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject BombObject;

    [SerializeField] private Health health;
    private bool exploded = false;

    private void Awake()
    {
        if (anim == null) anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!exploded && health.GetHealh() <= 0)
        {
            Explode();
        }
    }

    public void TriggerExplosionEvent()
    {
        if (!exploded)
        {
            Explode();
        }
    }

    private void Explode()
    {
        if (exploded) return;
        exploded = true;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);
        foreach (Collider2D hit in hits)
        {
            Health target = hit.GetComponent<Health>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
        AudioManager.Instance.PlaySoundFX(SoundFXLibrary.SoundFXName.Explosion);
        ScoreManager.Instance.ScoreApply(ScoreType.Easy);
        anim.SetTrigger("isExplode");
        Destroy(BombObject, 1.0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
