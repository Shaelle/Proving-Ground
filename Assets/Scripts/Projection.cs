using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projection : MonoBehaviour
{

    Scene simulationScene;
    PhysicsScene physicsScene;

    [SerializeField] Transform obstacles;

    [SerializeField] LineRenderer line;
    [SerializeField] int maxPhysicsFrameIterations = 100;


    // Start is called before the first frame update
    void Start() => CreatePhysicsScene();


    private void OnEnable() => MouseTarget.OnOutOfRange += HideLine;
    private void OnDisable() => MouseTarget.OnOutOfRange -= HideLine;

    private void HideLine() => line.enabled = false;


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


    public void SimulateTrajectory(Projectile projectilePrefab, Transform pos, float speed, float time, bool explosive)
    {

        if (line.enabled == false) line.enabled = true;

        bool isDestroyed = false;
        void OnDestroy() => isDestroyed = true;

        Projectile ghostObj = Instantiate(projectilePrefab, pos.position, pos.rotation);
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, simulationScene);

        ghostObj.Init(speed, time, explosive, OnDestroy, true);

        line.positionCount = 1;
        line.SetPosition(0, ghostObj.transform.position);

        while(line.positionCount < maxPhysicsFrameIterations)
        {
            
            physicsScene.Simulate(Time.fixedDeltaTime);

            if (isDestroyed) break;

            line.positionCount++;
            line.SetPosition(line.positionCount -1, ghostObj.transform.position);
            
        }

        Destroy(ghostObj.gameObject);
    }

 
}
