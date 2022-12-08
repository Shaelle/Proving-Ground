using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseTarget : MonoBehaviour
{

    [SerializeField] GameObject pointerPrefab;

    GameObject pointer;

    [SerializeField] Turret turret;

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

            turret.gameObject.transform.LookAt(lookPos);

            turret.ShowTrajectory();

        }
    }
}
