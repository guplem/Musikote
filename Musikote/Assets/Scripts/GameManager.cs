using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private List<Objetive> _objectives;
    [SerializeField] public LayerMask clickHit;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        //Start interactiong
         if (Input.GetMouseButtonDown(0)) {
             Debug.Log("Click");
             RaycastHit hit;
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             if (Physics.Raycast(ray, out hit, 200, clickHit)) {
                 Clickable clickable = hit.transform.gameObject.GetComponent<Clickable>();
                 if (clickable != null)
                     Debug.Log("Clicked clickable " + clickable.gameObject.name + ". Current interactable is " + UIManager.instance.currentInteractable);
                 if (clickable != null && UIManager.instance.currentInteractable == null) 
                     clickable.IsClicked();
             }
         }
         
         //Finish interacting - Close UI
         if (Input.GetKeyDown(KeyCode.Escape))
         {
             if (UIManager.instance.currentInteractable != null)
             {
                 UIManager.instance.EndInteract();        
             }
             else
             {
                 //TODO: Show/Close pause menu or exit game
             }
         }

         if (_objectives.All(objective => objective.isDone))
         {
             //Game Completed
         }
    }

    public void CompleteObjective(Objetive currentObjective)
    {
        foreach (var objective in _objectives)
        {
            if (objective.Equals(currentObjective))
                objective.isDone = true;
        }
    }
}
