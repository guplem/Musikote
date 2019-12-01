using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject leftHandObject;
    [SerializeField] private GameObject rightHandObject;

    private void Start()
    {
        UpdateVisuals();
    }

    public void InteractWithLeft()
    {
        Player.instance.items[0].IsClicked();
    }
    
    public void InteractWithRight()
    {
        Player.instance.items[1].IsClicked();
    }

    public void UpdateVisuals()
    {
        leftHandObject.gameObject.SetActive(Player.instance.DoesItemExistAt(0));
        rightHandObject.gameObject.SetActive(Player.instance.DoesItemExistAt(1)); 
    }
}
