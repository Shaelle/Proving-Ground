using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    Rigidbody body;

    private void Awake() => body = GetComponent<Rigidbody>();

    public void Init(float speed, float time)
    {
        body.AddRelativeForce(Vector3.forward * speed);

        StartCoroutine(Timer(time));
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(this.gameObject);
    }

  

 
}
