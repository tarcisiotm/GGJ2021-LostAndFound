using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // WIP
    public enum CollectibleType
    {
        Fuel,
        Mineral,
        Sonar,
        Weapon,
        Fuselage, // restores ship integrity?
    }

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
        // if collided with player, grant item
    }
}