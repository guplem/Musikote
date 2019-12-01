using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedgehog : Interactable
{
    [SerializeField] private Objetive ObjectiveToComplete;
    
    public override bool UseWith(Interactable interactableWaiting)
    {
        if (interactableWaiting is HedgehogFood)
        {
            GameManager.instance.RemoveInteractableFromGame(interactableWaiting);
            GameManager.instance.CompleteObjective(ObjectiveToComplete);
        }
        else
        {
            return false;
        }

        return true;
    }
}
