using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
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

    private bool isOpen;
    private bool allowMovemment;
    [SerializeField] private List<DoorTile> tilesAffectedByDoor;
    
    [SerializeField] private AnimationCurve movementAnimationCurve;
    private float currentMovementAnimationDuration;
    private Vector3 lastPosition;
    [SerializeField] private float movementAnimationDuration;
    private bool isMovementFinished;

    private void Start()
    {
        lastPosition = visual.position;
        isMovementFinished = true;
        allowMovemment = true;
        foreach (DoorTile tileDoor in tilesAffectedByDoor)
            tileDoor.tile.SetupTile(tileDoor.accessesWhileClosed);
    }

    public override bool Use()
    {
        if (!base.Use()) return false;

        if (isOpen)
            return Close();
        
        return Open();
    }
    
    public override bool Open()
    {
        if (isOpen) return false;
        if (!base.Open()) return false;
        if (!allowMovemment) return false;
        
        foreach (DoorTile tileDoor in tilesAffectedByDoor)
            tileDoor.tile.SetupTile(tileDoor.accessesWhileOpen);
        
        isMovementFinished = false;
        var target = visual.position;
        target.x -= 1;
        
        StartCoroutine(Open(target));
        allowMovemment = false;
        return isOpen = true;
    }
    
    public override bool Close()
    {
        if (!isOpen) return false;
        if (!base.Use()) return false;
        if (!allowMovemment) return false;
        
        foreach (DoorTile tileDoor in tilesAffectedByDoor)
            tileDoor.tile.SetupTile(tileDoor.accessesWhileClosed);

        isMovementFinished = false;
        var target = visual.position;
        target.x += 1;
        StartCoroutine(Open(target));
        allowMovemment = false;
        isOpen = false;
        return true;
    }

    private IEnumerator Open(Vector3 target)
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            currentMovementAnimationDuration += Time.deltaTime;
            visual.position = Vector3.Lerp(lastPosition, target, movementAnimationCurve.Evaluate(
                currentMovementAnimationDuration / movementAnimationDuration));

            if (Vector3.Distance(visual.position, target) < 0.1f)
            {
                currentMovementAnimationDuration = 0f;
                lastPosition = visual.position;
                isMovementFinished = true;
                allowMovemment = true;
                yield break;
            }
        }
    }
}
