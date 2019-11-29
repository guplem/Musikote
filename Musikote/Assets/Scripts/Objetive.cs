using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New objective", menuName = "Objective")]
public class Objetive : ScriptableObject
{
    private string name;
    private string description;
    private bool isDone;
}
