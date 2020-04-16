using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int currentHealth = 0;
    [SerializeField] int maxHealth = 0;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D RB = null;
    PlayerActions action;
    Vector3 movement;
    float actionTimeLeft;
    float jumpForce = 4.0f;

    public void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && actionTimeLeft == 0)
        {
            RB.AddRelativeForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            action.type = PlayerActions.Action.Jump;
            actionTimeLeft = 2.0f;
        }

        movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
        if (Input.GetAxis("Horizontal") != 0.0f) { action.type = PlayerActions.Action.Move; }

        if (Input.GetMouseButtonDown(1) && actionTimeLeft == 0)
        {
            action.type = PlayerActions.Action.Attack;
            actionTimeLeft = 3.0f;
        }

        if(Input.GetAxis("Horizontal") == 0.0f)
        {
            action.type = PlayerActions.Action.None;
        }

        if(actionTimeLeft > 0.0f)
        {
            actionTimeLeft -= Time.deltaTime;
        }

        if(currentHealth <= 0)
        {
            //Not 100% sure what is ment to happen on death so this is just a skeleton at the moment
        }

    }

    public void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            currentHealth -= 10;
            Destroy(collision.gameObject);
        }
    }

}
