using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletZeroPrefab;
    public GameObject bulletOnePrefab;
    bool flip = false;
    public int bulletSpeed = 12;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the left mouse button is pressed

        if(Input.GetMouseButtonDown(0))
        {
            //Instantiate a bullet object towards the direction of the mouse click
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - transform.position).normalized;
            if (flip)
            {
                GameObject bullet = Instantiate(bulletOnePrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
            }
            else
            {
                GameObject bullet = Instantiate(bulletZeroPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
            }
            flip = !flip;

        }
    }
}
