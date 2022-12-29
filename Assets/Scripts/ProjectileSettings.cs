using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="ProjectileSettings", menuName ="Game/Create Projectile Settings")]
public class ProjectileSettings : ScriptableObject
{

    [SerializeField, Min(1)] float speed = 30;
    public float Speed => speed;

    [SerializeField, Min(1)] float minSpeed = 10;
    public float MinSpeed => minSpeed;

    [SerializeField, Min(1)] float maxSpeed = 1000;
    public float MaxSpeed => maxSpeed;


    [SerializeField] bool explosive = false;
    public bool Explosive => explosive;


    [SerializeField] Projectile projectilePrefab;
    public Projectile ProjectilePrefab => projectilePrefab;


    [SerializeField, Min(1)] float timer = 5;
    public float Timer => timer;

}
