using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class Turret : MonoBehaviour
{

    [Header("Turrert")]
    [SerializeField] float sensitivityHor = 9;
    [SerializeField] float sensitivityVert = 9;

    [SerializeField] float minVert = -45;
    [SerializeField] float maxVert = 45;

    [Header("Projectile")]
    [SerializeField, Min(1)] float speed = 30;
    [SerializeField, Min(1)] float minSpeed = 10;
    [SerializeField, Min(1)] float maxSpeed = 1000;


    [SerializeField] bool explosive = false;

    [SerializeField] Projectile projectilePrefab;

    [SerializeField, Min(1)] float timer = 5;

    [Header("Projection")]

    [SerializeField] Projection projection;

    [Header("UI")]

    [SerializeField] Toggle button;
    [SerializeField] Slider slider;


    public static Action<Vector3> OnChangingPosition;
    public static Action<float> OnChangingSpeed;


    Vector2 movement;
    bool isMoving = false;

    bool canHit = true;
    bool isGUI = false;

    float rotationX = 0;


    // Start is called before the first frame update
    void Start()
    {
        button.isOn = explosive;

        slider.minValue = minSpeed;
        slider.maxValue = maxSpeed;
        slider.value = speed;

        slider.onValueChanged.AddListener(ChangeSpeed);

        OnChangingPosition?.Invoke(transform.position);
        OnChangingSpeed?.Invoke(speed);

    }

    private void OnDestroy() => slider.onValueChanged.RemoveListener(ChangeSpeed);


    private void ChangeSpeed(float speed)
    {
        this.speed = speed;
        OnChangingSpeed?.Invoke(speed);
    }

    private void OnEnable()
    {
        button.onValueChanged.AddListener(ToggleExplosive);
        
        MouseTarget.OnCanHit += CanHit;
        MouseTarget.OnOutOfRange += OutOfRange;
    }

    private void OnDisable()
    {
        button.onValueChanged.RemoveListener(ToggleExplosive);
        
        MouseTarget.OnCanHit -= CanHit;
        MouseTarget.OnOutOfRange -= OutOfRange;
    }


    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            isGUI = EventSystem.current.IsPointerOverGameObject();
        }

        if (isMoving)
        {

            rotationX -= movement.y * sensitivityVert * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, minVert, maxVert);

            float delta = movement.x * sensitivityHor * Time.deltaTime;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);

            ShowTrajectory();
          
        }
    }


    public void ShowTrajectory() => projection.SimulateTrajectory(projectilePrefab, transform, speed, timer, explosive);

    public void Move(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Started) isMoving = true;
        else if (context.phase == InputActionPhase.Performed) movement = context.ReadValue<Vector2>();
        else isMoving = false;
    }


    public void Fire(InputAction.CallbackContext context)
    {

        if (canHit && context.phase == InputActionPhase.Canceled && !isGUI)
        {           
            Projectile projectile = Instantiate(projectilePrefab, transform.position,transform.rotation);
            projectile.Init(speed, timer, explosive);
            
        }
    }


    private void ToggleExplosive(bool value) => explosive = value;


    private void CanHit(Vector3 lookPos, float angle)
    {
        gameObject.transform.LookAt(lookPos);
        gameObject.transform.Rotate(angle, 0, 0);

        ShowTrajectory();

        canHit = true;
    }

    private void OutOfRange() => canHit = false;


}
