using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    [SerializeField] private Image gear;
    private float gearSpeed;
    private float gearRotation = 25;

    void FixedUpdate()
    {
        GearRotation();
        SetGearSpeed();
    }

    private void GearRotation()
    {
        gear.transform.Rotate(0f, 0f, -(gearSpeed * Time.fixedDeltaTime));
    }

    private void SetGearSpeed()
    {
        gearSpeed = slider.value * gearRotation;
    }

    public void SetMaxHelth(int health)
    {
        slider.maxValue = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
