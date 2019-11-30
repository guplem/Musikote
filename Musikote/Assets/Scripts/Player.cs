using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        instance = this;
        lastKnownPosition = transform.position;
        isMovementFinished = true;
    }

    private IEnumerator MoveTo(Vector3 target)
    {
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
                yield break;
            }
        }
    }

    private IEnumerator RotateThenMove(Vector3 target)
    {
        var targetForRotation = new Vector3(target.x, transform.position.y, target.z);
        var angle = Vector3.Angle(transform.forward, targetForRotation - transform.position);
        var currentRotationAnimationDuration = rotationAnimationDuration * angle / 90;

        while (true)
        {
            yield return new WaitForEndOfFrame();
            currentMovementAnimationDuration += Time.deltaTime;
            var targetDirection = targetForRotation - transform.position;
            //Calculate the time in order the angles to always rotate at the same velocity
            var newRotation = Vector3.Lerp(transform.forward, targetDirection, rotationAnimationCurve.Evaluate(
                currentMovementAnimationDuration / currentRotationAnimationDuration));
            
            transform.rotation = Quaternion.LookRotation(newRotation);
            if (Vector3.Angle(transform.forward, targetForRotation - transform.position) < 1)
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
