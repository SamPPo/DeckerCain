using UnityEngine;

public class Attributes_sc : MonoBehaviour
{
    [SerializeField]
    private int MaxHealth = 100;
    private int Health;

    public void InitializeAttributes()
    {
        Health = MaxHealth;
    }

    public void DealDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
            Debug.Log("Attributes_sc: Character died!");
        }

    }
}
