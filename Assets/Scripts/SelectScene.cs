using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(Button))]
public class SelectScene : MonoBehaviour
{
    Button button;

    [SerializeField] string scene;

    private void Awake()
    {
        button = GetComponent<Button>();


        TextMeshProUGUI label = button.GetComponentInChildren<TextMeshProUGUI>();
        label.text = scene;
    }

    private void OnEnable() => button.onClick.AddListener(Click);

    private void OnDisable() => button.onClick.RemoveListener(Click);


    void Click() => SceneManager.LoadScene(scene);



}
