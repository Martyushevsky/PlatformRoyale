using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public float health = 100;

    public void ApplyDamage(IDamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health <= 0 && gameObject)
        {
            Destroy(gameObject);
            Debug.Log(gameObject.name + " died :(");
        }
    }
}
