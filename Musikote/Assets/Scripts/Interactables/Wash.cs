using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wash : Interactable
{
    [SerializeField] private Objetive ObjectiveToComplete;
    
    public override bool Use()
    {
        if (!base.Use()) return false;
        GameManager.instance.CompleteObjective(ObjectiveToComplete);
        return true;
    }
}
