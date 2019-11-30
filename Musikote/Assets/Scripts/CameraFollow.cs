using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private GameObject objectToFollow;
    [SerializeField] private float distance = 5;
    [SerializeField] private float duration = 1;
    private Vector3 velocity = Vector3.zero;
    
    void Update()
    {
        if (objectToFollow == null) return;
        Vector3 targetPosition = new Vector3(objectToFollow.transform.position.x-distance, transform.position.y, objectToFollow.transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, duration);
    }
}
