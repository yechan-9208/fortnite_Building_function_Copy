using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Gameover : UI_Popup
{
    enum Buttons
    {
        GameEndButton,
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));


        GetButton((int)Buttons.GameEndButton).gameObject.BindEvent(GameEnd);


        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void GameEnd(PointerEventData data)
    {
        Managers.Game.LoadMainScene();
    }
}
