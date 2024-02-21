using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerController _player;
    SphereCollider collider;

    public Vector2 MoveInput { get; private set; }
    public bool RunInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool FireInput { get; private set; }
    public bool ReloadInput { get; private set; }
    public bool InteractionInput { get; private set; }

    public float Attention { get; private set; }
    public float Meditation { get; private set; }

    void Start()
    {
        _player = GetComponent<PlayerController>();
        collider = GameObject.Find("Player").GetComponent<SphereCollider>();

        Managers.Input.KeyAction -= OnKey;
        Managers.Input.KeyAction += OnKey;
        Managers.Input.KeyAction -= OnTab;
        Managers.Input.KeyAction += OnTab;
        Managers.Input.KeyAction -= OnSearch;
        Managers.Input.KeyAction += OnSearch;

    }

    void OnKey()
    {
        InteractionInput = Input.GetKeyDown(KeyCode.F);

        if (Managers.Game.IsGameover || (_player.State != Define.PlayerState.Infil && _player.State != Define.PlayerState.Detec))
        {
            MoveInput = Vector2.zero;
            FireInput = false;
            ReloadInput = false;
            return;
        }

        MoveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (MoveInput.sqrMagnitude > 1) 
            MoveInput = MoveInput.normalized;

        JumpInput = Input.GetKey(KeyCode.Space);
    
        RunInput = Input.GetKey(KeyCode.LeftShift) && MoveInput.y > 0.0f;
        FireInput = Input.GetButton("Fire1");
        ReloadInput = Input.GetButton("Reload");
    }

    void OnTab()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (_player.State == Define.PlayerState.Infil)
                _player.State = Define.PlayerState.Recon;
            else if (_player.State == Define.PlayerState.Recon)
                _player.State = Define.PlayerState.Infil;
        }
    }

    void OnSearch()
    {
        // // Attention
        // if (Input.GetKeyDown(KeyCode.Alpha1))
        // {
        //     if (Attention >= 100)
        //         return;
        //     Attention += 20;
        // }
        // if (Input.GetKeyDown(KeyCode.Alpha2))
        // {
        //     if (Attention <= 0)
        //         return;
        //     Attention -= 20;
        // }
    }

    IEnumerator Delay(){
        yield return new WaitForSeconds(0.1f);
        //Debug.Log("is Delayed");
    }

}
