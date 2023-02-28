using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(TextMeshProUGUI))]
public class EventShowSpeed : MonoBehaviour
{

    TextMeshProUGUI text;


    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void SetSpeed(float speed)
    {
        text.text = speed.ToString("0.##");
    }
}
