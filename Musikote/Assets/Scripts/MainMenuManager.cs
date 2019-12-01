﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
            SceneManager.LoadScene(sceneToLoad);
    }
}
