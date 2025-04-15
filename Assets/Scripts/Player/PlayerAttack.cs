
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletZeroPrefab;
    public GameObject bulletOnePrefab;
    public Transform bulletTransform;
    bool flip = false;
    public int bulletSpeed = 12;
    public bool canFire;
    private float timer;
    public float timeBetweenShots;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }




    // Update is called once per frame
    void Update()
    {
            //Instantiate a bullet object towards the direction of the mouse click
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 rotation = mousePosition - transform.position;

            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            if(!canFire){
                timer += Time.deltaTime;
                if(timer > timeBetweenShots){
                    canFire = true;
                    timer = 0;
                }
            }


        // Check if the left mouse button is pressed
        if(Input.GetMouseButtonDown(0) && canFire)
        {
            canFire = false;


            if (flip)
            {
                Instantiate(bulletOnePrefab, bulletTransform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(bulletZeroPrefab, bulletTransform.position, Quaternion.identity);
       
            }
            
            flip = !flip;

        }
    }
}
