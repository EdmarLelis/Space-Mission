using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    [Header("Shoot")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireCooldown = 0.7f;
    
    private float lastFireTime;

    public void Shoot()
    {
        if (Time.time >= lastFireTime + fireCooldown)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.laser, 0.4f);
            lastFireTime = Time.time;
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}