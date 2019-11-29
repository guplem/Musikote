using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : Clickable
{
    private bool open;
    private bool close;
    private bool pickUp;
    private bool push;
    private bool shake;
    private bool use;

    private AudioSource _audioSource;
    private AudioClip openClip;
    private AudioClip closeClip;
    private AudioClip pickUpClip;
    private AudioClip pushClip;
    private AudioClip shakeClip;
    private AudioClip useClip;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    public override void IsClicked()
    {
        UIManager.instance.ShowInteractionsFor(this);
    }

    private void Open(bool withOther)
    {
        if (!withOther) return;
        _audioSource.clip = openClip;
        _audioSource.Play();
    }
    
    public void Close()
    {
        if (!close) return;
        _audioSource.clip = closeClip;
        _audioSource.Play();
    }

    public void PickUp()
    {
        if (!pickUp) return;
        Player.instance.AddToInventory(this);
    }

    public void Push()
    {
        if (!push) return;
        _audioSource.clip = openClip;
        _audioSource.Play();
    }

    public void Shake()
    {
        if (!shake) return;
        _audioSource.clip = shakeClip;
        _audioSource.Play();
    }

    public void Use()
    {
        if (!use) return;
        _audioSource.clip = useClip;
        _audioSource.Play();
    }
}
