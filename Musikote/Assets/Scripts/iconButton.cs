using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class iconButton : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private Image img;

    public void Setup()
    {
        img.color = !btn.interactable ? btn.colors.disabledColor : Color.white;
    }
}
