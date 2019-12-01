using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIObjectiveManager : MonoBehaviour
{

    [SerializeField] private GameObject objectivePrefab;


    public void Setup()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Objetive objective in GameManager.instance.objectives)
        {
            GameObject go = Instantiate(objectivePrefab, transform);
            go.transform.GetChild(0).GetComponent<TMP_Text>().text = objective.title;
            go.transform.GetChild(1).GetComponent<TMP_Text>().text = objective.description;
        }
    }
    
    
}
