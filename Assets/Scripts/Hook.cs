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
    public Transform initialParent;

    private Transform hookEndTransform;
    private Coroutine stretchCoroutine;
    private bool _isHooking = false;

    private bool IsHooking
    {
        get { return _isHooking; }
        set
        {
            PlayerMovement.Instance.enabled = !value;
            _isHooking = value;
        }
    }
    void Start()
    {
        hookEndTransform = transform.GetChild(0).GetChild(0);
        initialParent = transform.parent;
    }

    void Update()
    {
        
    }

    private void SetParentState(bool state)
    {
        if (state)
        {
            transform.SetParent(initialParent);
            transform.localPosition = new Vector3(0.343f, -0.665f, 0.4912f);
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.SetParent(null);
        }
    }

    public void StretchHook()
    {
        if (!IsHooking)
        {
            stretchCoroutine = StartCoroutine(Stretch());
            IsHooking = true;
        }
    }
    
    private IEnumerator Stretch()
    {
        SetParentState(false);
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

        IsHooking = false;
        
        SetParentState(true);
    }

    public IEnumerator Grapple(Vector3 finalPosition, float upperYBound)
    {
        Transform characterTransform = PlayerTriggers.Instance.transform;
        while ((finalPosition - characterTransform.position).sqrMagnitude > 1.2f)
        {
            PlayerMovement.Instance.controls.Move(-transform.forward * Time.deltaTime * grappleSpeed);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        while (Mathf.Abs(upperYBound - characterTransform.position.y) > .1f)
        {
            PlayerMovement.Instance.controls.Move(transform.up * Time.deltaTime);
        }
        
        SetParentState(true);
        
        StartCoroutine(UnStretch());
    }

    public void OnCollisionEnter(Collision other)
    {
        if (stretchCoroutine != null)
        {
            StopCoroutine(stretchCoroutine);
            if (Mathf.Abs((other.collider.bounds.max - hookEndTransform.position).y) < 0.5f &&
                other.collider.bounds.max.y > PlayerMovement.Instance.transform.position.y + 5f)
            {
                StartCoroutine(Grapple(hookEndTransform.position, other.collider.bounds.max.y));
            }
            else
            {
                StartCoroutine(UnStretch());
            }
        }
    }
}