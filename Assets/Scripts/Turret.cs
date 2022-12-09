using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class Turret : MonoBehaviour
{

    [Header("Turrert")]
    [SerializeField] float sensitivityHor = 9;
    [SerializeField] float sensitivityVert = 9;

    [SerializeField] float minVert = -45;
    [SerializeField] float maxVert = 45;

    [Header("Projectile")]
    [SerializeField] float speed = 30;
    [SerializeField] float maxSpeed = 1000;
    [SerializeField] float speedBust = 10;

    [SerializeField] bool explosive = false;

    float currSpeed = 0;
    public float CurrSpeed => currSpeed * Time.deltaTime;

    [SerializeField] Projectile projectilePrefab;

    [SerializeField, Min(1)] float timer = 5;

    [SerializeField] Projection projection;

    [SerializeField] Toggle button;


    Vector2 movement;
    bool isMoving = false;
    bool isPowering = false;

    float rotationX = 0;


    // Start is called before the first frame update
    void Start()
    {
        button.isOn = explosive;
        currSpeed = speed;
    }


    private void OnEnable() => button.onValueChanged.AddListener(ToggleExplosive);

    private void OnDisable() => button.onValueChanged.RemoveListener(ToggleExplosive);


    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {

            rotationX -= movement.y * sensitivityVert * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, minVert, maxVert);

            float delta = movement.x * sensitivityHor * Time.deltaTime;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);

            ShowTrajectory();
          
        }

        if (isPowering)
        {
            currSpeed += speedBust;
            ShowTrajectory();
        }
    }


    public void ShowTrajectory()
    {
        projection.SimulateTrajectory(projectilePrefab, transform, currSpeed, timer, explosive);
    }


    public void Move(InputAction.CallbackContext context)
    {

        switch (context.phase)
        {
            case InputActionPhase.Disabled:
                break;

            case InputActionPhase.Waiting:
                break;

            case InputActionPhase.Started:
                isMoving = true;
                break;
            case InputActionPhase.Performed:
                movement = context.ReadValue<Vector2>();
                break;

            case InputActionPhase.Canceled:
                isMoving = false;
                break;

            default:
                isMoving = false;
                break;
        }


    }

    public void Fire(InputAction.CallbackContext context)
    {

  

        if (context.phase == InputActionPhase.Started)
        {
         //   currSpeed = speed;
         //   isPowering = true;
          
        }
        else if (context.phase == InputActionPhase.Canceled)
        {           
            Projectile projectile = Instantiate(projectilePrefab, transform.position,transform.rotation);
            projectile.Init(currSpeed, timer, explosive);

            isPowering = false;

           // Debug.Log("Fire. New speed " + currSpeed);

            currSpeed = speed;

            
        }
    }


    private void ToggleExplosive(bool value)
    {
        explosive = value;
    }


}
