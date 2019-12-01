using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(AudioSource))]
public abstract class Interactable : Clickable
{
    [SerializeField] public bool open;
    [SerializeField] public bool close;
    [FormerlySerializedAs("pickUp")] [SerializeField] public bool pickUpAndDrop;
    [SerializeField] public bool push;
    [SerializeField] public bool pull;
    [SerializeField] public bool shake;
    [SerializeField] public bool use;

    private AudioSource audioSource;
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
        audioSource = GetComponent<AudioSource>();
        isInitialScale = true;
    }

    protected void Update()
    {
        if (Vector3.Distance(Player.instance.transform.position, visual.transform.position) >= 1.5f)
        {
            if (!isInitialScale) return;
            StartCoroutine(ChangeSize(visual, Vector3.zero));
            isInitialScale = false;
        }
        else
        {
            if (isInitialScale) return;
            StartCoroutine(ChangeSize(visual, Vector3.one));
            isInitialScale = true;
        }
    }

    public override void IsClicked()
    {
        if (Vector3.Distance(Player.instance.transform.position, transform.position) <= 1.01f)
        {
            UIManager.instance.ShowInteractionsFor(this);
            Player.instance.Rotate(transform.position);
        }
    }

    public virtual bool Open()
    {
        if (!open) return false;
        audioSource.clip = openClip;
        audioSource.Play();
        return true;
    }
    
    public virtual bool Close()
    {
        if (!close) return false;
        audioSource.clip = closeClip;
        audioSource.Play();
        return true;
    }

    public virtual bool PickUp()
    {
        if (!pickUpAndDrop) return false;
        Player.instance.AddToInventory(this);
        audioSource.clip = pickUpClip;
        audioSource.Play();
        return true;
    }

    public virtual bool Drop()
    {
        if (!pickUpAndDrop) return false;
        Player.instance.RemoveFromInventory(this);
        audioSource.clip = pickUpClip;
        audioSource.Play();
        return true;
    }
    
    public virtual bool Push()
    {
        if (!push) return false;
        audioSource.clip = pushClip;
        audioSource.Play();
        return true;
    }
    
    public virtual bool Pull()
    {
        if (!pull) return false;
        audioSource.clip = pullClip;
        audioSource.Play();
        return true;
    }

    public virtual bool Shake()
    {
        if (!shake) return false;
        audioSource.clip = shakeClip;
        audioSource.Play();
        return true;
    }

    public virtual bool Use()
    {
        if (!use) return false;
        audioSource.clip = useClip;
        audioSource.Play();
        return true;
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
    }
}
