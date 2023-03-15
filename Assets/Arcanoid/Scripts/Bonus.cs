using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bonus : MonoBehaviour, IDestructable
{

    public enum BonusType { Expand, Shrink, Superball}
    public BonusType type { get; private set; } = BonusType.Expand;

    public static event System.Action<BonusType> OnBonusCollected;


    new Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        int n = Random.Range(0, 101);


        if (n < 20)
        {
            type = BonusType.Shrink;
            renderer.material.color = Color.red;
        }
        else if (n < 70)
        {
            type = BonusType.Expand;
            renderer.material.color = Color.blue;
        }
        else
        {
            type = BonusType.Superball;
            renderer.material.color = Color.green;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Collect()
    {
        OnBonusCollected?.Invoke(type);
        SelfDestruct();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bat>() != null) Collect();
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
