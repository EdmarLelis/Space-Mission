using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Script")]
    [SerializeField] private MonoBehaviour healthScript;
    private IHealth healthController;

    void Awake()
    {
        healthController = healthScript as IHealth;
        if (healthController == null)
            Debug.LogError("O componente atribuído a healthScript não implementa IHealth!");
    }

    public void HandleExternalEvent(int damage = 0)
    {
        healthController.TakeDamage(damage);
    }
}