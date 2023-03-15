using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class Bat : MonoBehaviour
{

    [SerializeField] PlayerInput input;

    [SerializeField, Range(1, 100)] float sensitivity = 10;

    [SerializeField, Range(1, 20)] float drag = 5;

    InputActionMap actionMap;

    InputAction move;

    Rigidbody body;

    Vector3 defaultPosition;

    float defaultScale;

    const float sizeStep = 2.5f;

   



    private void Awake() => body = GetComponent<Rigidbody>();

    // Start is called before the first frame update
    void Start()
    {
        move = input.currentActionMap.FindAction("Move");

        defaultPosition = transform.position;
        defaultScale = transform.localScale.x;
    }

    private void OnEnable()
    {
        input.onActionTriggered += ReadAction;
        Bonus.OnBonusCollected += CollectBonus;
    }

    private void OnDisable()
    {
        input.onActionTriggered -= ReadAction;
        Bonus.OnBonusCollected -= CollectBonus;
    }

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


    public void ResetBat()
    {
        transform.position = defaultPosition;

        transform.localScale = new Vector3(defaultScale, transform.localScale.y, transform.localScale.z);
    }


    void CollectBonus(Bonus.BonusType type)
    {
        if (type == Bonus.BonusType.Expand) Expand();
        else if (type == Bonus.BonusType.Shrink) Shrink();
    }


    void Expand()
    {        
        if (transform.localScale.x <= defaultScale * 1.5f) transform.DOScaleX(transform.localScale.x + sizeStep, 0.5f);
    }

    void Shrink()
    {
        if (transform.localScale.x >= defaultScale / 1.5f) transform.DOScaleX(transform.localScale.x - sizeStep, 0.5f);
    }



}
