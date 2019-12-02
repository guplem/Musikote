using System;
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

        try
        {
            Objetive firstObj = GameManager.instance.objectives[0];
            if (firstObj != null)
            {
                GameObject go = Instantiate(objectivePrefab, transform);
                go.transform.GetChild(0).GetComponent<TMP_Text>().text = firstObj.title;
                go.transform.GetChild(1).GetComponent<TMP_Text>().text = firstObj.description;
            }
                
                
        } catch (ArgumentException) {   }
        
    }
    
    
}
