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

    public delegate void OnDestroy();


    [SerializeField] Material health1;
    [SerializeField] Material health2;
    [SerializeField] Material health3;

    [SerializeField] Color[] pallete;

    [SerializeField] ParticleSystem debrisParticles;

    [SerializeField] AudioClip breakSound;

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

        renderer.material.color = new Color(color.r, color.g, color.b, renderer.material.color.a);
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
            OnBlockDestroyed?.Invoke(this, DestroyBlock);
        }
    }


    void DestroyBlock()
    {
        Instantiate(debrisParticles, transform.position, Quaternion.identity);

        AudioFX.instance.Play(breakSound);

        Destroy(gameObject);
    }
}
