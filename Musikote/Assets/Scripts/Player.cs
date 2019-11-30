using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    private List<Interactable> items = new List<Interactable>();

    [SerializeField] private float movementSpeed; // 5f
    [SerializeField] private float rotationSpeed; // 5f

    private void Awake()
    {
        instance = this;
    }

    private IEnumerator MoveTo(Vector3 target)
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                WorldManager.Instance.RecalculateWorldTiles();
                yield break;
            }
        }
    }

    private IEnumerator RotateThenMove(Vector3 target)
    {
        Vector3 targetForRotation = new Vector3(target.x, transform.position.y, target.z);
        while (true)
        {
            yield return new WaitForEndOfFrame();
            var targetDirection = targetForRotation - transform.position;
            var newRotation = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newRotation);
            Debug.DrawRay(transform.position, newRotation, Color.red);
            if (Vector3.Angle(transform.forward, targetForRotation - transform.position) < 1)
            {
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
