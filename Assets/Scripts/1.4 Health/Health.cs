using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using JS;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float StartingHealth => startingHealth;
    private float currentHealth;
    public float CurrentHealth => currentHealth;
    private bool dead;

    [Header("Iframe")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private float numberOfFlashes;
    private SpriteRenderer spriteRender;

    [Header("component")]
    [SerializeField] private Behaviour[] components;
    [Header("Decay")]
    [SerializeField] private GameObject decayPrefab;

    public bool isPlayer;
    public bool isCanon;

    private void Awake()
    {
        currentHealth = startingHealth;
        spriteRender = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            if (isPlayer)
            {
                AudioManager.Instance.PlaySoundFX(SoundFXLibrary.SoundFXName.PlayerHurt);
            }
            StartCoroutine(Invunerability());
        }
        else
        if (!dead)
        {
            if (isCanon) return;

            foreach (Behaviour component in components)
            {
                component.enabled = false;
                Deactivate();
            }


            if (decayPrefab != null)
            {
                GameObject decayInstance = Instantiate(decayPrefab, transform.position, Quaternion.identity);
                decayInstance.SetActive(true);
            }

            if (isPlayer)
            {
                Debug.Log("Game Over on");
                AudioManager.Instance.PlaySoundFX(SoundFXLibrary.SoundFXName.PlayerDeath);
                UIManager.Instance.ShowUI(UIName.GameOverScreen);
            }
            dead = true;
        }

    }
    public void SetHealth(float health)
    {
        currentHealth = Mathf.Clamp(health, 0, startingHealth);
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRender.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRender.color = new Color(0, 0.860742f, 0.8679245f, 1f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(9, 10, false);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);

    }

    public void SetHealh(float value){
        currentHealth = value;
    }

    public float GetHealh()
    {
        return currentHealth;
    }
}
