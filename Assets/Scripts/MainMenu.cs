using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayCurrentLevel()
    {
        LevelControl.CurrentScene = 2;
        MuiscController.instance.PlayGameMusic();
        SceneManager.LoadScene(LevelControl.CurrentScene);
    }

    public void OpenLevelsList()
    {
        LevelControl.CurrentScene = 1;
        MuiscController.instance.PlayMenuMusic();
        SceneManager.LoadScene(LevelControl.CurrentScene);
    }
}
