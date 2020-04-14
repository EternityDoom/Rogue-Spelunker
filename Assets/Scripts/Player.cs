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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            action.type = PlayerActions.Action.Jump;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
            action.type = PlayerActions.Action.Move;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
            action.type = PlayerActions.Action.Move;
        }

        if(Input.GetMouseButtonDown(1))
        {
            action.type = PlayerActions.Action.Attack;
        }

        if(GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            action.type = PlayerActions.Action.None;
        }
    }
}
