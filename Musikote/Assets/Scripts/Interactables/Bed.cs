using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bed : Interactable
{
    [SerializeField] private Objetive ObjectiveToComplete;
    
    public override bool Use()
    {
        if (!base.Use()) return false;
        GameManager.instance.CompleteObjective(ObjectiveToComplete);
        Invoke(nameof(LoadFinalScene), 2f);
        return true;
    }

    public void LoadFinalScene()
    {
        SceneManager.LoadScene("FinalScene");
    }
}
