using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int currentHealth = 0;
    [SerializeField] int maxHealth = 0;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D RB = null;
    [SerializeField] BoxCollider2D weapon = null;

    float actionTimeLeft;
    float jumpForce = 6.0f;
    bool doubleJump = false;
    bool initDoubleJump = false;
    bool initAttacking = false;
    bool attacked = false;
    bool[] powerUps;

    //power ups
    //1 - torch (begin progress)
    //2 - double jump (two jumps for backtracking and exploring)
    //3 - sword (attacking)
    //4 - pickaxe (reach end the game)



    public void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        weapon = gameObject.GetComponentInChildren<BoxCollider2D>();
        weapon.enabled = false;
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
            weapon.enabled = true;
            Debug.Log("Player attacked");
        }

        if(actionTimeLeft >= 0.0f)
        {
            actionTimeLeft -= Time.deltaTime;
        }

        if (actionTimeLeft <= 0.5f && weapon.enabled)
        {
            weapon.enabled = false;
        }

        if (actionTimeLeft <= 0.0f && initDoubleJump == true)
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

        powerUps = GetComponentInParent<Game>().powerFound;


        if(powerUps[1] == true)
        {
            initDoubleJump = true;
        }

        if (powerUps[2] == true)
        {
            initAttacking = true;
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
            GetComponentInParent<Game>().powerFound[index] = true;
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "breakableWalls")
        {
            int BP = collision.gameObject.GetComponent<Tile>().breakingPower;
            if(BP >= 0)
            {
                if(powerUps[BP] == true)
                {
                    Destroy(collision.gameObject);
                }
            }
        }

    }

}