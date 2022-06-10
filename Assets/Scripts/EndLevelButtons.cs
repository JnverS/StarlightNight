using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelButtons : MonoBehaviour
{
    public Button retry, levels, next;
    LevelControl levelControl;

    // Start is called before the first frame update
    void Start()
    {
        levelControl = FindObjectOfType<LevelControl>();
        retry.onClick.AddListener(levelControl.Retry);
        levels.onClick.AddListener(levelControl.LevelsMenu);
        next.onClick.AddListener(levelControl.Nextlevel);
    }

}
