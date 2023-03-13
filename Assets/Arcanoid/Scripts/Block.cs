using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    public static event System.Action OnBlockDestroyed;

    new MeshRenderer renderer;


    [SerializeField] Material health1;
    [SerializeField] Material health2;
    [SerializeField] Material health3;

    [SerializeField] Color[] pallete;

    Color color;

    int health;

    // Start is called before the first frame update
    void Start()
    {
        health = Random.Range(1, 4);

        renderer = GetComponent<MeshRenderer>();

        if (pallete.Length > 0) color = pallete[Random.Range(0, pallete.Length)];

        UpdateView();
       
    }


    void UpdateView()
    {
        if (health == 3) renderer.material = health3;
        else if (health == 2) renderer.material = health2;
        else renderer.material = health1;

        renderer.material.color = new Color(color.r, color.g, color.b);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionExit(Collision collision)
    {

        health--;

        if (health > 0) UpdateView();
        else
        {
            OnBlockDestroyed?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
