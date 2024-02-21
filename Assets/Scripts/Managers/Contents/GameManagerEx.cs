using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx
{
    //PlayerController _player;

    string _collectionName;
    int _collectionCnt;
    int _maxCollectionCnt;

    public bool IsGameover { get; private set; } // ���� ���� ����

    public Action<bool> MapIndicator;
    public Action<bool> CoverIndicator;

    public void Init()
    {

        // temp
        _collectionName = "USB";
        _collectionCnt = 0;
        _maxCollectionCnt = 5;
    }

    public void AddCollection()
    {
        _collectionCnt++;

        UI_HUD ui = Managers.UI.SceneUI as UI_HUD;
        if (ui != null)
            ui.UpdateCollectionText(_collectionName, _collectionCnt, _maxCollectionCnt);

        if (_collectionCnt == _maxCollectionCnt)
            EndGame(true);
    }

    public void EndGame(bool success)
    {
        // ���� ���� ���¸� ������ ����
        IsGameover = true;

        if (success)
        {
            Managers.UI.ShowPopupUI<UI_MissionClear>();
        }
        else
        {
            // ���� ���� UI�� Ȱ��ȭ
            Managers.UI.ShowPopupUI<UI_Gameover>();
        }
    }

    public void LoadMainScene()
    {
        IsGameover = false;
        Managers.Scene.LoadScene(Define.Scene.Main);
    }

    public void Clear()
    {
        MapIndicator = null;
        CoverIndicator = null;
        _collectionCnt = 0;
    }
}
