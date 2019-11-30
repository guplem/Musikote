using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
internal class DoorTile
{
    [SerializeField] public Tile tile;
    [Header("At open state")] public AllowedAccesses accessesWhileOpen;
    [Header("At open state")] public AllowedAccesses accessesWhileClosed;
}

public class Door : Interactable
{

    private bool isOpen = false;
    [SerializeField] private List<DoorTile> tilesAffectedByDoor;
    [SerializeField] private GameObject visuals;

    private void Start()
    {
        Close();
    }

    public override bool Use()
    {
        if (!base.Use()) return false;

        if (isOpen)
            return Close();
        else
            return Open();
    }
    
    public override bool Open()
    {
       
        if (!base.Open()) return false;

        foreach (DoorTile tileDoor in tilesAffectedByDoor)
            tileDoor.tile.SetupTile(tileDoor.accessesWhileOpen);
        
        visuals.SetActive(false); //TODO: Animations

        return true;
    }
    
    public override bool Close()
    {
        if (!base.Use()) return false;
        
        foreach (DoorTile tileDoor in tilesAffectedByDoor)
            tileDoor.tile.SetupTile(tileDoor.accessesWhileClosed);

        visuals.SetActive(true); //TODO: Animations

        return true;
    }

}
