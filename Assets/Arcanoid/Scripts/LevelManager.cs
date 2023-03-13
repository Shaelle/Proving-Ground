using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput))]
public class LevelManager : MonoBehaviour
{


    public UnityEvent OnRestart;
    public UnityEvent OnBeginPlay;

    public UnityEvent OnWin;
    public UnityEvent OnLose;


    PlayerInput input;


    enum Status { ready, play, endgame}
    Status status = Status.ready;


    InputActionMap actionMap;
    InputAction trigger;


    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        trigger = input.currentActionMap.FindAction("Fire");
    }

    private void OnEnable() => input.onActionTriggered += ReadAction;
    private void OnDisable() => input.onActionTriggered -= ReadAction;


    void ReadAction(InputAction.CallbackContext context)
    {
        if (context.performed && context.action == trigger)
        {
            if (status == Status.endgame)
            {
                OnRestart.Invoke();
                status = Status.ready;
            }
            else if (status == Status.ready)
            {
                OnBeginPlay.Invoke();
                status = Status.play;
            }
        }

    }


    public void Lose()
    {
        OnLose.Invoke();
        status = Status.endgame;
    }


    public void Win()
    {
        OnWin.Invoke();
        status = Status.endgame;
    }


}
