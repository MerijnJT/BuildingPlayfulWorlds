using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;



public class playerManager : MonoBehaviour
{
    public int maxHealth = 150;
    public int currentHealth;

    public HealthBar healthBar;
    public GameObject vignette;

    public GameObject gameOverScreen;
    public GameObject executeOverlay;

    public LayerMask demon;
    public bool inExecuteRange;
    public GameObject target;

    public float executeDamage = 50;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        inExecuteRange = Physics.CheckSphere(transform.position, 4f, demon);

        target = FindClosestEnemy();

        

        if (inExecuteRange && target != null)
        {
            executeOverlay.SetActive(true);


            if (Input.GetKeyDown(KeyCode.E))
            {
                Execute(target);
            }


        } else { executeOverlay.SetActive(false); }
    }


    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Demon");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            if (!go.GetComponent<Enemy>().isWeak)
            {
                continue;
            }
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }






    private void OnTriggerEnter(Collider other)
    {
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
        vignette.GetComponent<Bloed>().DAMAGEKRIJGEN(currentHealth);
        if (currentHealth <= 0)
        {
            FindObjectOfType<Manager>().EndGame();

            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;

        }

        
    }

    public void Execute(GameObject target)
    {
        //play animation

        target.GetComponent<Enemy>().TakeDamage(executeDamage);

       if (currentHealth <= maxHealth - 50)
        { currentHealth = currentHealth + 50; }
       else { currentHealth = currentHealth + (maxHealth - currentHealth); }
        healthBar.ChangeHealth(currentHealth);
    }

}
