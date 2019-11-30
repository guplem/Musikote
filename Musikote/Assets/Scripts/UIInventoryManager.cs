using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject leftHandObject;
    [SerializeField] private GameObject rightHandObject;

    public void InteractWithLeft()
    {
        Player.instance.items[0].IsClicked();
    }
    
    public void InteractWithRight()
    {
        Player.instance.items[1].IsClicked();
    }
}
