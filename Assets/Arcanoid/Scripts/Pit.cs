using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pit : MonoBehaviour
{

    public UnityEvent<bool> OnFail;

    public UnityEvent OnRestart;

    GameObject lastObject;

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Ball>() != null)
        {

            lastObject = other.gameObject;

            other.gameObject.SetActive(false);

            OnFail.Invoke(true);
        }
    }

    public void RestartGame()
    {
        if (lastObject != null)
        {
            lastObject.GetComponent<Ball>().ResetBall();
            OnFail.Invoke(false);

            OnRestart.Invoke();
        }
    }
}
