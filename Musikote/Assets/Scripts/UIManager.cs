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
            Debug.Log("Setting _currentInteractable to '" + _currentInteractable + "'");

            if ( _currentInteractable == null)
            {
                interactions.SetActive(false); //TODO: Replace to play animation
                invetoryInteractions.SetActive(false); //TODO: Replace to play animation
            } 
            else if (!Player.instance.IsITemInInventory(currentInteractable))
                interactions.SetActive(true); //TODO: Replace to play animation
            else
                invetoryInteractions.SetActive(true); //TODO: Replace to play animation
            
            Debug.Log("interactions " + interactions.activeSelf + ", invetoryInteractions " + invetoryInteractions.activeSelf);
            
            if (_currentInteractable == null) return;
            openButton.interactable = _currentInteractable.open;
            closeButton.interactable = _currentInteractable.close;
            pickUpButton.interactable = _currentInteractable.pickUpAndDrop;
            pullButton.interactable = _currentInteractable.pull;
            pushButton.interactable = _currentInteractable.push;
            shakeButton.interactable = _currentInteractable.shake;
            useButton.interactable = _currentInteractable.use;
            
            Debug.Log("Interacting with " + _currentInteractable.gameObject.name);
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
        Debug.Log("Finishing interact with " + currentInteractable);
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
