using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int rTScore;
    [SerializeField]
    TextMeshProUGUI rTScoreText;
    public int rTHScore;
    [SerializeField]
    TextMeshProUGUI rTHScoreText;
    TileSpawnScript tileSpawner;
    [SerializeField]
    GameManager gameManager;
    bool isPlaying = true;



    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    
    // Update is called once per frame
    void Update()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;
        if (sceneName == "Menu")
        {
            if (isPlaying)
            {
                rTScoreText = GameObject.Find("RTScore").GetComponent<TextMeshProUGUI>();
                rTScoreText.text = "Score: " + rTScore;
                rTHScoreText = GameObject.Find("RTHScore").GetComponent<TextMeshProUGUI>();
                rTHScoreText.text = "High Score: " + rTHScore;
                isPlaying = false;
            }

            if (rTScore > rTHScore)
            {
                rTHScore = rTScore;
            }
            rTScore = 0;
        }
        else if (sceneName == "ReactionTiles")
        {
            isPlaying = true;
            tileSpawner = GameObject.Find("TileSpawner").GetComponent<TileSpawnScript>();
            tileSpawner.gameManager = gameManager;
        }
        else if (sceneName == "StartScene")
        {
            SceneManager.LoadScene(1);
        }
    }
}
