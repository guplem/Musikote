using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTile : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Player.instance.LookAndRotate(transform.position);
    }
}
