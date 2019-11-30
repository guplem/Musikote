using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    private List<Interactable> items = new List<Interactable>();

    private void Awake()
    {
        instance = this;
    }

    public void MoveTo()
    {
        //TODO: MoveTo
    }

    public void LookAt()
    {
        //TODO: LookAt
    }

    public void LookAndRotate()
    {
        MoveTo();
        LookAt();
    }

    public void AddToInventory(Interactable item)
    {
        if (items.Count >= 2) Debug.LogError("Inventory completed");
        else items.Add(item);
    }
}
