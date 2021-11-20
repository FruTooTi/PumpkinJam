using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.player.transform.position = transform.position;
        GameManager.Instance.player.transform.eulerAngles = transform.eulerAngles;
    }

    void Update()
    {
        
    }
}
