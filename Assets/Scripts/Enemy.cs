﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    public Vector2 movement { get; set; } = Vector2.left;
    void Update()
    {
        transform.Translate(movement * speed * Time.deltaTime);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Mathf.Abs(collision.collider.transform.position.y - collision.otherCollider.transform.position.y) <= 0.5f)
        {
            Debug.Log("Enemy hit with height gap " + Mathf.Abs(collision.collider.transform.position.y - collision.otherCollider.transform.position.y));
            if (movement == Vector2.left)
            {
                movement = Vector2.right;
            }
            else
            {
                movement = Vector2.left;
            }
        }
    }
}
