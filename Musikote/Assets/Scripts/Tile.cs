using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Tile : Clickable
{ 
    private float maxHeightDifference = 0.25f;
    private bool isPlayerCurrentlyNextToTile;

    [SerializeField] private MeshRenderer visuals;

    [SerializeField] private bool connectForward = true;
    [SerializeField] private bool connectBack = true;
    [SerializeField] private bool connectLeft = true;
    [SerializeField] private bool connectRight = true;
    
    //TODO: remove variables after completing 'SetVisuals' method
    [SerializeField] private Material materialPlayerNext;
    [SerializeField] private Material defaultMaterial;

    private void Awake()
    {
        WorldManager.Instance.RegisterTile(this);
    }

    public void SetupTile()
    {
        isPlayerCurrentlyNextToTile = IsPlayerNextToTile();
        SetVisuals(isPlayerCurrentlyNextToTile);
    }

    private void SetVisuals(bool isPlayerNextToTile)
    {
        if (!isPlayerNextToTile)
            visuals.material = defaultMaterial;
        else
            visuals.material = materialPlayerNext;
        
        //TODO: Properly done
    }

    private bool IsPlayerNextToTile()
    {
        if (IsPlayerAtPos(GaetAllPositions()))
                return true;
        
        return false;
    }

    private bool IsPlayerAtPos(List<Vector3> positions)
    {
        foreach (var pos in positions)
        {
            Collider[] hitColliders = Physics.OverlapSphere(pos, maxHeightDifference);
            foreach (Collider collider in hitColliders)
            {
                Player player = collider.GetComponent<Player>();
                if (player != null)
                    return true;
            }
        }
        return false;
    }

    private List<Vector3> GaetAllPositions()
    {
        var positions = new List<Vector3>();
        for (int i = 0; i < 4; i++)
        {
             switch (i)
            {
                    case 0:if (connectForward) positions.Add(transform.position + Vector3.forward); break;
                    case 1:if (connectRight) positions.Add(transform.position + Vector3.right); break;
                    case 2:if (connectBack) positions.Add(transform.position + Vector3.back); break;
                    case 3:if (connectLeft) positions.Add(transform.position + Vector3.left); break;
                    default: throw new Exception("Value not expected. Should be between 0 and 3 (included).");
            }
        }

        return positions;
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (var pos in GaetAllPositions())
        {
            //Gizmos.DrawCube(GaetAllPositions(p), maxHeightDifference*Vector3.one);
            Gizmos.DrawWireSphere(pos, maxHeightDifference);
        }

        /*
        if (IsPlayerNextToTile())
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.6f);
        */
    }

    public override void IsClicked()
    {
        if (!isPlayerCurrentlyNextToTile) return;
        Player.instance.RotateAndMove(transform.position);
    }
}
