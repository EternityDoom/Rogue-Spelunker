using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int currentHealth = 0;
    [SerializeField] int maxHealth = 0;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D RB = null;
    Vector2 momentum;
    PlayerActions action;
    float actionTimeLeft;
    float jumpForce = 4.0f;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RB.AddRelativeForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            action.type = PlayerActions.Action.Jump;
        }

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
        if (Input.GetAxis("Horizontal") != 0.0f) { action.type = PlayerActions.Action.Move; }

        if (Input.GetMouseButtonDown(1))
        {
            action.type = PlayerActions.Action.Attack;
        }

        if(Input.GetAxis("Horizontal") == 0.0f)
        {
            action.type = PlayerActions.Action.None;
        }
    }
}
