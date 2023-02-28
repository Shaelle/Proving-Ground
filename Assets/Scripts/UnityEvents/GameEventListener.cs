using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{

    [SerializeField] GameEvent gameEvent;
    [SerializeField] UnityEvent onEventTriggered;


    private void OnEnable() => gameEvent.AddListener(this);


    private void OnDisable() => gameEvent.RemoveListener(this);


    public void OnEventTriggered() => onEventTriggered.Invoke();

}
