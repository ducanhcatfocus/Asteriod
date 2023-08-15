using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _turnDirection;
    private bool _goforward;

    public float bulletSpeed = 500f;
    public float speed;
    public float speedinc = 0;
    public float turnSpeed;
    public float turnSpeedInc = 0;
    public float hp = 100;
    public float curentHp;
    public float hpinc = 0;
    public float dmg;
    public float dmginc = 0;
    public float def;
    public float definc = 0;
    public float exprateinc = 1;
    public float invincibleTime = 2f;
    public float baseDmgReduce = 0;


    public int lv;
    public float initialExp;
    public float currentExp;
    public float requiredExp;
    public float multiplierExp;
    public bool isInvincible;

    public Bullet BulletPrefab;
    public Sprite bulletSprite;
    public Transform shootPos;
    public Transform rearShootPos;
    public Transform firstPos;
    public Transform secondPos;


    private bool isRearAtackActive;
    private bool isDoubleGunActive;


    public HPbar hPbar;
  
    private Rigidbody2D _rigidbody;
    private Animator animator;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
      
        curentHp = hp + hpinc;
        requiredExp = initialExp * Mathf.Pow(multiplierExp, lv - 1);

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
        PauseGame();

    }

    private void Movement()
    {
        _goforward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _turnDirection = 1f;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _turnDirection = -1f;
        }
        else
        {
            _turnDirection = 0;
        }
      
    }

    private void FixedUpdate()
    {
        if (_goforward)
        {
            _rigidbody.AddForce(transform.up * (speed + speedinc));
            animator.SetBool("IsMove", true);
        }
        else
        {
            animator.SetBool("IsMove", false);

        }

        if (_turnDirection != 0f)
        {
            _rigidbody.AddTorque(_turnDirection * (turnSpeed + turnSpeed));
        }
    }

    private void InstantiateBullet(Vector3 position, Vector3 direction)
    {
        Bullet bullet = PoolingManager.Instance.GetFromPool(BulletPrefab);
        bullet.transform.position = position;
        bullet.Projetile(direction, bulletSpeed);
        bullet.dmg = (dmg + dmginc) * (1f - baseDmgReduce);
        bullet.eBullet = EBullet.playerBullet;
        SpriteRenderer spriteRenderer = bullet.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = bulletSprite;
    }

    private void Shoot()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && GameManager.Instance.GetPauseState() == false)
        {
            if(isDoubleGunActive)
            {
                InstantiateBullet(firstPos.position, transform.up);
                InstantiateBullet(secondPos.position, transform.up);

            }
            else
            {
              

                InstantiateBullet(shootPos.position, transform.up);
            }
 
            if(isRearAtackActive)
            {
               
                InstantiateBullet(rearShootPos.position, -transform.up);
            }
            AudioManager.Instance.PlayShotSound();
        }
     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet.eBullet == EBullet.playerBullet) return;
            TakeDmg(bullet.dmg);
            collision.gameObject.SetActive(false);

        }
    }

    private void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape)  || Input.GetKeyDown(KeyCode.Tab))
        {
            GameManager.Instance.PauseGame();
            UIManager.Instance.DisplayPausePanel();
        }
    }

    public void TakeDmg(float dmg)
    {
        if (isInvincible == false)
        {
            isInvincible = true;

            curentHp -= dmg;
            float percent = (float)curentHp / (hp + hpinc); 

            if (curentHp  <= 0)
            {
                hPbar.UpdateHP(percent);
                GameManager.Instance.SetGameOver();
                return;
            }

            hPbar.UpdateHP(percent);
            animator.SetTrigger("IsDmg");
            AudioManager.Instance.playTakeDmg();

            StartCoroutine(IncincubleTime());
        }
    }
    IEnumerator IncincubleTime()
    {
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
        animator.SetTrigger("IsNormal");

    }

    public void GetExp(float exp)
    {
        currentExp += (exp * exprateinc);
        if(currentExp >= requiredExp)
        {
            LvUp();
        }
        float percent = currentExp / requiredExp;
        hPbar.UpdateExp(percent);
    }

    public void LvUp()
    {
        GameManager.Instance.PauseGame();
        UIManager.Instance.DisplayCardPanel();
        
        lv++;
        currentExp = 0;
        requiredExp = initialExp * Mathf.Pow(multiplierExp, lv - 1);
        hPbar.DisplayLv(lv);
    }

    public void AddDmg(float value)
    {
        dmginc += value;
    }

    public void AddSpeed(float value)
    {
        speedinc += value;
    }
    public void AddTurnRate(float value)
    {
        turnSpeedInc += value;
    }
    public void AddHp(float value)
    {
        hpinc += value;
        curentHp += value;
        float percent = (float)curentHp / (hp + hpinc);
        hPbar.UpdateHP(percent);

    }

    public void AddEXPgainRate(float value)
    {
        exprateinc += value;
    }

    public void AddDef(float value)
    {
        definc += value;
    }

    public void AddRearAttack()
    {
       
            isRearAtackActive = true;
        
    }
    public void AddDoubleAttack(float value)
    {
        isDoubleGunActive = true;
        baseDmgReduce = value;
    }
}
