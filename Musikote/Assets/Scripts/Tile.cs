using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Tile : MonoBehaviour
{ 
    private float maxHeightDifference = 0.25f;
    private bool isPlayerCurrentlyNextToTile;

    [SerializeField] private MeshRenderer visuals;
    
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
        for (int p = 0; p < 4; p++)
            if (IsPlayerAtPos(GetPositionByIndex(p)))
                return true;
        
        return false;
    }

    private bool IsPlayerAtPos(Vector3 pos)
    {
        Collider[] hitColliders = Physics.OverlapSphere(pos, maxHeightDifference);
        int i = 0;
        foreach (Collider collider in hitColliders)
        {
            Player player = collider.GetComponent<Player>();
            if (player != null)
                return true;
        }

        return false;
    }

    private Vector3 GetPositionByIndex(int i)
    {
        switch (i)
        {
            case 0: return transform.position + Vector3.forward; 
            case 1: return transform.position + Vector3.right; 
            case 2: return transform.position + Vector3.back; 
            case 3: return transform.position + Vector3.left; 
            default: throw new Exception("Value not expected. Should be between 0 and 3 (included).");
        }
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for (int p = 0; p < 4; p++)
        {
            //Gizmos.DrawCube(GetPositionByIndex(p), maxHeightDifference*Vector3.one);
            Gizmos.DrawWireSphere(GetPositionByIndex(p), maxHeightDifference);
        }
        
        /*
        if (IsPlayerNextToTile())
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.6f);
        */
    }
}
