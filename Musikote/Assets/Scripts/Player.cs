using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    private List<Interactable> items;

    [SerializeField] private float movementSpeed; // 7f
    [SerializeField] private float rotationSpeed; // 7f

    private void Awake()
    {
        instance = this;
    }

    private IEnumerator MoveTo(Vector3 target)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target) < 0f) yield break;
        }
    }

    private IEnumerator LookAt(Vector3 target)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            var targetDirection = target - transform.position;
            var newRotation = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newRotation);
            Debug.DrawRay(transform.position, newRotation, Color.red);
            if (Vector3.Angle(transform.forward, target - transform.position) < 1)
            {
                StartCoroutine(MoveTo(target));
                yield break;
            }
        }
    }

    public void LookAndRotate(Vector3 target)
    {
        StartCoroutine(LookAt(target));
    }

    public void AddToInventory(Interactable item)
    {
        if (items.Count >= 2) Debug.LogError("Inventory completed");
        else items.Add(item);
    }
}
