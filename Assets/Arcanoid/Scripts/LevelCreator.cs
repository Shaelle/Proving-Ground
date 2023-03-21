using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;



public class LevelCreator : MonoBehaviour
{

    [SerializeField] AssetReferenceGameObject prefab;

    [SerializeField] float minX = -5;
    [SerializeField] float maxX = 5;

    [SerializeField] float minY = 7;
    [SerializeField] float maxY = 12;

    [SerializeField] float zPoz = -3.24f;

    [SerializeField] float offset = 0.1f;

    List<Block> blocks = new List<Block>();

    public UnityEvent OnFinish;

    public UnityEvent OnBeginLoad;
    public UnityEvent OnLoaded;

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


    async public void GenerateLevel()
    {

        OnBeginLoad.Invoke();

        float x = minX;
        float y = minY;

        float size = 0;


        for (int i = blocks.Count - 1; i >= 0; i--)
        {
           if (blocks[i] != null)  prefab.ReleaseInstance(blocks[i].gameObject);
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

                    GameObject obj = await prefab.InstantiateAsync().Task;

                    Block block = obj.GetComponent<Block>();

                    if (block != null)
                    {

                        if (size == 0) size = block.GetComponent<MeshRenderer>().bounds.size.magnitude;

                        block.transform.position = new Vector3(x, y, zPoz);

                        blocks.Add(block);
                    }
                    else Debug.LogWarning("Object " + obj.name + " is not a block.");
    
                }

                y += size / 2 + offset;
            }
                     
            x += size / 2 + offset;
            
        }

        OnLoaded.Invoke();

    }





    public void RemoveBlock(Block block, Block.OnDestroy destroy)
    {

        blocks.Remove(block);

        destroy?.Invoke(prefab);

        if (blocks.Count == 0) OnFinish.Invoke();
    }

}
