using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float healthDecreaseRate;
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
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
                GameManager.Instance.RemoveOneEnemy();
                Destroy(gameObject);
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
            GameManager.Instance.RemoveOneEnemy();
            Destroy(gameObject);
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
}
