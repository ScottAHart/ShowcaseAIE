using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Highlights all selectable objects with an emmisive shader to show what can be used
/// Stores original material in a dictionary to replace when turned off
/// </summary>
public class ShowSelectable : MonoBehaviour {
    public Material _glowMat;
    private bool _toggled = false;
    public Dictionary<GameObject, Material> _selectables;
    public PostProcess _screenEffect;
    //Set up 
    void Start()
    {
        _selectables = new Dictionary<GameObject, Material>();
        GetSelectables();
    }
    //Toggles shader
    public void ToggleSelect()
    {
        //Not glowing
        if (!_toggled)
        {
            //Change shader
            GetSelectables();
            foreach(KeyValuePair<GameObject, Material> o in _selectables)
            {
                if (o.Value != null)
                    o.Key.GetComponent<MeshRenderer>().material = _glowMat;
            }

            _screenEffect.enabled = true;

             _toggled = true;
        }
        //Already glowing set back
        else
        {
            foreach (KeyValuePair<GameObject, Material> o in _selectables)
            {
                if (o.Value != null)
                    o.Key.GetComponent<MeshRenderer>().material = o.Value;
            }
            _screenEffect.enabled = false;
            _toggled = false;
        }
    }
    //Get all objects in scene tagged intertable and adds to dictionary
    private void GetSelectables()
    {
        _selectables.Clear();
        GameObject[] gos =  GameObject.FindGameObjectsWithTag("Interactable");
        foreach(GameObject g in gos)
        {
            MeshRenderer mr = g.GetComponent<MeshRenderer>();
            _selectables.Add(g, mr != null ? mr.material : null);
        }
    }
}
