using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class AllowedAccesses
{
    [FormerlySerializedAs("allowAccessFromForward")] [SerializeField] public bool forward = true;
    [FormerlySerializedAs("allowAccessFromBack")] [SerializeField] public bool back = true;
    [FormerlySerializedAs("allowAccessFromLeft")] [SerializeField] public bool left = true;
    [FormerlySerializedAs("allowAccessFromRight")] [SerializeField] public bool right = true;
}

[SelectionBase]
public class Tile : Clickable
{ 
    private float maxHeightDifference = 0.25f;

    public bool isPlayerCurrentlyNextToTile
    {
        get { return _isPlayerCurrentlyNextToTile;}
        set
        {
            _isPlayerCurrentlyNextToTile = value;
            SetVisuals(_isPlayerCurrentlyNextToTile);
        }
    }
    private bool _isPlayerCurrentlyNextToTile;

    [SerializeField] private AllowedAccesses acessesAllowed;
    
    [SerializeField] private GameObject avaliabilityMarker;
    
    private void Awake()
    {
        WorldManager.Instance.RegisterTile(this);
    }

    public void SetupTile()
    {
        isPlayerCurrentlyNextToTile = IsPlayerNextToTile() && UIManager.instance.interactableWaiting == null;
    }
    
    public void SetupTile(AllowedAccesses allowedAccesses)
    {
        SetAllowedAccesses(allowedAccesses);
        SetupTile();
    }

    private void SetAllowedAccesses(AllowedAccesses allowedAccesses)
    {
        this.acessesAllowed.forward = allowedAccesses.forward;
        this.acessesAllowed.right = allowedAccesses.right;
        this.acessesAllowed.back = allowedAccesses.back;
        this.acessesAllowed.left = allowedAccesses.left;
    }

    public void DeactivateTileAvaliability()
    {
        isPlayerCurrentlyNextToTile = false;
    }

    private void SetVisuals(bool isPlayerNextToTile)
    {
        avaliabilityMarker.SetActive(isPlayerNextToTile);
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
                    case 0:if (this.acessesAllowed.forward) positions.Add(transform.position + Vector3.forward); break;
                    case 1:if (this.acessesAllowed.right) positions.Add(transform.position + Vector3.right); break;
                    case 2:if (this.acessesAllowed.back) positions.Add(transform.position + Vector3.back); break;
                    case 3:if (this.acessesAllowed.left) positions.Add(transform.position + Vector3.left); break;
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
        WorldManager.Instance.DeactivateAvaliabilityInAllTiles();
        Player.instance.RotateAndMove(transform.position);
    }
}
