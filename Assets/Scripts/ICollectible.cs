using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICollectible : IInteractable
{
    public int score;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void Interact()
    {
        GameManager.Instance.LevelScore += score;
    }
}
