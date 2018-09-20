using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Change the shader to set material
/// Used to toggle back to original material
/// </summary>
[CreateAssetMenu(menuName = "Actions/ShaderAction")]
public class ShaderChange : Action {
    public Material _newMat;
    private Material _defaultMat;
    private bool _applied;
    public override void Apply(GameObject go)
    {
        go.GetComponent<MeshRenderer>().material = _newMat;
        /*
        if (!_applied)
        {
            if(_defaultMat == null)
                _defaultMat = go.GetComponent<MeshRenderer>().material;
            
            _applied = true;
        }
        else
        {
            go.GetComponent<MeshRenderer>().material = _defaultMat;
        }
        */
    }
}
