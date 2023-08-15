using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;


public enum EEnemy
{
    normal,
    epic,
    tank,
    boss,
}

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float hp;
    public float speed;
    public float dmg;
    public float range;
    public float def;
    public float firerate;
    public float colliderDmg;
    public float exp;
    public float bulletSpeed;

    public EnemySO enemySO;

    public EEnemy enemyType;
    private float currentTime = 0;
    

    public Player playerPos;
    public Bullet bulletPrefab;
    public Sprite bulletSprite;
    public SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        InitEnemy();
        playerPos = FindObjectOfType<Player>();
    }
    
    private void InitEnemy()
    {
        int randomIndex = UnityEngine.Random.Range(0, enemySO.sprites.Count);
        spriteRenderer.sprite = enemySO.sprites[randomIndex];
        hp = enemySO.hp;
        speed = enemySO.speed;  
        dmg = enemySO.dmg;
        range = enemySO.range;
        def = enemySO.def;
        firerate = enemySO.firerate;
        colliderDmg = enemySO.colliderDmg;
        exp = enemySO.exp;
        bulletSprite = enemySO.bulletSprites[randomIndex];
        bulletSpeed = enemySO.bulletSpeed;
    }

    private void EnemyMove()
    {
        if (playerPos != null)
        {
 
            Vector3 directionToTarget = (playerPos.transform.position - transform.position).normalized;

            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);

            float distanceToPlayer = Vector3.Distance(transform.position, playerPos.transform.position);
            if(distanceToPlayer >= range)
            {
                transform.position += speed * Time.deltaTime * transform.right;


            }
            else if(enemyType != EEnemy.tank)
            {
                EnemyAttack();
            }

        }

    }

    private void EnemyAttack()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= firerate)
        {
            currentTime = 0;
            Bullet bullet = PoolingManager.Instance.GetFromPool(bulletPrefab);
            
            bullet.transform.position = transform.position;
            bullet.Projetile(transform.right, bulletSpeed);
            bullet.dmg = dmg;
            bullet.eBullet = EBullet.enemyBullet;
            SpriteRenderer spriteRenderer = bullet.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = bulletSprite;
        }
      
    }



    // Update is called once per frame
    void Update()
    {
        EnemyMove();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet")) {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet.eBullet == EBullet.enemyBullet) return;
            TakeDmg(bullet.dmg);
            collision.gameObject.SetActive(false);
        }
    }

    private void TakeDmg(float dmg)
    { 
            hp -= dmg;
            if (hp <= 0)
            {
            Player player = playerPos.GetComponent<Player>();
            player.GetExp(exp);
            Destroy(gameObject);
            }
    }
}
