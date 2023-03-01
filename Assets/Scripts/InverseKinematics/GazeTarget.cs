using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GazeTarget : MonoBehaviour
{

    new MeshRenderer renderer;

    public static event Action<Transform> OnLooking;

    private void Awake() => renderer = GetComponent<MeshRenderer>();


    private void OnMouseOver() => renderer.material.color = Color.yellow;

    private void OnMouseExit() => renderer.material.color = Color.white;

    private void OnMouseDown() => OnLooking?.Invoke(transform);
}
