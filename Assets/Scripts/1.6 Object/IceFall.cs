using UnityEngine;

public class IceFall : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 5f;
    [SerializeField] private string groundTag = "Ground";
    [SerializeField] private float damaged = 10f;
    private Animator anim;
    private bool hasFallen = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.Self);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<Health>().TakeDamage(damaged);
            AudioManager.Instance.PlaySoundFX(SoundFXLibrary.SoundFXName.IceBreak);
            anim.SetTrigger("IceFalled");
            Destroy(gameObject, 0.5f);
        }

        if (!hasFallen && collision.collider.CompareTag(groundTag))
        {
            hasFallen = true;
            AudioManager.Instance.PlaySoundFX(SoundFXLibrary.SoundFXName.IceBreak);
            anim.SetTrigger("IceFalled");
            Destroy(gameObject, 0.5f);
        }
    }
}
