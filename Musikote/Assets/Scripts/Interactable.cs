using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Interactable : Clickable
{
    [SerializeField] public bool open;
    [SerializeField] public bool close;
    [FormerlySerializedAs("pickUp")] [SerializeField] public bool pickUpAndDrop;
    [SerializeField] public bool push;
    [SerializeField] public bool pull;
    [SerializeField] public bool shake;
    [SerializeField] public bool use;
    [SerializeField] public bool canBeUsedAlone;
    
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;
    [SerializeField] private AudioClip pickUpClip;
    [SerializeField] private AudioClip dropClip;
    [SerializeField] private AudioClip pushClip;
    [SerializeField] private AudioClip pullClip;
    [SerializeField] private AudioClip shakeClip;
    [SerializeField] private AudioClip useClip;

    [SerializeField] protected Transform visual;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float scaleAnimationCurveDuration;
    private float currentScaleAnimationCurveDuration;

    private bool isInitialScale;
    
    private void Awake()
    {
        isInitialScale = true;
        visual.localScale = Vector3.zero;
    }

    protected void Update()
    {
        if (Vector3.Distance(Player.instance.transform.position, visual.transform.position) >= 1.5f)
        {
            if (isInitialScale) return;
            StartCoroutine(ChangeSize(visual, Vector3.zero));
            isInitialScale = true;
        }
        else
        {
            if (!isInitialScale) return;
            StartCoroutine(ChangeSize(visual, Vector3.one));
            isInitialScale = false;
        }
    }

    public override void IsClicked()
    {
        if (!Player.instance.IsITemInInventory(this))
        {
            if (Vector3.Distance(Player.instance.transform.position, transform.position) >= 1.5f)
            {
                Debug.Log("The distance to interact is too high: " + Vector3.Distance(Player.instance.transform.position, transform.position));
                return;
            }
            
            Player.instance.Rotate(transform.position);
        }
        
        UIManager.instance.ShowInteractionsFor(this);
    }

    public virtual bool Open()
    {
        if (!open) return false;
        AudioController.Instance.PlayClip(openClip, transform.position);
        return true;
    }
    
    public virtual bool Close()
    {
        if (!close) return false;
        AudioController.Instance.PlayClip(closeClip, transform.position);
        return true;
    }

    public virtual bool PickUp()
    {
        if (!pickUpAndDrop) return false;
        Player.instance.AddToInventory(this);
        AudioController.Instance.PlayClip(pickUpClip, transform.position);
        return true;
    }

    public virtual bool Drop()
    {
        if (!pickUpAndDrop) return false;
        Player.instance.RemoveFromInventory(this);
        AudioController.Instance.PlayClip(dropClip, transform.position);
        return true;
    }
    
    public virtual bool Push()
    {
        if (!push) return false;
        AudioController.Instance.PlayClip(pushClip, transform.position);
        return true;
    }
    
    public virtual bool Pull()
    {
        if (!pull) return false;
        AudioController.Instance.PlayClip(pullClip, transform.position);
        return true;
    }

    public virtual bool Shake()
    {
        if (!shake) return false;
        AudioController.Instance.PlayClip(shakeClip, transform.position);
        return true;
    }

    public virtual bool Use()
    {
        if (!use) return false;

        if (!canBeUsedAlone)
        {
            UIManager.instance.LookForSecondObjectToUse();
            return true;
        }

        AudioController.Instance.PlayClip(useClip, transform.position);
        return true;
    }


    public virtual bool UseWith(Interactable interactableWaiting)
    {
        return false;
    }

    private IEnumerator ChangeSize(Transform trans, Vector3 targetScale)
    {
        Vector3 originalScale = trans.localScale;
        while (Vector3.Distance(trans.localScale, targetScale) > 0.1f)
        {
            yield return new WaitForEndOfFrame();
            currentScaleAnimationCurveDuration += Time.deltaTime;
            trans.localScale = Vector3.Lerp(originalScale, targetScale, animationCurve.Evaluate(
                currentScaleAnimationCurveDuration / scaleAnimationCurveDuration));
        }
        currentScaleAnimationCurveDuration = 0f;
        trans.localScale = targetScale;
    }

}
