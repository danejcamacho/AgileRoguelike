using System;
using UnityEngine;

public class Enemy : MonoBehaviour{
    //an enemy class that moves towards the player
    public float speed = 4f;
    private Transform player;
    private Vector2 direction;
    private Rigidbody2D rb2D;
    private void Start(){
        player = GameObject.Find("Player").transform;
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update(){
        direction = (player.position - transform.position).normalized;
    }

    private void FixedUpdate(){
        rb2D.linearVelocity = direction * speed;
    }


}