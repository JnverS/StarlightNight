using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGame : MonoBehaviour
{
    public Button exit;
    LevelControl levelControl;

    // Start is called before the first frame update
    void Start()
    {
        levelControl = FindObjectOfType<LevelControl>();
        exit.onClick.AddListener(levelControl.ExitGame);
    }
}
