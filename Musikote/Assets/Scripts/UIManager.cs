using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Interactable currentInteractable
    {
        get { return _currentInteractable; }
        private set
        {
            _currentInteractable = value;
            interactions.SetActive(_currentInteractable != null); //TODO: Replace to play animation
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
    
    private void OpenCurrentInteractable()
    {
        currentInteractable.Open();
    }
    
    public void CloseCurrentInteractable()
    {
        currentInteractable.Close();
    }

    public void PickUpCurrentInteractable()
    {
        currentInteractable.PickUp();
    }

    public void PushCurrentInteractable()
    {
        currentInteractable.Push();
    }

    public void ShakeCurrentInteractable()
    {
        currentInteractable.Shake();
    }

    public void UseCurrentInteractable()
    {
        //TODO: check if the currentInteractable is in the inventory to use the "userWith(...)" method instead
        currentInteractable.Use();
    }
}
