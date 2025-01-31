using UnityEngine;

public class Attributes_sc : MonoBehaviour
{
    public int MaxHealth { get; private set; } = 100;
    public int Health { get; private set; }

    public void InitializeAttributes()
    {
        Health = MaxHealth;
    }

    public void DealDamage(int damage)
    {
        Health -= damage;
        Debug.Log(gameObject + " Took " + damage + " damage");
        if (Health < 0)
        {
            Health = 0;
            Debug.Log("Attributes_sc: Character died!");
        }
        gameObject.GetComponent<HealthBar_sc>().SetValue(Health);
    }
}
