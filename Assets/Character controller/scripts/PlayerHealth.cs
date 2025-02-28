using UnityEngine;
public class PlayerHealth : MonoBehaviour
{
        public int maxHealth;
        public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"Damage Amount: {currentHealth}");

        if(currentHealth <= 0)
        {
            youdied();
        }

    }
    void youdied()
    {
        Destroy(gameObject);

    }
}
    