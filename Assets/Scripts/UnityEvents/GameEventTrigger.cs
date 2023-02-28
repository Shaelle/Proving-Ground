using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameEventTrigger : MonoBehaviour
{

    [SerializeField] GameEvent gameEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) gameEvent.TriggerEvent();
    }
}
