using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projection : MonoBehaviour
{

    Scene simulationScene;
    PhysicsScene physicsScene;

    [SerializeField] Transform obstacles;

    // Start is called before the first frame update
    void Start()
    {
        CreatePhysicsScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreatePhysicsScene()
    {
        simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        physicsScene = simulationScene.GetPhysicsScene();


        foreach (Transform obj in obstacles)
        {
            GameObject ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            ghostObj.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObj, simulationScene);
        }
    }


    [SerializeField] LineRenderer line;
    [SerializeField] int maxPhysicsFrameIterations = 100;

    public void SimulateTrajectory(Projectile projectilePrefab, Transform pos, float speed, float time)
    {
        Projectile ghostObj = Instantiate(projectilePrefab, pos.position, pos.rotation);
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, simulationScene);

        ghostObj.Init(speed, time);

        line.positionCount = maxPhysicsFrameIterations;

        for (int i = 0; i < maxPhysicsFrameIterations; i++)
        {
            physicsScene.Simulate(Time.fixedDeltaTime);
            line.SetPosition(i, ghostObj.transform.position);
        }

        Destroy(ghostObj.gameObject);
    }
}
