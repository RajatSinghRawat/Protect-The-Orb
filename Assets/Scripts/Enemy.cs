using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float healthDecreaseRate;
    [SerializeField] private float speed;
    [SerializeField] private EnemyType typeOfEnemy;
    private ScoreManager scoreManagerController;
    private Animator enemyAnimator;

    //getters
    public EnemyType GetTypeOfEnemy()
    {
        return typeOfEnemy;
    }

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    public void SetTransform(Vector2 EnemyPosition, Vector2 EnemyDirection, float RotationOffset)
    {
        gameObject.transform.position = EnemyPosition;
        float angle = Mathf.Atan2(EnemyDirection.y, EnemyDirection.x) * Mathf.Rad2Deg;
        // Create a rotation based on the calculated angle
        Quaternion rotation = Quaternion.AngleAxis(angle + RotationOffset, Vector3.forward);
        gameObject.transform.rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public void SetScoreManagerReference(ScoreManager scoreManagerObject)
    {
      scoreManagerController = scoreManagerObject.GetComponent<ScoreManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Projectile ProjectileObject = collision.gameObject.GetComponent<Projectile>();
        Orb OrbObject = collision.gameObject.GetComponent<Orb>();
        if (ProjectileObject != null) 
        {
            GetDamage(ProjectileObject.GetDamage());
        }
        else if (OrbObject != null) 
        {
            speed = 0f;
            startAttack(OrbObject);
        }
    }

    private void startAttack(Orb lightOrb)
    {
        enemyAnimator.SetBool("Attack", true);
        StartCoroutine(AttackLightOrb(lightOrb));      
    }

    private void Attack(Orb lightOrb)
    {
        if(lightOrb != null)
        {
            lightOrb.GetDamage(damage);
            //Enemy health decreases while attacking
            health -= healthDecreaseRate;
            if (CheckIfAlive())
            {
                StartCoroutine(AttackLightOrb(lightOrb));
            }
            else
            {
                KillEnemy();
            }
        }        
    }

    private IEnumerator AttackLightOrb(Orb lightOrb)
    {
        yield return new WaitForSeconds(1f);
        Attack(lightOrb);
    }

    private void GetDamage(float damageValue)
    {
        health -= damageValue;
        if (!CheckIfAlive())
        {
            KillEnemy();
        }
    }

    private bool CheckIfAlive()
    {
        if(health <= 0f)
        {
            return false;
        }
        return true;
    } 
    
    private void KillEnemy()
    {
        GameManager.Instance.RemoveOneEnemy();
        scoreManagerController.increaseScore(typeOfEnemy);
        Destroy(gameObject);
    }
}

public enum EnemyType
{
    Blue_Striped_Spider,
    BlueWithOrangeSpots_Spider,
    Gray_Spider,
    Red_Spider,
    Yellow_Spider
}
