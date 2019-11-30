using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public static Player instance;
    private List<Interactable> items = new List<Interactable>();

    private Vector3 lastKnownPosition;
    [SerializeField] private AnimationCurve movementAnimationCurve;
    [SerializeField] private AnimationCurve rotationAnimationCurve;
    [SerializeField] private float movementAnimationDuration;
    [SerializeField] private float rotationAnimationDuration;
    private float currentMovementAnimationDuration;
    private bool isMovementFinished;
    [SerializeField] public Animator animator;

    private void Awake()
    {
        instance = this;
        lastKnownPosition = transform.position;
        isMovementFinished = true;
    }

    private IEnumerator MoveTo(Vector3 target)
    {
        Debug.Log("Moving to " + target);
        animator.SetBool("Walking", true);
        while (true)
        {
            yield return new WaitForEndOfFrame();
            currentMovementAnimationDuration += Time.deltaTime;
            transform.position = Vector3.Lerp(lastKnownPosition, target, movementAnimationCurve.Evaluate(
                currentMovementAnimationDuration / movementAnimationDuration));
            
            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                WorldManager.Instance.RecalculateWorldTiles();
                currentMovementAnimationDuration = 0f;
                lastKnownPosition = transform.position;
                isMovementFinished = true;
                animator.SetBool("Walking", false);
                yield break;
            }
        }
    }

    private IEnumerator RotateThenMove(Vector3 target)
    {

        
        Vector3 targetForRotation = new Vector3(target.x+Random.Range(-0.01f, 0.01f), transform.position.y, target.z+Random.Range(-0.01f, 0.01f));
        
        float angle = Vector3.Angle(transform.forward, targetForRotation - transform.position);

        float currentRotationAnimationDuration = rotationAnimationDuration * angle / 90;
        Debug.Log("Rotating to face " + target + " with a duration of " + currentRotationAnimationDuration + "s");
        
        while (true)
        {
            yield return new WaitForEndOfFrame();
            currentMovementAnimationDuration += Time.deltaTime;
            Vector3 targetDirection = targetForRotation - transform.position;
            //Calculate the time in order the angles to always rotate at the same velocity
            Vector3 newRotation = Vector3.Lerp(transform.forward, targetDirection, rotationAnimationCurve.Evaluate( currentMovementAnimationDuration / currentRotationAnimationDuration));
            transform.rotation = Quaternion.LookRotation(newRotation);

            Debug.Log("New rotation = "+ newRotation);
            
            angle = Vector3.Angle(transform.forward, targetForRotation - transform.position);
            if (angle < 1)
            {
                currentMovementAnimationDuration = 0f;
                StartCoroutine(MoveTo(target));
                yield break;
            }
        }
    }
    


    public void RotateAndMove(Vector3 target)
    {
        if (!isMovementFinished) return;
        isMovementFinished = false;
        StartCoroutine(RotateThenMove(target));
    }

    public void AddToInventory(Interactable item)
    {
        if (items.Count >= 2) Debug.LogError("Inventory completed");
        else items.Add(item);
    }
}
