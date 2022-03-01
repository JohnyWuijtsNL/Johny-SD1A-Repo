using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    // Start is called before the first frame update
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("LoadSceneA");

    }
}

