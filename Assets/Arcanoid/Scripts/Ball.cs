using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody body;

    Vector3 defaultPos;

    [SerializeField, Min(1)] float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();

        body.isKinematic = true;

        defaultPos = transform.position;
    }


    public  void FireBall()
    {

        if (Vector3.Distance(body.velocity, Vector3.zero) < 0.01f)
        {

            body.isKinematic = false;

            Vector3 initForce = new Vector3(Random.Range(-8, 8), 4).normalized;

            body.velocity = (initForce * speed);
        }
    }

    private void LateUpdate()
    {
        if (!body.isKinematic) body.velocity = (Vector3.Normalize(body.velocity) * speed);
    }


    public void ResetBall()
    {

        gameObject.transform.position = defaultPos;

        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;

        gameObject.SetActive(true);

        FireBall();
    }

    public void StopBall()
    {
        body.isKinematic = true;
    }

}
