using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;


[System.Serializable]
public class CustomAssetReference : AssetReferenceT<Block>
{
    public CustomAssetReference(string guid) : base(guid)
    {
    }
}



public class SpawnObjectAdressables : MonoBehaviour
{

    [SerializeField] AssetReferenceGameObject prefab;

    [SerializeField] AssetReferenceSprite sprite;

    [SerializeField] CustomAssetReference block;

    List<GameObject> spawnedObjects = new List<GameObject>();

    Vector3 pos = Vector3.zero;



    // Start is called before the first frame update
    void Start()
    {
        spawnedObjects.Clear();
    }


    public void SpawnObject(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
           
            prefab.InstantiateAsync().Completed += (asyncOperation) =>
            {
                GameObject spawnedObject = asyncOperation.Result;

                if (pos == Vector3.zero) pos = spawnedObject.transform.position;

                Vector2 movement = context.ReadValue<Vector2>();

                pos.x += movement.x;
                pos.y += movement.y;

                spawnedObject.transform.Translate(new Vector3(pos.x, pos.y, 0));

                spawnedObjects.Add(spawnedObject);
            };
        }
    }


    public void DestroyObject(InputAction.CallbackContext context)
    {
        if (context.performed && spawnedObjects.Count > 0)
        {
            prefab.ReleaseInstance(spawnedObjects[0]);
            spawnedObjects.RemoveAt(0);
        }
    }


    public void DestroyAll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            foreach (GameObject item in spawnedObjects)
            {
                prefab.ReleaseInstance(item);
            }

            spawnedObjects.Clear();
        }
    }


}
