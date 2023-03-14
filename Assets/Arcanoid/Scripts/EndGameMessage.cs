using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class EndGameMessage : MonoBehaviour
{

    TextMeshProUGUI label;

    [SerializeField] AudioClip sound;

    private void Awake()
    {
        label = GetComponent<TextMeshProUGUI>();
        label.gameObject.SetActive(false);
    }


    public void ShowMessage(bool isShown)
    {
        label.gameObject.SetActive(isShown);

        if (isShown) AudioFX.instance.Play(sound);
    }
}
