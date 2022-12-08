using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    Rigidbody body;

    bool isExplosive = false;
    bool isSimulating = false;

    [SerializeField] GameObject explosion;

    private void Awake() => body = GetComponent<Rigidbody>();

    Action OnDestroyed;

    public void Init(float speed, float time, bool explosive = false, Action OnDestroy = null, bool simulating = false)
    {
        body.AddRelativeForce(Vector3.forward * speed);

        isExplosive = explosive;
        isSimulating = simulating;

        StartCoroutine(Timer(time));

        OnDestroyed = OnDestroy;
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);

        Kill();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (isExplosive)
        {
            if (explosion != null && !isSimulating) Instantiate(explosion, transform.position, Quaternion.identity);
            Kill();
        }
    }

    private void Kill()
    {
        OnDestroyed?.Invoke();
        Destroy(this.gameObject);
    }






}
