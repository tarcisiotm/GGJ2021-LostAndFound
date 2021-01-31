using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // WIP
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
    
    uint _healthAmount = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TODO
    private void OnTriggerEnter(Collider other)
    {
        
        var player = other.GetComponentInParent<PlayerController>();
        if (player == null) return;
         
        if (_collectibleType == CollectibleType.Health)
        {
           Health health = player.GetComponentInChildren<Health>();
           health.AddHealth(_healthAmount);   
        }
            
        // if collided with player, grant item
    }
}