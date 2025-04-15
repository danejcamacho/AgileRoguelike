using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    // Represents a cell in the dungeon grid
    public class Cell
    {
        public bool visited = false; // Indicates if the cell has been visited
        public bool[] status = new bool[4]; // Status of the cell's walls (up, right, down, left)
    }

    [System.Serializable]
    public class Rule
    {
        public GameObject room; // The room prefab to instantiate
        public Vector2Int minPosition; // Minimum position where the room can spawn
        public Vector2Int maxPosition; // Maximum position where the room can spawn
        public bool isStartRoom; // Indicates if this is the starting room
        public bool obligatory; // Indicates if this room must be spawned

        // Determines the probability of spawning the room at a given position
        public int ProbabilityOfSpawning(int x, int y)
        {
            // 0 - cannot spawn, 1 - can spawn, 2 - HAS to spawn
            if (x >= minPosition.x && x <= maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
            {
                return obligatory ? 2 : 1;
            }
            return 0;
        }
    }

    public Vector2Int size; // Size of the dungeon grid
    public int startPos = 0; // Starting position in the grid for maze generation
    public Vector2Int playerStartPosition; // Starting position for the player (x,y coordinates)
    public Rule[] rooms; // Array of room rules
    public Vector2 offset; // Offset for room positioning

    List<Cell> board; // List representing the dungeon grid

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator(); // Generate the maze when the game starts
    }

    // Generates the dungeon based on the maze
    void GenerateDungeon()
    {
        // First, ensure the player start position cell is visited
        int startIndex = playerStartPosition.x + playerStartPosition.y * size.x;
        board[startIndex].visited = true;
        
        // Make sure the start room is connected to the rest of the maze
        ConnectStartRoom();
        
        // Find the start room rule
        int startRoomIndex = -1;
        for (int i = 0; i < rooms.Length; i++)
        {
            if (rooms[i].isStartRoom)
            {
                startRoomIndex = i;
                break;
            }
        }
        
        // If no room is marked as start room, use first room
        if (startRoomIndex == -1)
        {
            Debug.LogWarning("No room marked as start room! Using first room as start room.");
            startRoomIndex = 0;
        }
        
        // Process all grid cells to create rooms
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[(i + j * size.x)];
                
                // Skip cells that haven't been visited (except for start position which we already ensured is visited)
                if (!currentCell.visited)
                    continue;
                
                int randomRoom = -1;
                
                // Check if this is the player start position
                if (i == playerStartPosition.x && j == playerStartPosition.y)
                {
                    // Always use the start room at this position
                    randomRoom = startRoomIndex;
                }
                else
                {
                    List<int> availableRooms = new List<int>();

                    // Determine which rooms can spawn at the current cell
                    for (int k = 0; k < rooms.Length; k++)
                    {
                        // Skip start rooms for non-start positions
                        if (rooms[k].isStartRoom)
                            continue;
                            
                        int p = rooms[k].ProbabilityOfSpawning(i, j);
                        if (p == 2)
                        {
                            randomRoom = k;
                            break;
                        }
                        else if (p == 1)
                        {
                            availableRooms.Add(k);
                        }
                    }

                    // Select a random room from the available rooms
                    if (randomRoom == -1)
                    {
                        if (availableRooms.Count > 0)
                        {
                            randomRoom = availableRooms[Random.Range(0, availableRooms.Count)];
                        }
                        else
                        {
                            // Find first non-start room to use as default
                            for (int k = 0; k < rooms.Length; k++)
                            {
                                if (!rooms[k].isStartRoom)
                                {
                                    randomRoom = k;
                                    break;
                                }
                            }
                            
                            // If all rooms are start rooms (unlikely), use the first room
                            if (randomRoom == -1)
                            {
                                randomRoom = 0;
                            }
                        }
                    }
                }

                // Instantiate the selected room and update its status
                var newRoom = Instantiate(rooms[randomRoom].room, new Vector3(i * offset.x, -j * offset.y, 0), Quaternion.identity, transform).GetComponent<RoomBehavior>();
                newRoom.UpdateRoom(currentCell.status);
                newRoom.name += " " + i + "-" + j;
            }
        }
    }

    // Make sure the start room connects to the rest of the maze
    void ConnectStartRoom()
    {
        int x = playerStartPosition.x;
        int y = playerStartPosition.y;
        int startIndex = x + y * size.x;
        Cell startCell = board[startIndex];
        
        List<int> possibleConnections = new List<int>();
        
        // Check for visited neighbors to connect to
        // Up
        if (y > 0 && board[x + (y-1) * size.x].visited)
        {
            startCell.status[0] = true;
            board[x + (y-1) * size.x].status[1] = true;
            return; // Connected to an existing path
        }
        else if (y > 0)
        {
            possibleConnections.Add(0);
        }
        
        // Right
        if (x < size.x - 1 && board[(x+1) + y * size.x].visited)
        {
            startCell.status[2] = true;
            board[(x+1) + y * size.x].status[3] = true;
            return; // Connected to an existing path
        }
        else if (x < size.x - 1)
        {
            possibleConnections.Add(2);
        }
        
        // Down
        if (y < size.y - 1 && board[x + (y+1) * size.x].visited)
        {
            startCell.status[1] = true;
            board[x + (y+1) * size.x].status[0] = true;
            return; // Connected to an existing path
        }
        else if (y < size.y - 1)
        {
            possibleConnections.Add(1);
        }
        
        // Left
        if (x > 0 && board[(x-1) + y * size.x].visited)
        {
            startCell.status[3] = true;
            board[(x-1) + y * size.x].status[2] = true;
            return; // Connected to an existing path
        }
        else if (x > 0)
        {
            possibleConnections.Add(3);
        }
        
        // If no visited neighbors found, force a connection with a random direction
        // and mark that cell as visited
        if (possibleConnections.Count > 0)
        {
            int randomDirection = possibleConnections[Random.Range(0, possibleConnections.Count)];
            
            switch (randomDirection)
            {
                case 0: // Up
                    startCell.status[0] = true;
                    board[x + (y-1) * size.x].status[1] = true;
                    board[x + (y-1) * size.x].visited = true;
                    break;
                case 1: // Down
                    startCell.status[1] = true;
                    board[x + (y+1) * size.x].status[0] = true;
                    board[x + (y+1) * size.x].visited = true;
                    break;
                case 2: // Right
                    startCell.status[2] = true;
                    board[(x+1) + y * size.x].status[3] = true;
                    board[(x+1) + y * size.x].visited = true;
                    break;
                case 3: // Left
                    startCell.status[3] = true;
                    board[(x-1) + y * size.x].status[2] = true;
                    board[(x-1) + y * size.x].visited = true;
                    break;
            }
        }
    }

    // Generates the maze using a depth-first search algorithm
    void MazeGenerator()
    {
        board = new List<Cell>();

        // Initialize the board with cells
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;
        Stack<int> path = new Stack<int>();
        int k = 0;

        // Depth-first search algorithm to generate the maze
        while (k < 1000)
        {
            k++;
            board[currentCell].visited = true;

            if (currentCell == board.Count - 1)
            {
                break;
            }

            // Check the cell's neighbors
            List<int> neighbors = CheckNeighbors(currentCell);

            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);
                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                // Update the status of the current cell and the new cell
                if (newCell > currentCell)
                {
                    // Down or right
                    if (newCell - 1 == currentCell)
                    {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    // Up or left
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }
            }
        }
        GenerateDungeon(); // Generate the dungeon after the maze is created
    }

    // Checks the neighbors of a given cell and returns a list of unvisited neighbors
    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        // Check up neighbor
        if (cell - size.x >= 0 && !board[(cell - size.x)].visited)
        {
            neighbors.Add((cell - size.x));
        }

        // Check down neighbor
        if (cell + size.x < board.Count && !board[(cell + size.x)].visited)
        {
            neighbors.Add((cell + size.x));
        }

        // Check right neighbor
        if ((cell + 1) % size.x != 0 && !board[(cell + 1)].visited)
        {
            neighbors.Add((cell + 1));
        }

        // Check left neighbor
        if (cell % size.x != 0 && !board[(cell - 1)].visited)
        {
            neighbors.Add((cell - 1));
        }

        return neighbors;
    }
}