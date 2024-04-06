using UnityEngine;

public class BarrelController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private GameObject projectilePrefab;
    //[SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private KeyCode ButtonForShooting;
    [SerializeField] private Transform spawnPosition;
    // Time between shots in seconds
    [SerializeField] private float shootRate = 0.5f;
    [SerializeField] private float projectileSpeed;
    private float shootTimer = 0f;
    private Vector3 mousePosition;
    

    void Update()
    {
        BarrelRotation();
        GetPlayerInput();
        shootTimer += Time.deltaTime;
    }

    private void BarrelRotation()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = (mouseWorldPos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Create a rotation based on the calculated angle
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // Rotate the object towards the mouse position with a smooth interpolation
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, spawnPosition.position, transform.rotation);
        projectileObject.GetComponent<Projectile>().SetprojectileSpeed(projectileSpeed);
    }

    private void GetPlayerInput()
    {
       mousePosition = Input.mousePosition;
       if (Input.GetKey(ButtonForShooting) && shootTimer >= shootRate)
       {
            Shoot();
            shootTimer = 0f;
       }            
    }
}

