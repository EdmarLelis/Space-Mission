using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class BulletController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform sprite;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private ParticleSystem destroyParticle;
    [SerializeField] private Light2D spotLight;

    [Header("Bullet attributes")]
    [SerializeField] private float speed = 15f;
    [SerializeField] private bool isMoving = true;

    void Awake()
    {
        if (destroyParticle == null) destroyParticle = GetComponent<ParticleSystem>();
        if (sprite != null) spriteRenderer = sprite.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (isMoving)
            transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);

        OutScreenDelete();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player"))
        {
            isMoving = false;
            StartCoroutine(DestroyAfterTime(0));
        }
    }
    
    private void OutScreenDelete()
    {
        if (transform.position.x > 11f)
        {
            Destroy(gameObject);
        }

    }

    private IEnumerator DestroyAfterTime(float waitTime)
    {
        if (waitTime > 0)
            yield return new WaitForSeconds(waitTime);

        if (spriteRenderer != null)
            spriteRenderer.enabled = false;
        if (spotLight != null)
            spotLight.enabled = false;

        if (destroyParticle != null)
            destroyParticle.Play();

        GetComponent<Collider2D>().enabled = false;

        float delay = destroyParticle.main.duration + 1f;
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}
