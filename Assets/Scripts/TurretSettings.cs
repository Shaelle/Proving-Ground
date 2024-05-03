using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="TurretSettings", menuName ="Game/Create Turret Settings")]
public class TurretSettings : ScriptableObject
{
    [SerializeField] float sensitivityHor = 9;
    public float SensitivityHor => sensitivityHor;

    [SerializeField] float sensitivityVert = 9;
    public float SensitivityVert => sensitivityVert;

    [SerializeField] float minVert = -45;
    public float MinVert => minVert;

    [SerializeField] float maxVert = 45;
    public float MaxVert => maxVert;

}
