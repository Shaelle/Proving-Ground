using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Block : MonoBehaviour
{

    public static event Action OnBlockDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionExit(Collision collision)
    {

        OnBlockDestroyed?.Invoke();

        Destroy(this.gameObject);
    }
}
