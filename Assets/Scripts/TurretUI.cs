using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TurretUI : MonoBehaviour
{

    [SerializeField] Toggle button;
    [SerializeField] Slider slider;

    [SerializeField] ProjectileSettings projectile;

    public static Action<float> OnChangingSpeed;
    public static Action<bool> OnTogglingExplosive;

    // Start is called before the first frame update
    void Start()
    {
        button.isOn = projectile.Explosive;

        slider.minValue = projectile.MinSpeed;
        slider.maxValue = projectile.MaxSpeed;
        slider.value = projectile.Speed;
    }

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(ChangeSpeed);
        button.onValueChanged.AddListener(ToggleExplosive);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(ChangeSpeed);
        button.onValueChanged.RemoveListener(ToggleExplosive);
    }


    private void ChangeSpeed(float speed) => OnChangingSpeed?.Invoke(speed);
    private void ToggleExplosive(bool explosive) => OnTogglingExplosive?.Invoke(explosive);

}
