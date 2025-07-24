using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AsteroidController : CollectibleObject
{
    [Header("Asteroide components")]
    [SerializeField] private Transform asteroidSprite;
    [SerializeField] private ParticleSystem asteroidDestroyParticle;
    [SerializeField] private Light2D asteroidSpotLight;

    protected override void Awake()
    {
        base.Awake();

        sprite = asteroidSprite;
        destroyParticle = asteroidDestroyParticle;
        spotLight = asteroidSpotLight;

        minSpeed = 1.5f;
        maxSpeed = 3.5f;
        minScale = .7f;
        maxScale = 1.2f;

        minFloatAmplitude = .5f;
        minFloatFrequency = 1.2f;
        maxFloatAmplitude = 1f;
        maxFloatFrequency = 2f;

        maxRotationSpeed = 35f;
        minRotationSpeed = 65f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.explosion, 0.4f);
            StartCoroutine(DestroyAfterTime(0));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.HandleExternalEvent(1);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.explosion, 0.6f);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.damage, .5f);
            StartCoroutine(DestroyAfterTime(0f));
        }
            
    }

}
