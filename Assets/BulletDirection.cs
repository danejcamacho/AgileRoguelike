using UnityEditor.Callbacks;
using UnityEngine;

public class BulletDirection : MonoBehaviour
{
    public float force = 10f;
    public Camera MainCam;
    private Rigidbody2D rb;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MainCam = Camera.main;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        Vector3 rotation = transform.position - mousePosition;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
