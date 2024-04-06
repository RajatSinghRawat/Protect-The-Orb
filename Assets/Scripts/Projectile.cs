using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float projectileSpeed;
    [SerializeField] private float damage;

    //getters
    public float GetDamage()
    {
        return damage;
    }

    //settters
    public void SetprojectileSpeed(float speed)
    {
        projectileSpeed = speed;
    }

    private Rigidbody2D projectileRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        projectileRigidBody = GetComponent<Rigidbody2D>();
        projectileRigidBody.velocity = transform.right * projectileSpeed; // Adjust direction based on player's facing direction
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
