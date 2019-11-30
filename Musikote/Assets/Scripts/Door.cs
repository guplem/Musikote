using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{

    private bool isOpen = false;
    
    
    public new bool Use()
    {
        if (!base.Use()) return false;

        if (isOpen)
            return Close();
        else
            return Open();
    }
    
    public new bool Open()
    {
        if (!base.Use()) return false;


        return true;
    }
    
    public new bool Close()
    {
        if (!base.Use()) return false;


        return true;
    }

}
