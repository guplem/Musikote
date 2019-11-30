using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] public UIInventoryManager inventory;
    
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button pickUpButton;
    [SerializeField] private Button pullButton;
    [SerializeField] private Button pushButton;
    [SerializeField] private Button shakeButton;
    [SerializeField] private Button useButton;

    
    public static UIManager instance;
    public Interactable currentInteractable
    {
        get { return _currentInteractable; }
        private set
        {
            _currentInteractable = value;
            
            if (!Player.instance.IsITemInInventory(currentInteractable))
                interactions.SetActive(_currentInteractable != null); //TODO: Replace to play animation
            else
                invetoryInteractions.SetActive(_currentInteractable != null); //TODO: Replace to play animation
            
            if (currentInteractable == null) return;
            openButton.interactable = currentInteractable.open;
            closeButton.interactable = currentInteractable.close;
            pickUpButton.interactable = currentInteractable.pickUpAndDrop;
            pullButton.interactable = currentInteractable.pull;
            pushButton.interactable = currentInteractable.push;
            shakeButton.interactable = currentInteractable.shake;
            useButton.interactable = currentInteractable.use;
            
            Debug.Log("Interacting with " + currentInteractable.gameObject.name);
        }
    }
    private Interactable _currentInteractable;

    [SerializeField] private GameObject interactions;
    [SerializeField] private GameObject invetoryInteractions;
    
    private void Awake()
    {
        instance = this;
    }

    public void ShowInteractionsFor(Interactable interactable)
    {
        if (interactable == null)
            Debug.LogWarning("Trying to show interactions of a 'null' interactable.");
        
        if (currentInteractable == interactable) return;
        
        currentInteractable = interactable;
    }

    public void EndInteract()
    {
        currentInteractable = null;
    }
    
    public void OpenCurrentInteractable()
    {
        Player.instance.animator.SetTrigger("Interact");
        currentInteractable.Open();
        UIManager.instance.EndInteract(); 
    }
    
    public void CloseCurrentInteractable()
    {
        Player.instance.animator.SetTrigger("Interact");
        currentInteractable.Close();
        UIManager.instance.EndInteract(); 
    }

    public void PickUpCurrentInteractable()
    {
        Player.instance.animator.SetTrigger("Interact");
        currentInteractable.PickUp();
        UIManager.instance.EndInteract(); 
    }
    
    public void DropCurrentInteractable()
    {
        Player.instance.animator.SetTrigger("Interact");
        currentInteractable.Drop();
        UIManager.instance.EndInteract(); 
    }

    public void PushCurrentInteractable()
    {
        Player.instance.animator.SetTrigger("Interact");
        currentInteractable.Push();
        UIManager.instance.EndInteract(); 
    }
    
    public void PullCurrentInteractable()
    {
        Player.instance.animator.SetTrigger("Interact");
        currentInteractable.Pull();
        UIManager.instance.EndInteract(); 
    }

    public void ShakeCurrentInteractable()
    {
        Player.instance.animator.SetTrigger("Interact");
        currentInteractable.Shake();
        UIManager.instance.EndInteract(); 
    }

    public void UseCurrentInteractable()
    {
        //TODO: check if the currentInteractable is in the inventory to use the "userWith(...)" method instead
        Player.instance.animator.SetTrigger("Interact");
        currentInteractable.Use();
        UIManager.instance.EndInteract(); 
    }
}
