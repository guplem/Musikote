using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [FormerlySerializedAs("_objectives")] [SerializeField] public List<Objetive> objectives;
    [SerializeField] public LayerMask clickHit;
    [SerializeField] private AudioClip objectiveCompletedClip;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UIManager.instance.objectivesManager.Setup();
    }

    private void Update()
    {
        //Start interactiong
         if (Input.GetMouseButtonDown(0)) {
             RaycastHit hit;
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             if (Physics.Raycast(ray, out hit, 200, clickHit)) {

                 Clickable clickable = hit.transform.gameObject.GetComponent<Clickable>();
                 Interactable interactable = hit.transform.gameObject.GetComponent<Interactable>();

                 
                 if (!interactable && UIManager.instance.interactableWaiting != null) {
                     UIManager.instance.interactableWaiting = null;
                     return;
                 }

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
         
    }

    public void CompleteObjective(Objetive currentObjective)
    {
        AudioController.Instance.PlayClip(objectiveCompletedClip, Player.instance.transform.position);
        objectives.Remove(currentObjective);
        UIManager.instance.objectivesManager.Setup();
        
        //TODO complete game
    }

    public void RemoveInteractableFromGame(Interactable interactable)
    {
        Player.instance.RemoveFromInventory(interactable);
        interactable.gameObject.SetActive(false);
    }
}
