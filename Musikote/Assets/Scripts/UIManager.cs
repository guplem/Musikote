using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
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
            interactions.SetActive(_currentInteractable != null); //TODO: Replace to play animation
            
            if (currentInteractable == null) return;
            openButton.interactable = currentInteractable.open;
            closeButton.interactable = currentInteractable.close;
            pickUpButton.interactable = currentInteractable.pickUp;
            pullButton.interactable = currentInteractable.pull;
            pushButton.interactable = currentInteractable.push;
            shakeButton.interactable = currentInteractable.shake;
            useButton.interactable = currentInteractable.use;
            
            Debug.LogError("Interacting with " + currentInteractable.gameObject.name);
        }
    }
    private Interactable _currentInteractable;

    [SerializeField] private GameObject interactions;
    
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
    }
    
    public void CloseCurrentInteractable()
    {
        Player.instance.animator.SetTrigger("Interact");
        currentInteractable.Close();
    }

    public void PickUpCurrentInteractable()
    {
        Player.instance.animator.SetTrigger("Interact");
        currentInteractable.PickUp();
    }

    public void PushCurrentInteractable()
    {
        Player.instance.animator.SetTrigger("Interact");
        currentInteractable.Push();
    }
    
    public void PullCurrentInteractable()
    {
        Player.instance.animator.SetTrigger("Interact");
        currentInteractable.Pull();
    }

    public void ShakeCurrentInteractable()
    {
        Player.instance.animator.SetTrigger("Interact");
        currentInteractable.Shake();
    }

    public void UseCurrentInteractable()
    {
        //TODO: check if the currentInteractable is in the inventory to use the "userWith(...)" method instead
        Player.instance.animator.SetTrigger("Interact");
        currentInteractable.Use();
    }
}
