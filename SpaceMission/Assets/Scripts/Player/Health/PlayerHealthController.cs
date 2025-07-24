using System.Collections;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IHealth
{
    [Header("Components")]
    [SerializeField] private Transform sprite;
    [SerializeField] private ParticleSystem centerParticle;
    [SerializeField] private HealthBar healthBar;

    [Header("Atributes")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int health;
    [SerializeField] private bool isIntangible = false;

    private Coroutine blinkCoroutine;

    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHelth(maxHealth);
    }

    void FixedUpdate()
    {
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            GameController.Instance.GameOver();
        }

    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(DamageRoutine(damage));
    }

    private IEnumerator BlinkEffect()
    {
        SpriteRenderer sr = sprite.GetComponent<SpriteRenderer>();
        var centerMain = centerParticle.main;

        Color originalSpriteColor = sr.color;
        Color originalParticleColor = centerMain.startColor.color;

        float blinkSpeed = 2f;
        float minAlpha = 0.1f;
        float maxAlpha = 1f;

        float time = 0f;

        while (isIntangible)
        {
            float alpha = Mathf.Lerp(minAlpha, maxAlpha, Mathf.PingPong(time * blinkSpeed, 1f));

            Color spriteColor = originalSpriteColor;
            spriteColor.a = alpha;
            sr.color = spriteColor;

            Color particleColor = originalParticleColor;
            particleColor.a = alpha;
            centerMain.startColor = particleColor;

            time += Time.deltaTime;
            yield return null;
        }

        // Restaura cores
        sr.color = originalSpriteColor;
        centerMain.startColor = originalParticleColor;
    }

    public IEnumerator DamageRoutine(int damage)
    {
        SpriteRenderer sr = sprite.GetComponent<SpriteRenderer>();
        Color originalSpriteColor = new Color(1f, 1f, 1f, 1f);
        Color damagedColor = new Color(1f, 0.5f, 0.5f, 1f); 

        health -= damage;
        isIntangible = true;
        GetComponent<Collider2D>().enabled = false;

        float duration = 0.1f;
        float t = 0f;

        while (t < duration)
        {
            sr.color = Color.Lerp(originalSpriteColor, damagedColor, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        sr.color = damagedColor;

        blinkCoroutine = StartCoroutine(BlinkEffect());

        yield return new WaitForSeconds(3f);

        if (blinkCoroutine != null)
            StopCoroutine(blinkCoroutine);

        isIntangible = false;

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }

        GetComponent<Collider2D>().enabled = true;

        t = 0f;
        while (t < duration)
        {
            sr.color = Color.Lerp(damagedColor, originalSpriteColor, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        sr.color = originalSpriteColor;
    }
}

