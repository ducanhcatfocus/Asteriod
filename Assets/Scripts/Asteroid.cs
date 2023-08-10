using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    public float size;
    public float minSize = 0.5f;
    public float maxSize = 3f;
    public float speed = 50f;

    public float maxLifeTime = 30f;


    public List<Sprite> sprites = new List<Sprite>();


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count -1)];
        transform.eulerAngles = new Vector3(0,0, Random.value * 360f);
        transform.localScale = Vector3.one * size;
        _rigidbody2D.mass = size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody2D.AddForce(direction * speed);
        Destroy(gameObject, maxLifeTime);
    }



 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet.eBullet == EBullet.enemyBullet) return;
            if (size * 0.5f >= minSize)
            {
                CreateSplit();
                CreateSplit();

            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.TakeDmg(size * 10);
        }
    }

    private void CreateSplit()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;
        Asteroid newAteroid = Instantiate(this, position, Quaternion.identity);
        newAteroid.size = size * 0.5f;
        newAteroid.SetTrajectory(Random.insideUnitCircle.normalized * speed);
    }
}
