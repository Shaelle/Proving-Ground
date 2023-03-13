using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class EndGameMessage : MonoBehaviour
{

    TextMeshProUGUI label;

    private void Awake()
    {
        label = GetComponent<TextMeshProUGUI>();
        label.gameObject.SetActive(false);
    }


    public void ShowMessage(bool isShown) => label.gameObject.SetActive(isShown);

}
