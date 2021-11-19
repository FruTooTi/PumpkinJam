using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public float range;
    public float currentStretchAmount;
    public float hookSpeed;
    public float grappleSpeed;

    private Transform hookEndTransform;
    private Coroutine stretchCoroutine;
    void Start()
    {
        hookEndTransform = transform.GetChild(0).GetChild(0);
    }

    void Update()
    {
        
    }

    public void StretchHook()
    {
        stretchCoroutine = StartCoroutine(Stretch());
    }
    
    private IEnumerator Stretch()
    {
        while (currentStretchAmount < range)
        {
            float stretchAmount = Time.deltaTime * hookSpeed;
            Vector3 tempScale = transform.localScale;
            tempScale.z += stretchAmount;
            transform.localScale = tempScale;
            currentStretchAmount += stretchAmount;
            
            yield return new WaitForSeconds(Time.deltaTime);
        }

        stretchCoroutine = null;
        StartCoroutine(UnStretch());
    }
    
    public IEnumerator UnStretch()
    {
        while (currentStretchAmount > 0)
        {
            float unStretchAmount = -Time.deltaTime * hookSpeed;
            Vector3 tempScale = transform.localScale;
            tempScale.z += unStretchAmount;
            transform.localScale = tempScale;
            currentStretchAmount += unStretchAmount;
            
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public IEnumerator Grapple(Vector3 finalPosition)
    {
        Transform characterTransform = PlayerTriggers.Instance.transform;
        while ((finalPosition - characterTransform.position).sqrMagnitude < 800f)
        {
            PlayerMovement.Instance.controls.Move(-transform.forward * Time.deltaTime * grappleSpeed);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (stretchCoroutine != null)
        {
            StopCoroutine(stretchCoroutine);
            if (Mathf.Abs((other.collider.bounds.max - hookEndTransform.position).y) < 0.5f &&
                other.collider.bounds.max.y > PlayerMovement.Instance.transform.position.y + 5f)
            {
                StartCoroutine(Grapple(hookEndTransform.position));
            }
            else
            {
                StartCoroutine(UnStretch());
            }
        }
    }
}
