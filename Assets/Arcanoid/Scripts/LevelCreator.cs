using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelCreator : MonoBehaviour
{

    [SerializeField] Block prefab;

    [SerializeField] float minX = -5;
    [SerializeField] float maxX = 5;

    [SerializeField] float minY = 7;
    [SerializeField] float maxY = 12;

    [SerializeField] float zPoz = -3.24f;

    [SerializeField] float offset = 0.1f;

    List<Block> blocks = new List<Block>();

    public UnityEvent OnFinish;

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnEnable() => Block.OnBlockDestroyed += RemoveBlock;

    private void OnDisable() => Block.OnBlockDestroyed -= RemoveBlock;


    public void GenerateLevel()
    {
        float x = minX;
        float y = minY;

        float size = prefab.GetComponent<MeshRenderer>().bounds.size.magnitude;


        for (int i = blocks.Count - 1; i >= 0; i--)
        {
           if (blocks[i] != null) Destroy(blocks[i].gameObject);
        }

        blocks.Clear();


        while (x < maxX)
        {
            y = minY;
            while (y < maxY)
            {

                int random = Random.Range(0, 5);


                if (random > 2)
                {

                    Block block = Instantiate(prefab);

                    block.transform.position = new Vector3(x, y, zPoz);

                    blocks.Add(block);
                }

                y += size / 2 + offset;
            }
                     
            x += size / 2 + offset;
            
        }

    }


    public void RemoveBlock()
    {

        if (blocks.Count > 0) blocks.RemoveAt(0);

        if (blocks.Count == 0) OnFinish.Invoke();
    }

}
