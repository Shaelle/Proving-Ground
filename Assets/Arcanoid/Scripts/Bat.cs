using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


[RequireComponent(typeof(Rigidbody))]
public class Bat : MonoBehaviour
{

    [SerializeField] PlayerInput input;

    [SerializeField, Range(1, 100)] float sensitivity = 10;

    [SerializeField, Range(1, 20)] float drag = 5;

    InputActionMap actionMap;

    InputAction move;
    InputAction fire;

    Rigidbody body;

    public UnityEvent OnFire;

    

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        move = input.currentActionMap.FindAction("Move");
        fire = input.currentActionMap.FindAction("Fire");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnEnable()
    {
        input.onActionTriggered += ReadAction;
    }

    private void OnDisable()
    {
        input.onActionTriggered -= ReadAction;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody body = collision.rigidbody;

        if (body != null)
        {
          //  body.velocity *= 1.05f;
        }
    }


    void ReadAction (InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (context.action == move)
            {
                Vector2 movement = context.ReadValue<Vector2>();

                body.velocity = new Vector3(movement.x, 0) * sensitivity;

                body.drag = 0;
            }
            else if (context.action == fire)
            {
                OnFire.Invoke();
            }
        }
        else if (context.canceled && context.action == move) body.drag = drag;

    }


    void Move(InputAction.CallbackContext context)
    {

    }
}
