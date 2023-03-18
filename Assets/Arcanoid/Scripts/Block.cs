using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


[System.Serializable]
public class AssetReferenceBlock : AssetReferenceT<Block>
{
    public AssetReferenceBlock(string guid) : base(guid)
    {
    }
}


public class Block : MonoBehaviour
{

    public static event System.Action<Block, OnDestroy> OnBlockDestroyed;

    new MeshRenderer renderer;

    BoxCollider collider;

    public delegate void OnDestroy();


    [SerializeField] Material health1;
    [SerializeField] Material health2;
    [SerializeField] Material health3;

    [SerializeField] Color[] pallete;

    [SerializeField] ParticleSystem debrisParticles;

    [SerializeField] AudioClip breakSound;

    [SerializeField] Bonus bonusPrefab;

    [SerializeField] GameObject glassCube;

    Color color;

    int health;

    // Start is called before the first frame update
    void Start()
    {
        health = Random.Range(1, 4);

        renderer = GetComponent<MeshRenderer>();

        collider = GetComponent<BoxCollider>();

        if (pallete.Length > 0) color = pallete[Random.Range(0, pallete.Length)];

        UpdateView();
       
    }


    private void OnEnable() => Ball.SuperActivated += SuperActivated;
    private void OnDisable() => Ball.SuperActivated -= SuperActivated;

    void SuperActivated(bool isActivated) => collider.isTrigger = isActivated;


    void UpdateView()
    {
        if (health == 3) renderer.material = health3;
        else if (health == 2) renderer.material = health2;
        else
        {
            renderer.enabled = false;
            glassCube.SetActive(true);
            MeshRenderer rend = glassCube.GetComponent<MeshRenderer>();

            rend.materials[0].color = color;


           // renderer.material = health1;
        }

        renderer.material.color = new Color(color.r, color.g, color.b, renderer.material.color.a);
    }


   
    private void OnCollisionExit(Collision collision)
    {

        health--;

        if (health > 0) UpdateView();
        else
        {
            OnBlockDestroyed?.Invoke(this, DestroyBlock);
        }
    }


    private void OnTriggerEnter(Collider other) => OnBlockDestroyed?.Invoke(this, DestroyBlock);


    void DestroyBlock()
    {
        Instantiate(debrisParticles, transform.position, Quaternion.identity);

        AudioFX.instance.Play(breakSound);

        if (bonusPrefab != null && Random.Range(0, 11) > 6) Instantiate(bonusPrefab, transform.position, Quaternion.identity);


        Destroy(gameObject);
    }
}
