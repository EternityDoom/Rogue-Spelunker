using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int currentHealth = 0;
    [SerializeField] int maxHealth = 0;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D RB = null;
    //Vector3 movement;
    float actionTimeLeft;
    float jumpForce = 6.0f;

    Dictionary<int, bool> powerUps = new Dictionary<int, bool>();
    //for power ups, create a dictonary with the key being the int value of the power up, the value is a bool, when a power up is collected, it will search the dictonary 
    //and find the appropraite power up and turn it to true, these will be used in deteming the players actions 

    //power ups
    //1 - torch (begin progress & attack?)
    //2 - double jump
    //3 - 
    //4 - pickaxe (reach end the game)

    

    public void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        powerUps.Add(1, false);
        powerUps.Add(2, false);
        powerUps.Add(3, false);
        powerUps.Add(4, false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && actionTimeLeft <= 0)
        {
            RB.AddRelativeForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            actionTimeLeft = 1.0f;
        }

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * speed * Time.deltaTime;

        if (Input.GetMouseButtonDown(1) && actionTimeLeft == 0)
        {
            actionTimeLeft = 2.0f;
        }

        if(actionTimeLeft >= 0.0f)
        {
            actionTimeLeft -= Time.deltaTime;
        }

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (powerUps[1] == true)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("firstWall");
            foreach(GameObject go in gos)
            {
                go.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

    }

    public void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            currentHealth -= 10;
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "powerUp")
        {
            Debug.Log("Picked up power up");
            int index = collision.gameObject.GetComponent<Powerup>().Pickup();
            powerUps[index] = true;
            Destroy(collision.gameObject);
        }


    }

}
