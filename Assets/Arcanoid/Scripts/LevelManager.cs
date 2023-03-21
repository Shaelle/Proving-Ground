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
    public UnityEvent OnNewLife;

    public UnityEvent<int> UpdateScore;


    PlayerInput input;


    enum Status { loading, ready, play, newLife, lose, win}
    Status status = Status.loading;


    InputActionMap actionMap;
    InputAction trigger;

    [SerializeField, Min(1)] int scorePerBlock = 100;
    int score = 0;


    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        trigger = input.currentActionMap.FindAction("Fire");
    }


    private void Start()
    {
        UpdateScore.Invoke(score);
    }

    private void OnEnable()
    {
        input.onActionTriggered += ReadAction;
        Block.OnBlockDestroyed += AddScore;
    }

    private void OnDisable()
    {
        input.onActionTriggered -= ReadAction;
        Block.OnBlockDestroyed -= AddScore;
    }

    void ReadAction(InputAction.CallbackContext context)
    {
        if (context.performed && context.action == trigger)
        {

            if (status == Status.lose || status == Status.win)
            {
                OnRestart.Invoke();

                if (status == Status.lose)
                {
                    score = 0;
                    UpdateScore.Invoke(score);
                }

                status = Status.loading;
            }
            else if (status == Status.ready)
            {
                OnBeginPlay.Invoke();
                status = Status.play;
            }
            else if (status == Status.newLife)
            {
                OnNewLife.Invoke();
                status = Status.play;
            }
        }

    }


    public void LevelLoaded()
    {
        if (status == Status.loading) status = Status.ready;
    }

    public void Lose()
    {
        OnLose.Invoke();
        status = Status.lose;
    }


    public void Win()
    {
        OnWin.Invoke();
        status = Status.win;
    }


    public void LostLife()
    {
        status = Status.newLife;
    }


    private void AddScore(Block block, Block.OnDestroy destroy)
    {
        score += scorePerBlock;

        UpdateScore.Invoke(score);
    }


}
