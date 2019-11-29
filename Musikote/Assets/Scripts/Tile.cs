using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

[SelectionBase]
public class Tile : MonoBehaviour
{
    [SerializeField]private float maxHeightDifference = 0.25f;
    
    public void SetupTile()
    {
        //SetVisuals(IsPlayerNextToTile());
    }
    
    private bool IsPlayerNextToTile()
    {
        for (int p = 0; p < 4; p++)
            //if (IsPlayerAtPos(GetPositionByIndex(p)))
                return true;
        
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
    }
}
