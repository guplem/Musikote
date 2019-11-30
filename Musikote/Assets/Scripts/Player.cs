﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public static Player instance;
    [HideInInspector] public Interactable[] items;

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
        items = new Interactable[2];
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
        
        if (!DoesItemExistAt(0)) 
            items[0] = item;
        else if (!DoesItemExistAt(1)) 
            items[1] = item;
        else
            Debug.LogWarning("Inventory completed by " + items[0] + " and " + items[1]);
        
        
        UIManager.instance.inventory.UpdateVisuals();
    }

    public bool DoesItemExistAt(int i)
    {
        try
        {
            string kkk = items[i].gameObject.name;
            return true;
        }
        catch (NullReferenceException)
        {
            Debug.Log("OBJ " + i + " is free");
            return false;
        }
    }

    public void RemoveFromInventory(Interactable interactable)
    {
        if (items[0] == interactable) items[0] = null;
        if (items[1] == interactable) items[1] = null;
        UIManager.instance.inventory.UpdateVisuals();
    }

    public bool IsITemInInventory(Interactable item)
    {
        return items[0] == item || items[0] == item;
    }
}
