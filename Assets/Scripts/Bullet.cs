using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EBullet
{
    enemyBullet,
    playerBullet
}

public interface IPoolable
{
    public void ResetElapsedTime();
  
}

public class Bullet : MonoBehaviour, IPoolable
{
    // Start is called before the first frame update
    private Rigidbody2D _rigidbody;
    public float speed;
    public float bulletLifeTime = 10;
    public float elapsedTime = 0.0f;
    public float dmg;
    public EBullet eBullet;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DestroyBulletAftetElapsedTime();
    }

    private void DestroyBulletAftetElapsedTime()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= bulletLifeTime)
        {
            gameObject.SetActive(false);

        }
    }

    public void ResetElapsedTime()
    {
            elapsedTime = 0.0f;
    }

    public void Projetile(Vector2 direction, float bulletSpeed)
    {
        speed = bulletSpeed;
        _rigidbody.AddForce(direction * speed);
       

    }

  
}
