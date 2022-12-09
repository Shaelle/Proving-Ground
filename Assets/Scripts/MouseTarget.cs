using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseTarget : MonoBehaviour
{

    [SerializeField] TargetPointer pointerPrefab;

    TargetPointer pointer;

    [SerializeField] Turret turret;

    [SerializeField] float debugSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MouseMove(InputAction.CallbackContext context)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
     
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit mouseHit;

        if (Physics.Raycast(ray,out mouseHit))
        {

            if (pointer == null) pointer = Instantiate(pointerPrefab);
  
            pointer.transform.position = mouseHit.point;

            Vector3 lookPos = new Vector3(pointer.transform.position.x, turret.transform.position.y, pointer.transform.position.z);

           // turret.gameObject.transform.LookAt(lookPos);

            float angle;
            if (CalculateAngle(turret.CurrSpeed, out angle))
            {
                pointer.SetTarget(true);
                turret.gameObject.transform.Rotate(angle, 0, 0);
            }
            else pointer.SetTarget(false);


            turret.ShowTrajectory();

        }
    }


    private bool CalculateAngle(float speed, out float angle)
    {

        speed = debugSpeed;

        float height = pointer.transform.position.y - turret.transform.position.y;

        float distance = Vector2.Distance(new Vector2(turret.transform.position.x, turret.transform.position.z), new Vector2(pointer.transform.position.x, pointer.transform.position.z));

        float g = Physics.gravity.magnitude;
        float v2 = speed * speed;
 

    
        float sqrt = v2 * v2 - g * (g * distance * distance + 2 * height * v2);

        if (sqrt < 0)
        {
            angle = 0;
            return false;
        }
 
        float tempAngle = Mathf.Atan(v2 + Mathf.Sqrt(sqrt) / (g * distance)) * Mathf.Rad2Deg;

       // tempAngle = 90 + tempAngle;

        angle = -tempAngle;

        //Debug.Log("angle " + angle);

        return true;
    }
}
