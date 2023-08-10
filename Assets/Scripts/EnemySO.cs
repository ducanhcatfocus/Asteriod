using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class EnemySO: ScriptableObject
{
    public float hp;
    public float speed;
    public float dmg;
    public float colliderDmg;
    public float range;
    public float def;
    public float firerate;
    public float bulletSpeed;
    public float exp;
    public List<Sprite> sprites;
    public List<Sprite> bulletSprites;



}
