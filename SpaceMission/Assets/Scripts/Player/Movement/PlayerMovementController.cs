using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform sprite;

    [Header("Floating")]
    [SerializeField] private float floatAmplitude = 0.01f;
    [SerializeField] private float floatFrequency = 2f;
    [SerializeField] private float baseY;

    [Header("Accelaretion Rotation (Visual)")]
    [SerializeField] private float rotationAngle = 12f;
    [SerializeField] private float rotationSpeed = 5f;

    [Header("Movementation")]
    [SerializeField] private float maxSpeedX = 4f;
    [SerializeField] private float speedY = 5f;
    [SerializeField] private float horizontalMovement;
    public float HorizontalMovement => horizontalMovement;
    [SerializeField] private float verticalMovement;
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float friction = 5f;
    [SerializeField] private Vector2 velocity;

    [Header("Camera limit")]
    [SerializeField] private float leftHorizontalLimit = -8.5f;
    [SerializeField] private float rightHorizontalLimit = 8.5f;
    private AudioClip currentAmbienceClipVolume;


    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (rb != null) velocity = rb.linearVelocity;
        currentAmbienceClipVolume = AudioManager.Instance.rocket;
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.y == 0)
            baseY = transform.position.y;



        if (horizontalMovement > 0)
        {
            AudioManager.Instance.ambientSource.volume = 1f;
            currentAmbienceClipVolume = AudioManager.Instance.rocket;
        }
        else if (horizontalMovement <= 0)
        {
            AudioManager.Instance.ambientSource.volume = 0.4f;
            currentAmbienceClipVolume = AudioManager.Instance.rocket;
        }


        
        Move();
        Floating();
        VerticalReplacement();
        AccelerationRotation();
    }
    
    public void SetMovementInput(Vector2 input)
    {
        horizontalMovement = input.x;
        verticalMovement = input.y;
    }

    private void Move()
    {
        if (transform.position.x <= leftHorizontalLimit && horizontalMovement < 0)
            horizontalMovement = 0;
        if (transform.position.x >= rightHorizontalLimit && horizontalMovement > 0)
            horizontalMovement = 0;

        float targetVelocityX = horizontalMovement * maxSpeedX;
        float accelerationX = (Mathf.Abs(horizontalMovement) > 0.01f) ? acceleration : friction;

        velocity.x = Mathf.MoveTowards(velocity.x, targetVelocityX, accelerationX * Time.fixedDeltaTime);
        velocity.y = verticalMovement * speedY;

        if ((transform.position.x <= leftHorizontalLimit && velocity.x < 0) ||
            (transform.position.x >= rightHorizontalLimit && velocity.x > 0))
        {
            velocity.x = 0;
        }

        rb.linearVelocity = velocity;
    }

    public void AccelerationRotation()
    {
        float targetZ = 0f;

        if (horizontalMovement > 0.01f)
        {
            targetZ = (verticalMovement > 0.01f) ? rotationAngle : -rotationAngle;
        }
        else if (horizontalMovement < -0.01f)
        {
            targetZ = (verticalMovement < -0.01f) ? -rotationAngle : rotationAngle;
        }
        else
        {
            if (verticalMovement > 0.01f) targetZ = rotationAngle;
            else if (verticalMovement < -0.01f) targetZ = -rotationAngle;
        }

        Quaternion targetRot = Quaternion.Euler(0f, 0f, targetZ);
        sprite.localRotation = Quaternion.Lerp(
            sprite.localRotation,
            targetRot,
            Time.fixedDeltaTime * rotationSpeed
        );
    }

    public void Floating()
    {
        if (verticalMovement == 0f)
        {
            Vector3 pos = transform.position;
            pos.y = baseY + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
            transform.position = pos;
        }
        baseY = transform.position.y;
    }

    public void VerticalReplacement()
    {
        if (transform.position.y <= -6f)
        {
            transform.position = new Vector3(transform.position.x, 6f, 0f);
        }
        else if (transform.position.y >= 6f)
        {
            transform.position = new Vector3(transform.position.x, -6f, 0f);
        }
    }
}
