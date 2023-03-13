using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pit : MonoBehaviour
{

    public UnityEvent<bool> OnFail;


    private void OnTriggerEnter(Collider other)
    {
            other.gameObject.SetActive(false);
            OnFail.Invoke(true);
    }

}
