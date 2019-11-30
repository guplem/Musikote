using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private List<Objetive> _objetives;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        //Start interactiong
         if (Input.GetMouseButtonDown(0)) {
             RaycastHit hit;
             var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             if (Physics.Raycast(ray, out hit)) {
                 Clickable clickable = hit.transform.gameObject.GetComponent<Clickable>();
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
}
