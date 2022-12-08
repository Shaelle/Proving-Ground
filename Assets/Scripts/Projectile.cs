using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    Rigidbody body;

    bool isExplosive = false;

    private void Awake() => body = GetComponent<Rigidbody>();

    public void Init(float speed, float time, bool explosive = false)
    {
        body.AddRelativeForce(Vector3.forward * speed);

        isExplosive = explosive;

        StartCoroutine(Timer(time));
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(this.gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (isExplosive)
        {
            Destroy(this.gameObject);
        }
    }




}
