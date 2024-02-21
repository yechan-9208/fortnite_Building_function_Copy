using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_LoadChapter : UI_Popup
{
    [SerializeField]
    MainScene _mainScene;

    void OnEnable()
    {
        string chapterName;

        if (_mainScene.SelectedChapter == 0)
            chapterName = "TUTORIAL";
        else
            chapterName = "CHAPTER " + _mainScene.SelectedChapter;
    }
}
