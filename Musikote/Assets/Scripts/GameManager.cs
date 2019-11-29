using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private List<Objetive> _objetives;

    private void Awake()
    {
        instance = this;
    }
    
}
