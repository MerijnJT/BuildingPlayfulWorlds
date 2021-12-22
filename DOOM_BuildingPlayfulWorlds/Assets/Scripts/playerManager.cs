using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public class playerManager : MonoBehaviour
{
    public int maxHealth = 150;
    public int currentHealth;

    public HealthBar healthBar;

    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("auwie");
        if(other.gameObject.tag == "Claw")
        {
            TakeDamage(25);
        }
    }

    public void TakeDamage( int damage )
    {
        currentHealth -= damage;
        Debug.Log(transform.name + "takes" + damage + "damage");

        healthBar.ChangeHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("You Die");
            FindObjectOfType<Manager>().EndGame();

            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;



        }
    }

    
}
