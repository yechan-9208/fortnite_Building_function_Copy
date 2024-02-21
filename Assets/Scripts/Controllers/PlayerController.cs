using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Define.PlayerState _state;

    GameObject _followCam;
    GameObject _reconCam;
    GameObject _hideCam;
    public GameObject GameOverCam { get; set; }

    public Define.PlayerState State
    {
        get { return _state; }
        set
        {
            _state = value;

            switch (_state)
            {
                case Define.PlayerState.Infil:
                    _followCam.SetActive(true);
                    _reconCam.SetActive(false);
                    _hideCam.SetActive(false);
                    GameOverCam.SetActive(false);
                    (Managers.UI.SceneUI as UI_HUD).SetActiveCrosshair(true);
                    _sensingRoot.GetComponentInChildren<SpriteRenderer>().enabled = true;
                    if (Managers.Game.MapIndicator != null) Managers.Game.MapIndicator.Invoke(false);
                    break;
                case Define.PlayerState.Recon:
                    _reconCam.SetActive(false);
                   //_reconCam.SetActive(true);
                    _followCam.SetActive(false);
                    _hideCam.SetActive(false);
                    GameOverCam.SetActive(false);
                    (Managers.UI.SceneUI as UI_HUD).SetActiveCrosshair(false);
                    _sensingRoot.GetComponentInChildren<SpriteRenderer>().enabled = true;
                    if (Managers.Game.MapIndicator != null) Managers.Game.MapIndicator.Invoke(true);
                    break;
                case Define.PlayerState.Detec:
                    _followCam.SetActive(true);
                    _reconCam.SetActive(false);
                    _hideCam.SetActive(false);
                    GameOverCam.SetActive(false);
                    (Managers.UI.SceneUI as UI_HUD).SetActiveCrosshair(true);
                    _sensingRoot.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    _beDiscoveredRange = 20.0f;
                    _bgmVolume = 0.0f;
                    Managers.Sound.Play("Game Detection BGM", Define.Sound.Bgm, 0.0f);
                    if (Managers.Game.MapIndicator != null) Managers.Game.CoverIndicator.Invoke(true);
                    break;
                case Define.PlayerState.Hide:
                    _hideCam.SetActive(true);
                    _followCam.SetActive(false);
                    _reconCam.SetActive(false);
                    GameOverCam.SetActive(false);
                    _sensingRoot.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    if (Managers.Game.MapIndicator != null) Managers.Game.CoverIndicator.Invoke(false);
                    break;
                case Define.PlayerState.Failed:
                    Managers.Sound.Play("Game Detection BGM", Define.Sound.Bgm, 0.0f);
                    break;
                case Define.PlayerState.Die:
                    GameOverCam.SetActive(true);
                    _hideCam.SetActive(false);
                    _followCam.SetActive(false);
                    _reconCam.SetActive(false);
                    break;
            }
        }
    }

    [SerializeField][Range(1f, 10f)]
    float _beDiscoveredRange;

    [SerializeField][Range(0f, 10f)]
    float _stability;

    [SerializeField]
    Transform _sensingRoot;
    [SerializeField]
    Gun _gun;

    PlayerInput _playerInput;
    Animator _animator;
    SphereCollider _collider;

    bool _isCoverAround;
    Transform _cover;
    bool _success;
    float _bgmVolume = 1.0f;

    //private float averageValue = 0f; // ��� ��
    private float targetValue = 0f; // ������ �ݰ� ��

    private float maxRange = 2.5f; // Ž������ �ִ� �ݰ�
    private float minRange = 0.8f; // Ž������ �ּ� �ݰ�

    void Start()
    {
        _collider = GetComponent<SphereCollider>();
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();

        _followCam = GameObject.Find("Follow Cam");
        _reconCam = GameObject.Find("Recon Cam");
        _hideCam = GameObject.Find("Hide Cam");
        GameOverCam = GameObject.Find("GameOver Cam");

        //State = Define.PlayerState.Infil;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (_isCoverAround && _playerInput.InteractionInput && State == Define.PlayerState.Detec)
            StartCoroutine("Hide");
        if (State == Define.PlayerState.Hide && _success && _playerInput.InteractionInput)
        {
            _animator.SetBool("Hide", false);
            State = Define.PlayerState.Infil;
            _success = false;
            _bgmVolume = 1.0f;
            Managers.Sound.Play("Game Default BGM", Define.Sound.Bgm, 1.0f);
        }

        if (State == Define.PlayerState.Hide)
        {
            _bgmVolume = Mathf.Max(0.0f, _bgmVolume - 0.2f * Time.deltaTime) ;
            Managers.Sound.SetVolume(Define.Sound.Bgm, _bgmVolume);
        }
        if (State == Define.PlayerState.Detec || State == Define.PlayerState.Failed)
        {
            _bgmVolume = Mathf.Min(0.4f, _bgmVolume + 0.4f * Time.deltaTime);
            Managers.Sound.SetVolume(Define.Sound.Bgm, _bgmVolume);
        }

        // _beDiscoveredRange
        targetValue = (float)(maxRange + (minRange - maxRange) * (_playerInput.Meditation / 100f));
        _beDiscoveredRange = Mathf.Lerp(_beDiscoveredRange, targetValue, Time.deltaTime);

        // _stability
        _stability = _playerInput.Attention / 10f;

        _sensingRoot.localScale = new Vector3(_beDiscoveredRange, 1f, _beDiscoveredRange);
        _collider.radius = _beDiscoveredRange;
        _gun.Stability = _stability;
    }

    IEnumerator Hide()
    {
        transform.position = _cover.position;
        transform.rotation = _cover.rotation;

        _animator.SetBool("Hide", true);
        State = Define.PlayerState.Hide;
        Managers.Sound.Play("Player Breath");

        UI_MeditationBar ui = Managers.UI.ShowPopupUI<UI_MeditationBar>();
        yield return new WaitForSeconds(10f);
        if (ui.Success)
            _success = true;
        else
            State = Define.PlayerState.Failed; // ����
        ui.ClosePopupUI();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cover")
        {
            _isCoverAround = true;
            _cover = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Cover")
            _isCoverAround = false;
    }           
}
