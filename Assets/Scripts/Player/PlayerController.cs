using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    // A 2D top down roguelike character controller in the style of binding of isaac
    // Using rigidbody2D physics

    public float speed = 4f;

    private Rigidbody2D rb2D;
    private Vector2 input;
    private CircleCollider2D circleCollider;

    
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        input.Normalize();
    }

    private void FixedUpdate()
    {
        rb2D.linearVelocity = input * speed;
    }


}
