using UnityEngine;

public class PlayerInteract : MonoBehaviour
{   
    public Transform intup;
    public Transform intdown;
    public Transform intleft;
    public Transform intright;


    RaycastHit2D down;
    RaycastHit2D up;
    RaycastHit2D left;
    RaycastHit2D right;
    void Start(){


    }


    void Update(){
        if (Input.GetKeyDown(KeyCode.E)){
            down = Physics2D.Raycast(intdown.position, Vector2.down, 0.5f);
            up = Physics2D.Raycast(intup.position, Vector2.up, 0.5f);
            left = Physics2D.Raycast(intleft.position, Vector2.left, 0.5f);
            right = Physics2D.Raycast(intright.position, Vector2.right, 0.5f);
            Debug.Log("E pressed");
            if (down.collider != null){
                RunInteract(down.collider.gameObject);
            }
            if (up.collider != null){
                RunInteract(up.collider.gameObject);
            }
            if (left.collider != null){
                RunInteract(left.collider.gameObject);
            }
            if (right.collider != null){
                RunInteract(right.collider.gameObject);
            }
        }


    }

    void RunInteract(GameObject obj){
        Debug.Log("Interacting with " + obj.name);
        if (obj.CompareTag("Quiz")){
            obj.GetComponent<Quiz>().BeginQuiz();
        }
    }

}
