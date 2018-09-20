using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// To test shaders timer variables
/// </summary>
public class ShaderThings : MonoBehaviour {
    public Renderer[] _renderers;
	// Use this for initialization
	void Start () {
        _renderers = GetComponentsInChildren<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Renderer r in _renderers)
        {
            r.material.SetFloat("_Timer", Time.time);
        }
	}
}
