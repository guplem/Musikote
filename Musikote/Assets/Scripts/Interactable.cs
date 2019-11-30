using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Interactable : Clickable
{
    [SerializeField] public bool open;
    [SerializeField] public bool close;
    [SerializeField] public bool pickUp;
    [SerializeField] public bool push;
    [SerializeField] public bool pull;
    [SerializeField] public bool shake;
    [SerializeField] public bool use;

    private AudioSource audioSource;
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;
    [SerializeField] private AudioClip pickUpClip;
    [SerializeField] private AudioClip pushClip;
    [SerializeField] private AudioClip pullClip;
    [SerializeField] private AudioClip shakeClip;
    [SerializeField] private AudioClip useClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public override void IsClicked()
    {
        if (Vector3.Distance(Player.instance.transform.position, transform.position) <= 1)
            UIManager.instance.ShowInteractionsFor(this);
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
        if (!pickUp) return false;
        Player.instance.AddToInventory(this);
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
}
