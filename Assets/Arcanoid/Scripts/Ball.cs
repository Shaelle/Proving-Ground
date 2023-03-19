using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour, IDestructable
{
    Rigidbody body;

    Vector3 defaultPos;

    [SerializeField, Min(1)] float speed = 10;

    [SerializeField] AudioClip hitSound;

    [SerializeField] Material superMaterial;

    [SerializeField] ParticleSystem sparks;


    public static event System.Action<bool> SuperActivated;

    public UnityEvent OnFail;

    Material defaultMaterial;

    Renderer renderer;

    [SerializeField, Range(1, 30)] int bonusTime = 20;
    int timer = 0;

    bool isSuper = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();

        body.isKinematic = true;

        defaultPos = transform.position;

        renderer = GetComponent<Renderer>();

        defaultMaterial = renderer.material;
    }


    private void OnEnable()
    {
        Bonus.OnBonusCollected += CollectBonus;
    }

    private void OnDisable()
    {
        Bonus.OnBonusCollected -= CollectBonus;
    }


    void CollectBonus(Bonus.BonusType type)
    {
        if (type == Bonus.BonusType.Superball)
        {
            timer = bonusTime;
            if (!isSuper) StartCoroutine(SuperBall());  
        }
    }


    IEnumerator SuperBall()
    {

        isSuper = true; 

        renderer.material = superMaterial;

        SuperActivated?.Invoke(true);

        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
        }

        renderer.material = defaultMaterial;

        SuperActivated?.Invoke(false);

        isSuper = false;

    }


    public  void FireBall()
    {

        if (Vector3.Distance(body.velocity, Vector3.zero) < 0.01f)
        {

            body.isKinematic = false;

            body.velocity = Vector3.zero;

            Vector3 initForce = new Vector3(Random.Range(-8, 8), 4).normalized;

            body.velocity = (initForce * speed);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        AudioFX.instance.Play(hitSound);

        Instantiate(sparks, collision.GetContact(0).point, Quaternion.identity);
    }

    private void LateUpdate()
    {
        if (!body.isKinematic) body.velocity = (Vector3.Normalize(body.velocity) * speed);
    }


    public void ResetBall()
    {

        gameObject.transform.position = defaultPos;

        gameObject.SetActive(true);

        renderer.material = defaultMaterial;


        timer = 0;
        isSuper = false;

        SuperActivated?.Invoke(false);

        FireBall();
    }

    public void StopBall()
    {
        body.isKinematic = true;
    }


    public void SelfDestruct()
    {
        OnFail.Invoke();
        gameObject.SetActive(false);    
    }

}
