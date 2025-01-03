using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Each heart is 2 health points
    public int health = 6;
    public GameObject[] hearts;


    private void TakeDamage(int damage)
    {
        health -= damage;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Heal(int damage)
    {
        health += damage;

        if (health > 6)
        {
            health = 6;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }


    void Die()
    {
        //Reload the current scene for now
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }

    }

}
