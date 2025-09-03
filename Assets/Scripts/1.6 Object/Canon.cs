using UnityEngine;

public class Canon : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private float coolDownFire = 2f;
    [SerializeField] private float speedRate = -2f;
    [SerializeField] private float startFireTime = 1f;
    [Header("Components")]
    [SerializeField] private GameObject bulletConon;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator anim;
    [Header("AnimationClip")]
    [SerializeField] private string canonAnimationClip;

    private void OnEnable(){
        InvokeRepeating(nameof(StartAnimationCanon), startFireTime, coolDownFire);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void StartAnimationCanon() {
        anim.SetBool(canonAnimationClip,true);
    }

    public void CanonFire() {
        anim.SetBool(canonAnimationClip, false);
        AudioManager.Instance.PlaySoundFX(SoundFXLibrary.SoundFXName.Canon);
        GameObject bullet = Instantiate(bulletConon, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(speedRate, 0);
    }

}
