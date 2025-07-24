using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CollectibleObject : MonoBehaviour
{
    [Header("Object components")]
    protected Transform sprite;
    protected SpriteRenderer spriteRenderer;
    protected ParticleSystem destroyParticle;
    protected Light2D spotLight;


    [Header("Object atributes")]
    [SerializeField] protected float minSpeed;
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected float minScale;
    [SerializeField] protected float maxScale;
    protected float scale;
    protected float speed;

    [Header("Floating")]
    [SerializeField] protected float minFloatAmplitude;
    [SerializeField] protected float minFloatFrequency;
    [SerializeField] protected float maxFloatAmplitude;
    [SerializeField] protected float maxFloatFrequency;
    protected float baseY;
    protected float floatAmplitude;
    protected float floatFrequency;

    [Header("Rotation")]
    [SerializeField] protected float maxRotationSpeed;
    [SerializeField] protected float minRotationSpeed;
    protected float rotationSpeed;


    protected virtual void Awake()
    {
        if(sprite == null) sprite = transform.Find("Sprite");
        if (destroyParticle == null) destroyParticle = GetComponent<ParticleSystem>();
        if (sprite != null) spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        RandomizeVariables();
    }
    protected virtual void Start()
    {
        baseY = transform.position.y;
    }

    protected virtual void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);
        transform.localScale = new Vector3(scale, scale, 1f);
        Floating();
        Rotation();
        OutScreenDelete();
    }

    protected virtual void Floating()
    {
        Vector3 pos = transform.position;
        pos.y = baseY + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = pos;
    }

    protected virtual void RandomizeVariables()
    {
        floatAmplitude = Random.Range(minFloatAmplitude, maxFloatAmplitude);
        floatFrequency = Random.Range(minFloatFrequency, maxFloatFrequency);
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        speed = Random.Range(minSpeed, maxSpeed);
        scale = Random.Range(minScale, maxScale);
    }

    protected virtual void Rotation()
    {
        sprite.transform.Rotate(0f, 0f, rotationSpeed * Time.fixedDeltaTime);
    }

    protected virtual void OutScreenDelete()
    {
        if (transform.position.x < -11f)
        {
            Destroy(gameObject);
        }

    }

    protected virtual void DisabeleToDelete()
    {

    }

    protected virtual IEnumerator DestroyAfterTime(float waitTime)
    {
        if (waitTime > 0)
            yield return new WaitForSeconds(waitTime);

        if (spriteRenderer != null)
            spriteRenderer.enabled = false;
        if (spotLight != null)
            spotLight.enabled = false;

        DisabeleToDelete();

        if (destroyParticle != null)
            destroyParticle.Play();

        GetComponent<Collider2D>().enabled = false;

        float delay = destroyParticle.main.duration + 1f;
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }


}
