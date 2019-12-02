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
    
    [SerializeField] private Button openButtonInventory;
    [SerializeField] private Button closeButtonInventory;
    [SerializeField] private Button dropButtonInventory;
    [SerializeField] private Button pullButtonInventory;
    [SerializeField] private Button pushButtonInventory;
    [SerializeField] private Button shakeButtonInventory;
    [SerializeField] private Button useButtonInventory;

    [SerializeField] public UIObjectiveManager objectivesManager;
    
    public static UIManager instance;
    public Interactable currentInteractable
    {
        get { return _currentInteractable; }
        private set
        {
            _currentInteractable = value;

            if ( _currentInteractable == null)
            {
                interactions.SetActive(false); //TODO: Replace to play animation
                invetoryInteractions.SetActive(false); //TODO: Replace to play animation
            } 
            else if (!Player.instance.IsITemInInventory(currentInteractable))
                interactions.SetActive(true); //TODO: Replace to play animation
            else
                invetoryInteractions.SetActive(true); //TODO: Replace to play animation

            if (_currentInteractable == null) return;

            if (!Player.instance.IsITemInInventory(currentInteractable))
            {
                openButton.interactable = _currentInteractable.open;
                closeButton.interactable = _currentInteractable.close;
                pickUpButton.interactable = _currentInteractable.pickUpAndDrop;
                pullButton.interactable = _currentInteractable.pull;
                pushButton.interactable = _currentInteractable.push;
                shakeButton.interactable = _currentInteractable.shake;
                useButton.interactable = _currentInteractable.use;
            } else {
                openButtonInventory.interactable = _currentInteractable.open;
                closeButtonInventory.interactable = _currentInteractable.close;
                dropButtonInventory.interactable = _currentInteractable.pickUpAndDrop;
                pullButtonInventory.interactable = _currentInteractable.pull;
                pushButtonInventory.interactable = _currentInteractable.push;
                shakeButtonInventory.interactable = _currentInteractable.shake;
                useButtonInventory.interactable = _currentInteractable.use;
            }

            Debug.Log("Interacting with " + _currentInteractable.gameObject.name);
        }
    }
    private Interactable _currentInteractable;

    [SerializeField] private GameObject interactions;
    [SerializeField] private GameObject invetoryInteractions;
    
    private void Awake()
    {
        instance = this;
        interactableWaiting = null;
    }

    public void ShowInteractionsFor(Interactable interactable)
    {
        if (interactable == null)
            Debug.LogWarning("Trying to show interactions of a 'null' interactable.");
        
        if (currentInteractable == interactable) return;

        if (interactableWaiting == null)
            currentInteractable = interactable;
        else
        {
            if (!interactable.UseWith(interactableWaiting))
                Debug.Log("Interaction not permited between " + interactableWaiting + " and " + interactable);
        }

        interactableWaiting = null;
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
        if (currentInteractable == null)
            Debug.LogWarning("Trying to interact with a null interactable.");
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


    [SerializeField] private Texture2D mixUseCursor;
    [SerializeField] private Texture2D standardCursor;
    public Interactable interactableWaiting
    {
        get { return _interactableWaiting; }
        set
        {
            _interactableWaiting = value;
            Cursor.SetCursor(_interactableWaiting == null ? mixUseCursor : standardCursor, new Vector2(66, 66), CursorMode.Auto);
            WorldManager.Instance.RecalculateWorldTiles();
        }
    }
    private Interactable _interactableWaiting { get; set; }

    public void LookForSecondObjectToUse()
    {
        interactableWaiting = currentInteractable;
        Debug.LogWarning("Saved Interactable to wait for another to be used with.");
    }

}
