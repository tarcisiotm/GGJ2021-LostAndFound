using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType
    {
        Fuel,
        Health,
        Mineral,
        Sonar,
        Weapon,
        Fuselage, // restores ship integrity?
    }

    public CollectibleType _collectibleType;

    public UnityEvent OnCollected;

    [Header("SFX")]
    [SerializeField] private AudioClip _onCollectibleAudio;
    [SerializeField] private float _onCollectibleAudioVolume;

    uint _healthAmount = 1;
    
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<PlayerController>();
        if (player == null) return;

        HandleCollectible(player);
    }

    private void HandleCollectible(PlayerController player)
    {
        switch (_collectibleType)
        {
            case CollectibleType.Health: HandleHealth(player); break;
            case CollectibleType.Weapon: HandleWeapon(player); break;
            case CollectibleType.Mineral: player.HandleMineral(); break;
        }

        OnCollected?.Invoke();

        //play collect sound
        if (_onCollectibleAudio != null) AudioManager.I.CreateOneShot(_onCollectibleAudio, transform.position, _onCollectibleAudioVolume);
        Destroy(gameObject);
    }

    private void HandleHealth(PlayerController player)
    {
        Health health = player.GetComponentInChildren<Health>();
        health.AddHealth(_healthAmount);
    }

    private void HandleWeapon(PlayerController player)
    {
        player.SetCanShoot(true);
    }
}