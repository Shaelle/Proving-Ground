using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointer : MonoBehaviour
{

    [SerializeField] GameObject target;

    private void OnEnable()
    {
        MouseTarget.OnCanHit += CanHit;
        MouseTarget.OnOutOfRange += OutOfReach;
    }

    private void OnDisable()
    {
        MouseTarget.OnCanHit -= CanHit;
        MouseTarget.OnOutOfRange -= OutOfReach;
    }

    private void CanHit(Vector3 pos, float angle) => target.SetActive(true);

    private void OutOfReach() => target.SetActive(false);


}
