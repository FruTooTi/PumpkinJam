using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType{Hook}
public class IPowerUp : IInteractable
{
    public PowerUpType powerUpType;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void Interact()
    {
        switch (powerUpType)
        {
            case PowerUpType.Hook:
                //TODO
                break;
        }
    }
}
