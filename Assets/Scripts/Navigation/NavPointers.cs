using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPointers : MonoBehaviour
{

    [SerializeField] Transform[] points;

    public static NavPointers instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetNewPoint(Vector3 current)
    {

        if (points.Length > 0)
        {
            Vector3 newPos = points[Random.Range(0, points.Length)].position;

            while (newPos == current)
            {
                newPos = points[Random.Range(0, points.Length)].position;
            }

            return newPos;

        }
        else return current;




    }
}
