using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    public int SelectedChapter { get; set; }

    public override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Main;


    }

    public override void Clear()
    {

    }

    public void SelectChapter(int chapterNum)
    {
        SelectedChapter = chapterNum;

        //_modalWindowManager.ModalWindowIn();

        string chapterName;

        if (SelectedChapter == 0)
            chapterName = "TUTORIAL";
        else
            chapterName = "CHAPTER " + SelectedChapter;
        //_modalWindowManager.description = "ARE YOU SURE YOU WANT TO LOAD " + chapterName + "?\r\nTHIS WILL OVERWRITE YOUR CURRENT SAVE FILE.";
    }

    public void LoadChapter()
    {
        if (SelectedChapter == 1)
            Managers.Scene.LoadScene(Define.Scene.Map_v1);
    }
}
