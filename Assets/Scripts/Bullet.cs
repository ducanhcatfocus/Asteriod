using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EBullet
{
    enemyBullet,
    playerBullet
}
public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rigidbody;
    public float speed;
    public float bulletLifeTime = 10;
    public float dmg;
    public EBullet eBullet;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Projetile(Vector2 direction, float bulletSpeed)
    {
        speed = bulletSpeed;
        _rigidbody.AddForce(direction * speed);
      
        Destroy(gameObject, bulletLifeTime);
    }

  
}
