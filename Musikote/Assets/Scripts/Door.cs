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
    
    public new bool Use()
    {
        if (!base.Use()) return false;

        if (isOpen)
            return Close();
        else
            return Open();
    }
    
    public new bool Open()
    {
        if (!base.Use()) return false;

        foreach (DoorTile tileDoor in tilesAffectedByDoor)
            tileDoor.tile.SetupTile(tileDoor.accessesWhileOpen);

        return true;
    }
    
    public new bool Close()
    {
        if (!base.Use()) return false;

        foreach (DoorTile tileDoor in tilesAffectedByDoor)
            tileDoor.tile.SetupTile(tileDoor.accessesWhileClosed);

        return true;
    }

}
