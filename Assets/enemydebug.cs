using UnityEngine;

public class enemydebug : MonoBehaviour
{
    //A debug class to test the enemy behavior

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
