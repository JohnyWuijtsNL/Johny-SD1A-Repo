using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //layout of levels
    string[] testLevel = {
        "XXXXXXDXXXXXX",
        "XXX       XXX",
        "XXX    M    X",
        "X      XX T X",
        "X M     X   X",
        "X  T    X   X",
        "X    P  X   X",
        "XXX     X   X",
        "XXXXX   X   X",
        "XXXXXXXXXXXXX" };
    string[] level1 = {
        "XXXXXXDXXXXXX",
        "XP      X   X",
        "X       X   X",
        "X  X      T X",
        "X  X        X",
        "X  X     X  X",
        "X XXXX   XXXX",
        "X  X    TX  X",
        "X  X        X",
        "XXXXXXXXXXXXX" };

    //sprite variables for testing levels
    [SerializeField]
    GameObject floorTestSprite;
    [SerializeField]
    GameObject wallTestSprite;
    [SerializeField]
    GameObject doorTestSprite;
    [SerializeField]
    GameObject playerTestSprite;
    [SerializeField]
    GameObject monsterTestSprite;
    [SerializeField]
    GameObject trapTestSprite;

    //variables for changing the location and size of the grid
    Vector3 offset;
    float tileSize = 0.9f;

    //layers for gameplay
    string[][] layer1;
    string[][] layer2;

    //variable for monster
    [SerializeField]
    GameObject monster;

    //variables for tracking player and monsters position and rotation
    [SerializeField]
    GameObject player;
    GameObject playerSprite;
    int playerY;
    int playerX;
    [SerializeField]
    int playerRotationY;
    [SerializeField]
    int playerRotationX;
    List<GameObject> monsters = new List<GameObject>();
    List<GameObject> monsterSprites = new List<GameObject>();
    List<int> monstersY = new List<int>();
    List<int> monstersX = new List<int>();
    [SerializeField]
    int[] monsterRotationsY;
    [SerializeField]
    int[] monsterRotationsX;

    //variable for how fast sprites move
    float moveSpeed = 0.01f;

    void Start()
    {
        //set offset to make the level appear in the center of the screen
        offset = new Vector3(10.1f - (level1[0].Length * tileSize / 2), -5.9f + (level1.Length * tileSize / 2), 0);

        //convert level layout to layers
        layer1 = ConvertLayer1(level1);
        layer2 = ConvertLayer2(level1);

        //generate tile sprites for level testing
        //GenerateLevelTest();

        //generate player and monsters
        GenerateMovables();
    }

    private void Update()
    {
        //move sprites towards their target positions
        MoveSprites();
        Move();
    }

    //converts walls, floor, doors and traps into a layer
    string[][] ConvertLayer1(string[] level)
    {
        string[][] grid = new string[level.Length][];
        for (int i = 0; i < grid.Length; i++)
        {
            grid[i] = new string[level[i].Length];
        }

        int y = 0;
        foreach (string levelY in level)
        {
            int x = 0;
            foreach (char character in levelY)
            {
                switch (character)
                {
                    case ' ': case 'M':
                    case 'P':
                        grid[y][x] = "floor";
                        break;
                    case 'X':
                        grid[y][x] = "wall";
                        break;
                    case 'D':
                        grid[y][x] = "door";
                        break;
                    case 'T':
                        grid[y][x] = "trap";
                        break;
                    default:
                        grid[y][x] = "error";
                        break;
                }
                x++;
            }
            y++;
        }
        return grid;
    }

    //converts players and monsters into a layer
    string[][] ConvertLayer2(string[] level)
    {
        string[][] grid = new string[level.Length][];
        for (int i = 0; i < grid.Length; i++)
        {
            grid[i] = new string[level[i].Length];
        }

        int y = 0;
        foreach (string levelY in level)
        {
            int x = 0;
            foreach (char character in levelY)
            {
                switch (character)
                {
                    case 'M':
                        grid[y][x] = "monster";
                        break;
                    case 'P':
                        grid[y][x] = "player";
                        break;
                    default:
                        grid[y][x] = "empty";
                        break;
                }
                x++;
            }
            y++;
        }
        return grid;
    }

    //generates tile sprites for level testing
    void GenerateLevelTest()
    {
        for (int y = 0; y < layer1.Length; y++)
        {
            for (int x = 0; x < layer1[y].Length; x++)
            {
                switch(layer1[y][x])
                {
                    case "floor":
                        Instantiate(floorTestSprite, new Vector3(x * tileSize, -y * tileSize) + offset, Quaternion.identity);
                        break;
                    case "wall":
                        Instantiate(wallTestSprite, new Vector3(x * tileSize, -y * tileSize) + offset, Quaternion.identity);
                        break;
                    case "trap":
                        Instantiate(trapTestSprite, new Vector3(x * tileSize, -y * tileSize) + offset, Quaternion.identity);
                        break;
                    case "door":
                        Instantiate(doorTestSprite, new Vector3(x * tileSize, -y * tileSize) + offset, Quaternion.identity);
                        break;
                    case "error":
                        break;
                    default:
                        Instantiate(floorTestSprite, new Vector3(x * tileSize, -y * tileSize) + offset, Quaternion.identity);
                        break;
                }
            }
        }
    }

    //generates player and monsters
    void GenerateMovables()
    {
        for (int y = 0; y < layer2.Length; y++)
        {
            for (int x = 0; x < layer2[y].Length; x++)
            {
                switch (layer2[y][x])
                {
                    case "player":
                        player.transform.position = new Vector3(x * tileSize, -y * tileSize) + offset;
                        playerX = x;
                        playerY = y;
                        if (playerSprite == null)
                        {
                            playerSprite = Instantiate(playerTestSprite, new Vector3(x * tileSize, -y * tileSize) + offset, Quaternion.identity);
                            if (playerRotationX == -1)
                            {
                                playerSprite.transform.eulerAngles = new Vector3(0, 0, 90);
                            }
                            else if (playerRotationX == 1)
                            {
                                playerSprite.transform.eulerAngles = new Vector3(0, 0, -90);
                            }
                            else if (playerRotationY == 1)
                            {
                                playerSprite.transform.eulerAngles = new Vector3(0, 0, 180);
                            }
                        }
                        break;
                    case "monster":
                        monsters.Add(Instantiate(monster, new Vector3(x * tileSize, -y * tileSize) + offset, Quaternion.identity));
                        monsterSprites.Add(Instantiate(monsterTestSprite, new Vector3(x * tileSize, -y * tileSize) + offset, Quaternion.identity));
                        monstersX.Add(x);
                        monstersY.Add(y);
                        if (monsterRotationsX[monsterSprites.Count - 1] == -1)
                        {
                            monsterSprites[monsterSprites.Count - 1].transform.eulerAngles = new Vector3(0, 0, 180);
                        }
                        else if (monsterRotationsY[monsterSprites.Count - 1] == 1)
                        {
                            monsterSprites[monsterSprites.Count - 1].transform.eulerAngles = new Vector3(0, 0, -90);
                        }
                        else if (monsterRotationsY[monsterSprites.Count - 1] == -1)
                        {
                            monsterSprites[monsterSprites.Count - 1].transform.eulerAngles = new Vector3(0, 0, 90);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    //moves sprites to their target locations
    void MoveSprites()
    {
        for (int i = 0; i < monsterSprites.Count; i++)
        {
            monsterSprites[i].transform.position += (monsters[i].transform.position - monsterSprites[i].transform.position) * moveSpeed;
        }
        playerSprite.transform.position += (player.transform.position - playerSprite.transform.position) * moveSpeed;
    }

    void Move()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Rotate(true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Rotate(false);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (layer1[playerY + playerRotationY][playerX + playerRotationX] != "wall")
            { 
                layer2[playerY][playerX] = "empty";
                playerY += playerRotationY;
                playerX += playerRotationX;
                layer2[playerY][playerX] = "player";
                player.transform.position = new Vector3(playerX * tileSize, -playerY * tileSize) + offset;
            }

            for (int i = 0; i < monsters.Count; i++)
            {
                Debug.Log("test");
                if (layer1[monstersY[i] + monsterRotationsY[i]][monstersX[i] + monsterRotationsX[i]] != "wall")
                {
                    layer2[monstersY[i]][monstersX[i]] = "empty";
                    monstersY[i] += monsterRotationsY[i];
                    monstersX[i] += monsterRotationsX[i];
                    layer2[playerY][playerX] = "monster";
                    monsters[i].transform.position = new Vector3(monstersX[i] * tileSize, -monstersY[i] * tileSize) + offset;
                }
            }
        }
    }

    void Rotate(bool isLeft)
    {
        if (isLeft)
        {
            playerSprite.transform.eulerAngles += new Vector3(0, 0, 90);
        }
        else
        {
            playerSprite.transform.eulerAngles += new Vector3(0, 0, -90);
        }

        if (playerRotationX == 1)
        {
            playerRotationX = 0;
            playerRotationY = -2 * Convert.ToInt32(isLeft) + 1;
        }
        else if (playerRotationX == -1)
        {
            playerRotationX = 0;
            playerRotationY = -2 * Convert.ToInt32(!isLeft) + 1;
        }
        else if (playerRotationY == 1)
        {
            playerRotationY = 0;
            playerRotationX = -2 * Convert.ToInt32(!isLeft) + 1;
        }
        else if (playerRotationY == -1)
        {
            playerRotationY = 0;
            playerRotationX = -2 * Convert.ToInt32(isLeft) + 1;
        }

        for (int i = 0; i < monsters.Count; i++)
        {
            if (isLeft)
            {
                monsterSprites[i].transform.eulerAngles += new Vector3(0, 0, 90);
            }
            else
            {
                monsterSprites[i].transform.eulerAngles += new Vector3(0, 0, -90);
            }
            if (monsterRotationsX[i] == 1)
            {
                monsterRotationsX[i] = 0;
                monsterRotationsY[i] = -2 * Convert.ToInt32(isLeft) + 1;
            }
            else if (monsterRotationsX[i] == -1)
            {
                monsterRotationsX[i] = 0;
                monsterRotationsY[i] = -2 * Convert.ToInt32(!isLeft) + 1;
            }
            else if (monsterRotationsY[i] == 1)
            {
                monsterRotationsY[i] = 0;
                monsterRotationsX[i] = -2 * Convert.ToInt32(!isLeft) + 1;
            }
            else if (monsterRotationsY[i] == -1)
            {
                monsterRotationsY[i] = 0;
                monsterRotationsX[i] = -2 * Convert.ToInt32(isLeft) + 1;
            }
        }
    }
}
