using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField] private float OrbHealth;
    private SpriteRenderer objectColor; 

    private void Start()
    {
        objectColor = GetComponent<SpriteRenderer>();
        OrbHealth = objectColor.color.a;
    }

    public void GetDamage(float damageValue)
    {
        var tempColor = objectColor.color;
        tempColor.a -= damageValue / 500f;
        objectColor.color = tempColor;
        OrbHealth -= damageValue / 500f;
        if (!CheckIfAlive())
        {
            Destroy(gameObject);
        }
    }

    private bool CheckIfAlive()
    {
        if (OrbHealth <= 0f)
        {
            return false;
        }
        return true;
    }
}
