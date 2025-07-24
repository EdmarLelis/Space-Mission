using UnityEngine;

public class PlayerParticleController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private ParticleSystem rocketParticle;
    [SerializeField] private ParticleSystem centerParticle;
    [SerializeField] private PlayerMovementController movementController;

    void FixedUpdate()
    {
        ParticleManagement();
    }

    public void ParticleManagement()
    {
        var rocketEmission = rocketParticle.emission;
        if (movementController.HorizontalMovement < 0)
            rocketEmission.enabled = false;
        else
            rocketEmission.enabled = true;
    }
}