using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script used to set up areas enables all listed objects and disables all listed. 
/// Useful to stop Zombies constantly going for you or particles that are not neeeded.
/// </summary>
public class EnableDisable : MonoBehaviour {
    public GameObject[] _enableThese;
    public GameObject[] _disableThese;

    /// <summary>
    /// Checks to see if other object is the player. If so enable/disable gameobjects
    /// </summary>
    /// <param name="other">Other GameObject, Only care if its Player</param>
    private void OnTriggerEnter(Collider other)
    {
        PlayerControl pc = other.GetComponent<PlayerControl>();
        if (pc != null && pc == PlayerControl.Instance)
        {
            foreach (GameObject g in _enableThese)
                g.SetActive(true);
            foreach (GameObject g in _disableThese)
                g.SetActive(false);
        }
    }
}
