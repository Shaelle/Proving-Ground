using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Turret : MonoBehaviour
{

    [Header("Turrert")]
    [SerializeField] float sensitivityHor = 9;
    [SerializeField] float sensitivityVert = 9;

    [SerializeField] float minVert = -45;
    [SerializeField] float maxVert = 45;

    [Header("Projectile")]
    [SerializeField] float speed = 30;

    [SerializeField] Rigidbody projectilePrefab;


    Vector2 movement;
    bool isMoving = false;

    float rotationX = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {

            rotationX -= movement.y * sensitivityVert;
            rotationX = Mathf.Clamp(rotationX, minVert, maxVert);

            float delta = movement.x * sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
          
        }
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
        if (context.phase == InputActionPhase.Performed)
        {           
            Rigidbody projectile = Instantiate(projectilePrefab, transform.position,transform.rotation);
            projectile.AddRelativeForce(Vector3.forward * speed);

            Debug.Log("Fire");
        }
    }

}
