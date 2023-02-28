using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[System.Serializable]
public class FloatEvent : UnityEvent<float> { }


public class TestingUnityEvents : MonoBehaviour
{

    public UnityEvent OnHit;
    public FloatEvent OnChangeSpeed;

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) OnHit.Invoke();

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            float value = Random.Range(0, 100) + (float)Random.Range(0,100) / 100;
            OnChangeSpeed.Invoke(value);
        }
    }

}
