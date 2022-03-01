using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("LoadSceneA");

    }


    public void Information()
    {
        SceneManager.LoadScene("Information");
        Debug.Log("LoadSceneB");

    }

}

