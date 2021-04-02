using UnityEngine;
using UnityEngine.SceneManagement;

public class TileScript : MonoBehaviour
{
    //variables for changing material
    [SerializeField]
    Material[] colours = new Material[4];
    [SerializeField]
    Renderer thisMaterial;

    //variables for game manager, tile spawner, and the next tile in line
    public TileSpawnScript tileSpawnScript;
    public GameManager gameManager;
    public GameObject afterAlive;
    int color = 0;

    private void Start()
    {
        //find gamemanager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //change colour based on position
        switch(transform.position.x)
        {
            case 2.4f:
                thisMaterial.material = colours[0];
                color = 0;
                break;
            case 7.2f:
                thisMaterial.material = colours[1];
                color = 1;
                break;
            case 12:
                thisMaterial.material = colours[2];
                color = 2;
                break;
            case 16.8f:
                thisMaterial.material = colours[3];
                color = 3;
                break;
        }

        //end game if the tile goes too far down
        if (transform.position.y < -1.35)
        {
            GameOver();
        }

        //move down
        transform.Translate(new Vector3(0, -tileSpawnScript.gameSpeed, 0) * Time.deltaTime);

        //if there is no next tile and the tile is onscreen,
        if (afterAlive == null && transform.position.y < 12)
        {
            //check if the currently pressed key matches the row the tile is in, if so, remove the tile and increase the score, if not, game over
            if (tileSpawnScript.currentKey == 0 && tileSpawnScript.lastRowDestroyed != 0)
            {
                if (color == 0)
                {
                    Score();
                    tileSpawnScript.lastRowDestroyed = 0;
                }
                else
                {
                    GameOver();
                }
            }
            if (tileSpawnScript.currentKey == 1 && tileSpawnScript.lastRowDestroyed != 1)
            {
                if (color == 1)
                {
                    Score();
                    tileSpawnScript.lastRowDestroyed = 1;
                }
                else
                {
                    GameOver();
                }
            }
            if (tileSpawnScript.currentKey == 2 && tileSpawnScript.lastRowDestroyed != 2)
            {
                if (color == 2)
                {
                    Score();
                    tileSpawnScript.lastRowDestroyed = 2;
                }
                else
                {
                    GameOver();
                }
            }
            if (tileSpawnScript.currentKey == 3 && tileSpawnScript.lastRowDestroyed != 3)
            {
                if (color == 3)
                {
                    Score();
                    tileSpawnScript.lastRowDestroyed = 3;
                }
                else
                {
                    GameOver();
                }
            }
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene(1);
        //Debug.Log("game over");
    }

    void Score()
    {
        gameManager.rTScore += 1;
        Destroy(gameObject);
    }
}
