using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

public enum PowerUpType{Dash}
public class IPowerUp : IInteractable
{
    public PowerUpType powerUpType;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * 100, 0),Space.World);
    }

    public override void Interact()
    {
        switch (powerUpType)
        {
            case PowerUpType.Dash:
                PlayerMovement.Instance.Dash();
                Destroy(gameObject);
                break;
        }
    }
}
