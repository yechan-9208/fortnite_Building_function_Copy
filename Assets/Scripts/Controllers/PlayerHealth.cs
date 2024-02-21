using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    Animator _playerAnimator;

    PlayerMovement _playerMovement;
    PlayerShooter _playerShooter;
    PlayerController _playerController;

    GameObject _lastDamager;

    void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerShooter = GetComponent<PlayerShooter>();
        _playerController = GetComponent<PlayerController>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // ü�� �����̴� �ʱ�ȭ

        _playerMovement.enabled = true;
        _playerShooter.enabled = true;
    }

    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);

        // ü�� �����̴� ����
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal, GameObject damager)
    {
        if (!Dead)
        {
            Managers.Resource.Instantiate("Particle/Flesh Impact", hitPoint, Quaternion.LookRotation(hitNormal), transform);
            Managers.Sound.Play("Player Damage");
            _lastDamager = damager;
        }

        if (_playerController.State == Define.PlayerState.Failed)
            damage = 1000.0f;

        base.OnDamage(damage, hitPoint, hitNormal, damager);
    }

    // ��� ó��
    public override void Die()
    {
        base.Die();

        // ü�� �����̴� active false

        CinemachineVirtualCamera camera =  _playerController.GameOverCam.GetComponent<CinemachineVirtualCamera>();
        camera.Follow = _lastDamager.transform;
        camera.LookAt = _lastDamager.transform;

        _playerMovement.enabled = false;
        _playerShooter.enabled = false;
        _playerController.State = Define.PlayerState.Die;

        Managers.Sound.Play("Player Die");
        Invoke("DieAnimation", 1.0f);
        Managers.Game.EndGame(false);
    }

    void DieAnimation()
    {
        _playerAnimator.SetTrigger("Die");
    }

    void OnTriggerEnter(Collider other)
    {
        if (!Dead)
        {
            IItem item = other.GetComponent<IItem>();

            if (item != null)
            {
                item.Use(gameObject);
                Managers.Sound.Play("Pick Up", Define.Sound.Effect, 0.5f);
            }
        }
    }
}
