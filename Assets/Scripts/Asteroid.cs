using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IPoolable
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    public float size;
    public float minSize = 0.5f;
    public float maxSize = 3f;
    public float speed = 50f;

    public float elapsedTime = 0;
    public float AstrroidLifeTime = 60f;


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
        DestroyAstrroidAftetElapsedTime();
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody2D.AddForce(direction * speed);
  
    }

    public void ResetElapsedTime()
    {
        elapsedTime = 0.0f;
    }
    private void DestroyAstrroidAftetElapsedTime()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= AstrroidLifeTime)
        {
            gameObject.SetActive(false);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet.eBullet == EBullet.enemyBullet) return;
            IsCanCreateSplit();
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);


        }
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.TakeDmg(size * 10);
            IsCanCreateSplit();
            gameObject.SetActive(false);
        }
    }

    private void IsCanCreateSplit()
    {
        if (size * 0.5f >= minSize)
        {
            CreateSplit();
            CreateSplit();

        }
   
    }

    private void CreateSplit()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;
        Asteroid newAsteroid = PoolingManager.Instance.GetFromPool(this);
        newAsteroid.size = size * 0.5f;
        newAsteroid.transform.localScale = 0.5f * size * Vector3.one;
        newAsteroid._rigidbody2D.mass = 0.5f * size;
        newAsteroid.transform.position = position;
        newAsteroid.transform.rotation = Quaternion.identity;
        newAsteroid.SetTrajectory(Random.insideUnitCircle.normalized * speed);
    }
}
