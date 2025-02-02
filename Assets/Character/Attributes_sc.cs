using UnityEngine;

public class Attributes_sc : MonoBehaviour
{
    public int MaxHealth { get; private set; } = 100;
    public int Health { get; private set; }
    public int Armor { get; private set; } = 0;

    public void InitializeAttributes()
    {
        Health = MaxHealth;
    }

    public void DealDamage(int damage)
    {
        if (Armor > 0)
        {
            Armor -= damage;
            if (Armor < 0)
            {
                Health += Armor;
                Armor = 0;
            }
            Debug.Log("Attributes_sc" + gameObject + " Took " + damage + " damage");
            gameObject.GetComponent<HealthBar_sc>().SetValue(Health);
            return;
        }
        Health -= damage;
        Debug.Log("Attributes_sc" + gameObject + " Took " + damage + " damage");
        if (Health < 0)
        {
            Health = 0;

            //Kill character
            gameObject.GetComponent<Character_sc>().KillCharacter();
            Debug.Log("Attributes_sc: Character died!");
        }
        gameObject.GetComponent<HealthBar_sc>().SetValue(Health);
    }

    public void GainArmor(int armor)
    {
        Armor += armor;
        Debug.Log("Attributes_sc" + gameObject + " Gained " + armor + " armor");
    }
}
