using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    private List<Interactable> items = new List<Interactable>();

    [SerializeField] private float movementSpeed; // 3f
    [SerializeField] private float rotationSpeed; // 3f

    private Vector3 lastKnownPosition;
    [SerializeField] private AnimationCurve movementAnimationCurve;
    [SerializeField] private float movementAnimationDuration;
    private float currentMovementAnimationDuration;
    private float currentRotatementAnimationDuration;

    private void Awake()
    {
        instance = this;
        lastKnownPosition = transform.position;
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
                yield break;
            }
        }
    }

    private IEnumerator RotateThenMove(Vector3 target)
    {
        var targetForRotation = new Vector3(target.x, transform.position.y, target.z);
        while (true)
        {
            yield return new WaitForEndOfFrame();
            currentRotatementAnimationDuration += Time.deltaTime;
            var targetDirection = targetForRotation - transform.position;
            var newRotation = Vector3.Lerp(transform.forward, targetDirection, movementAnimationCurve.Evaluate(
                currentRotatementAnimationDuration / movementAnimationDuration));
            
            transform.rotation = Quaternion.LookRotation(newRotation);
            if (Vector3.Angle(transform.forward, targetForRotation - transform.position) < 1)
            {
                currentRotatementAnimationDuration = 0f;
                StartCoroutine(MoveTo(target));
                yield break;
            }
        }
    }

    public void RotateAndMove(Vector3 target)
    {
        StartCoroutine(RotateThenMove(target));
    }

    public void AddToInventory(Interactable item)
    {
        if (items.Count >= 2) Debug.LogError("Inventory completed");
        else items.Add(item);
    }
}
