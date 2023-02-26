using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class Turret : MonoBehaviour
{

    [SerializeField] TurretSettings turretSettings;

    [SerializeField] ProjectileSettings projectileSettings;

    float speed = 30;
    bool explosive = false;

    [SerializeField] Projection projection;


    public static event Action<Vector3> OnChangingPosition;
    public static event Action<float> OnChangingSpeed;


    Vector2 movement;
    bool isMoving = false;

    bool canHit = true;
    bool isGUI = false;

    float rotationX = 0;


    // Start is called before the first frame update
    void Start()
    {
        speed = projectileSettings.Speed;
        explosive = projectileSettings.Explosive;

        TurretUI.OnChangingSpeed += ChangeSpeed;


        OnChangingPosition?.Invoke(transform.position);
        OnChangingSpeed?.Invoke(speed);

    }

    private void OnDestroy() => TurretUI.OnChangingSpeed -= ChangeSpeed;


    private void ChangeSpeed(float speed)
    {
        this.speed = speed;
        OnChangingSpeed?.Invoke(speed);
    }

    private void OnEnable()
    {
        TurretUI.OnTogglingExplosive += ToggleExplosive;
        
        MouseTarget.OnCanHit += CanHit;
        MouseTarget.OnOutOfRange += OutOfRange;
    }

    private void OnDisable()
    {
        TurretUI.OnTogglingExplosive -= ToggleExplosive;
        
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

            rotationX -= movement.y * turretSettings.SensitivityVert * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, turretSettings.MinVert, turretSettings.MaxVert);

            float delta = movement.x * turretSettings.SensitivityHor * Time.deltaTime;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);

            ShowTrajectory();
          
        }
    }


    public void ShowTrajectory() => projection.SimulateTrajectory(projectileSettings.ProjectilePrefab, transform, speed, projectileSettings.Timer, explosive);

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
            Projectile projectile = Instantiate(projectileSettings.ProjectilePrefab, transform.position,transform.rotation);
            projectile.Init(speed, projectileSettings.Timer, explosive);
            AudioVX.instance.PlayShoot(!explosive);
            
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
