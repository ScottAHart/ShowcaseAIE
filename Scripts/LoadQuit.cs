using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Script used to quit application and load scenes
/// </summary>
public class LoadQuit : MonoBehaviour {
    public void Quit()
    {
        Application.Quit();
    }
}
