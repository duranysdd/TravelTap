using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private Animator anim;

    [Header("UI")]
    public GameObject healthBarUI;   // ← arrastra aquí tu barra
    private bool barActivated = false;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        // Mostrar barra de vida la primera vez que recibe daño
        if (!barActivated)
        {
            healthBarUI.SetActive(true);
            barActivated = true;
        }

        currentHealth -= damage;

        // Animación de daño
        anim.SetTrigger("hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        anim.SetTrigger("die");

        // Destruye al boss después de que termine la animación
        Destroy(gameObject, 1.2f);
    }

    // Esto permite a la barra leer la vida actual
    public int CurrentHealth()
    {
        return currentHealth;
    }
}
