using UnityEngine;

public class BarrelController : MonoBehaviour
{
    public float rotationSpeed = 10f;

    void Update()
    {
        BarrelRotation(); 
    }

    private void BarrelRotation()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = (mouseWorldPos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Create a rotation based on the calculated angle
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // Rotate the object towards the mouse position with a smooth interpolation
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }    
}

