using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerControls inputActions;
    private PlayerMovementController movementController;
    private PlayerShootingController shootingController;

    void Awake()
    {
        inputActions = new PlayerControls();
        movementController = GetComponent<PlayerMovementController>();
        shootingController = GetComponent<PlayerShootingController>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Fire.performed += OnShoot;
        inputActions.Player.Pause.performed += OnPause;

        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.Fire.canceled += OnShoot;
        inputActions.Player.Pause.canceled += OnPause;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Fire.performed -= OnShoot;
        inputActions.Player.Pause.performed -= OnPause;

        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Fire.canceled -= OnShoot;
        inputActions.Player.Pause.canceled -= OnPause;
        inputActions.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (GameController.Instance.isPaused) return;

        Vector2 input = context.ReadValue<Vector2>();
        movementController.SetMovementInput(input);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (GameController.Instance.isPaused) return;

        if (context.performed)
        {
            shootingController.Shoot();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (!GameController.Instance.isPaused)
        {
            GameController.Instance.Options();
        }
    }
}
