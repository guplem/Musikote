using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bed : Interactable
{
    [SerializeField] private Objetive ObjectiveToComplete;
    [SerializeField] private AudioClip itIsNoTimeForBed;
    public override bool Use()
    {
        if (GameManager.instance.objectives.Count > 1)
        {
            AudioController.Instance.PlayClip(itIsNoTimeForBed, Player.instance.transform.position);
            return false;
        }
        
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
