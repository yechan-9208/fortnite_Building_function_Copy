using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        UI_HUD ui = Managers.UI.ShowSceneUI<UI_HUD>();

        Managers.Sound.Play("Game Detection BGM", Define.Sound.Bgm);
        Managers.Sound.Play("Game Default BGM", Define.Sound.Bgm);
    }

    public override void Clear()
    {

    }
}
