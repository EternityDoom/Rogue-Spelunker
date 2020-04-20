using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int currentHealth = 0;
    [SerializeField] int maxHealth = 0;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D RB = null;

    float actionTimeLeft;
    float jumpForce = 6.0f;
    bool doubleJump = false;
    bool initDoubleJump = false;
    bool initAttacking = false;
    bool attacked = false;
    Dictionary<int, bool> powerUps = new Dictionary<int, bool>();

    //power ups
    //1 - torch (begin progress)
    //2 - double jump (two jumps for backtracking and exploring)
    //3 - sword (attacking)
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
            if(initDoubleJump)
            {
                actionTimeLeft = 2.0f;
            }
            else
            {
                actionTimeLeft = 1.0f;
            }

        }

        if((doubleJump == true && Input.GetKeyDown(KeyCode.Space)) && actionTimeLeft <= 1.99f)
        {
            RB.AddRelativeForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            doubleJump = false;
        }

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * speed * Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && actionTimeLeft <= 0 && attacked == true)
        {
            actionTimeLeft = 1.0f;
            Debug.Log("Player attacked");
        }

        if(actionTimeLeft >= 0.0f)
        {
            actionTimeLeft -= Time.deltaTime;
        }

        if(actionTimeLeft <= 0.0f && initDoubleJump == true)
        {
            doubleJump = true;
        }

        if (actionTimeLeft <= 0.0f && initAttacking == true)
        {
            attacked = true;
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (powerUps[1] == true)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("firstWall");
            foreach(GameObject go in gos)
            {
                go.GetComponent<BoxCollider2D>().enabled = false;
                go.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        if(powerUps[2] == true)
        {
            initDoubleJump = true;
        }

        if (powerUps[3] == true)
        {
            initAttacking = true;
        }

        if (powerUps[4] == true) //just desapwns the final wall, figured we dont have the time to make an actual breaking mechanic
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("final wall");
            foreach (GameObject go in gos)
            {
                go.GetComponent<BoxCollider2D>().enabled = false;
                go.GetComponent<SpriteRenderer>().enabled = false;
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
