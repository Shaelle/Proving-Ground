using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        IDestructable item = other.GetComponent<IDestructable>();

        if (item != null) item.SelfDestruct();
    }

}
