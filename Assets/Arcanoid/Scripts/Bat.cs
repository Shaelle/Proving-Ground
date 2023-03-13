using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Bat : MonoBehaviour
{

    [SerializeField] PlayerInput input;

    [SerializeField, Range(1, 100)] float sensitivity = 10;

    [SerializeField, Range(1, 20)] float drag = 5;

    InputActionMap actionMap;

    InputAction move;

    Rigidbody body;



    private void Awake() => body = GetComponent<Rigidbody>();

    // Start is called before the first frame update
    void Start() => move = input.currentActionMap.FindAction("Move");


    private void OnEnable() => input.onActionTriggered += ReadAction;

    private void OnDisable() => input.onActionTriggered -= ReadAction;


    void ReadAction (InputAction.CallbackContext context)
    {

        if (context.action == move)
        {

            if (context.performed)
            {
                Vector2 movement = context.ReadValue<Vector2>();

                body.velocity = new Vector3(movement.x, 0) * sensitivity;

                body.drag = 0;

            }
            else if (context.canceled) body.drag = drag;
        }
    }

}
