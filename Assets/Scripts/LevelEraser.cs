using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

public class LevelEraser : MonoBehaviour
{
    public float speed;
    public float XLimitOffset;
    void Start()
    {
        
    }

    void Update()
    {
        Transform playerTransform = GameManager.Instance.player.transform;
        Vector3 limitPosition = playerTransform.position + new Vector3(XLimitOffset, 0, 0);
        if (transform.position.x < limitPosition.x)
        {
            transform.position += new Vector3(Time.deltaTime * speed, 0, 0);
        }
        
        GameManager.Instance.levelErasedMat.SetVector("eraserX", transform.position);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            CharacterController characterController = other.gameObject.GetComponent<CharacterController>();
            Physics.IgnoreLayerCollision(other.gameObject.layer, 0);
            Physics.IgnoreLayerCollision(other.gameObject.layer, 6);
        }
    }
}
