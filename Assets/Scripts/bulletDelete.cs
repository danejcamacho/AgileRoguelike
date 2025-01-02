using System.Collections;
using UnityEngine;

public class bulletDelete : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DeleteBullet());
    }

    IEnumerator DeleteBullet()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2);
        // Destroy the bullet object
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Destroy the bullet object when it collides with another object
        Destroy(gameObject);
    }
}
