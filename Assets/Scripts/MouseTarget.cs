using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;



public class MouseTarget : MonoBehaviour
{

    [SerializeField] TargetPointer pointerPrefab;

    TargetPointer pointer;

    Vector3 turret;
    float turretSpeed;

    public static Action<Vector3, float> OnCanHit;
    public static Action OnOutOfRange;

    private void OnEnable()
    {
        Turret.OnChangingPosition += SetPos;
        Turret.OnChangingSpeed += SetSpeed;
    }

    private void OnDisable()
    {
        Turret.OnChangingPosition -= SetPos;
        Turret.OnChangingSpeed -= SetSpeed;
    }

    private void SetPos(Vector3 pos) => turret = pos;
    private void SetSpeed(float speed) => turretSpeed = speed;


    public void MouseMove(InputAction.CallbackContext context)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
     
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit mouseHit;

        if (Physics.Raycast(ray,out mouseHit))
        {

            if (pointer == null) pointer = Instantiate(pointerPrefab);
  
            pointer.transform.position = mouseHit.point;

            Vector3 lookPos = new Vector3(pointer.transform.position.x, turret.y, pointer.transform.position.z);

           
            float angle;
            if (CalculateAngle(turretSpeed, out angle)) OnCanHit?.Invoke(lookPos, angle);
            else OnOutOfRange?.Invoke();


        }
    }


    private bool CalculateAngle(float speed, out float angle)
    {
        speed *= Time.fixedDeltaTime;
        float height = pointer.transform.position.y - turret.y;

        float distance = Vector2.Distance(new Vector2(turret.x, turret.z), new Vector2(pointer.transform.position.x, pointer.transform.position.z));

        float g = Physics.gravity.magnitude;


        float v2 = speed * speed;
   
        float sqrt = v2 * v2 - g * (g * distance * distance + 2 * height * v2);

     
        if (sqrt < 0)
        {
            angle = 0;
            return false;
        }

        float up = v2 + Mathf.Sqrt(sqrt);
        float down = (g * distance);


        float tempAngle = Mathf.Atan(up / down) * Mathf.Rad2Deg;

        angle = -tempAngle;

        return true;
    }
}
