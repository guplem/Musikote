using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : Clickable
{
    [SerializeField] private bool open;
    [SerializeField] private bool close;
    [SerializeField] private bool pickUp;
    [SerializeField] private bool push;
    [SerializeField] private bool shake;
    [SerializeField] private bool use;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;
    [SerializeField] private AudioClip pickUpClip;
    [SerializeField] private AudioClip pushClip;
    [SerializeField] private AudioClip shakeClip;
    [SerializeField] private AudioClip useClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public override void IsClicked()
    {
        UIManager.instance.ShowInteractionsFor(this);
    }

    public bool Open()
    {
        if (!open) return false;
        audioSource.clip = openClip;
        audioSource.Play();
        return true;
    }
    
    public bool Close()
    {
        if (!close) return false;
        audioSource.clip = closeClip;
        audioSource.Play();
        return true;
    }

    public bool PickUp()
    {
        if (!pickUp) return false;
        Player.instance.AddToInventory(this);
        audioSource.clip = pickUpClip;
        audioSource.Play();
        return true;
    }

    public bool Push()
    {
        if (!push) return false;
        audioSource.clip = pushClip;
        audioSource.Play();
        return true;
    }

    public bool Shake()
    {
        if (!shake) return false;
        audioSource.clip = shakeClip;
        audioSource.Play();
        return true;
    }

    public bool Use()
    {
        if (!use) return false;
        audioSource.clip = useClip;
        audioSource.Play();
        return true;
    }
}
