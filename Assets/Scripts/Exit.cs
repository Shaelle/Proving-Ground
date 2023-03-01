using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{

    public void Quit()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) SceneManager.LoadScene(0);
        else Application.Quit();
    }
}
