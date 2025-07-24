using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpaceCristalController : CollectibleObject
{
    [Header("Components")]
    [SerializeField] private Transform cristalSprite;
    [SerializeField] private Light2D cristalSpotLight;
    [SerializeField] private ParticleSystem colectParticle;
    [SerializeField] private Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();

        if (rb == null) GetComponent<Rigidbody2D>();

        sprite = cristalSprite;
        if (sprite != null) spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        destroyParticle = colectParticle;
        spotLight = cristalSpotLight;

        minSpeed = 1f;
        maxSpeed = 2.7f;
        minScale = .45f;
        maxScale = .45f;

        minFloatAmplitude = .3f;
        minFloatFrequency = 1.2f;
        maxFloatAmplitude = 8f;
        maxFloatFrequency = 2f;

        maxRotationSpeed = 35f;
        minRotationSpeed = 65f;

    }

    protected override void DisabeleToDelete()
    {
        base.DisabeleToDelete();

        if (rb != null)
            rb.linearVelocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerConstants.Instance.AddPoint();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.collect, 0.6f);
            StartCoroutine(DestroyAfterTime(0));
        }
    }

}
