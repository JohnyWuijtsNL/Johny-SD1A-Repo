using UnityEngine;

public class TileSpawnScript : MonoBehaviour
{
    //difficulty setting
    public float gameSpeed;
    [SerializeField]
    float difficulty;

    //save tile prefab, current spawned tile and last spawned tile
    [SerializeField]
    GameObject tilePrefab;
    GameObject currentTile;
    GameObject lastTile;

    //get gamemanager
    public GameManager gameManager;
    [SerializeField]
    TileSpawnScript tileSpawnScript;

    //check which row the player last selected
    int row = 0;
    int oldRow = 0;
    public int lastRowDestroyed = 4;
    public int currentKey = 4;
    
    //position for tile to spawn
    float horizontalPosition;

    private void Start()
    {
        SpawnTile();
    }

    // Update is called once per frame
    void Update()
    {
        //increase game speed
        gameSpeed += Time.deltaTime * difficulty;

        //get key input
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentKey = 0;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentKey = 1;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentKey = 2;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentKey = 3;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SpawnTile();
    }

    void SpawnTile()
    {
        //generate new position for tile to spawn, can't be the same as the previous one
        while (row == oldRow)
        {
            row = Random.Range(0, 4);
        }
        oldRow = row;

        switch (row)
        {
            case 0:
                horizontalPosition = 2.4f;
                break;
            case 1:
                horizontalPosition = 7.2f;
                break;
            case 2:
                horizontalPosition = 12;
                break;
            case 3:
                horizontalPosition = 16.8f;
                break;
        }

        //spawn tile and set a few of its variables
        lastTile = currentTile;
        currentTile = Instantiate(tilePrefab, new Vector3(horizontalPosition, 14.85f, 0), Quaternion.identity);
        currentTile.GetComponent<TileScript>().tileSpawnScript = tileSpawnScript;
        currentTile.GetComponent<TileScript>().afterAlive = lastTile;
        currentTile.GetComponent<TileScript>().gameManager = gameManager;
    }
}
