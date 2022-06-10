using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{

    private static int currentScene;

    public static int CurrentScene { get => currentScene; 
        set
        {
            currentScene = value;
            Debug.Log(currentScene);
        }
    }

    private void Awake()
    {
        var scene = PlayerPrefs.GetInt("scene");

    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("scene", CurrentScene);
    }


    public void Nextlevel()
    {
        Debug.Log("Nextlevel");
        PlayerPrefs.SetInt("lives", Hero.Instance.Health);
        PlayerPrefs.SetString("retry", "false");
        Debug.Log(Hero.Instance.Health);
        Debug.Log($"Nextlevel {CurrentScene}");
        MuiscController.instance.PlayGameMusic();
        SceneManager.LoadScene(++CurrentScene);
    }
    public void LevelsMenu()
    {
        CurrentScene = 1;
        MuiscController.instance.PlayMenuMusic();
        SceneManager.LoadScene(CurrentScene);
    }
    public void Retry()
    {
        PlayerPrefs.SetString("retry", "true");
        SceneManager.LoadScene(CurrentScene);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
