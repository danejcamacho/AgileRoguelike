using UnityEngine;

public class RoomBehavior : MonoBehaviour
{
    public GameObject[] walls; // 0 = up, 1 = down, 2 = right, 3 = left
    public GameObject[] doors; // 0 = up, 1 = down, 2 = right, 3 = left
    public GameObject lockedDoors;

    public GameObject[] enemies;
    bool playerInRoom = false;
    bool enemiesInRoom = false;
    bool noEnemyFlag = true;
    bool[] status = new bool[4];

    private void Awake() {
        foreach(GameObject enemy in enemies){
           enemy.GetComponent<Enemy>().enabled = false;
        }
    }

    private void Update() {
        foreach(GameObject enemy in enemies){
            noEnemyFlag = true;
            if (enemy != null){
                noEnemyFlag = false;
                enemiesInRoom = true;
                break;
            }
        }
        if (noEnemyFlag){
            enemiesInRoom = false;
        }

        if (playerInRoom && enemiesInRoom) {
            lockedDoors.SetActive(true);

        } else {
            lockedDoors.SetActive(false);
        }
    }

    public void UpdateRoom(bool[] status){
        this.status = status;
        for (int i =0; i < status.Length; i++){
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRoom = true;
            foreach(GameObject enemy in enemies){
                if (enemy != null){
                    enemy.GetComponent<Enemy>().enabled = true;
                }

            }
        }
    }
}
