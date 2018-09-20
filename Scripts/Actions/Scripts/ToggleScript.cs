using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Turn off and on set comonent script on an object
/// </summary>
[CreateAssetMenu(menuName = "Actions/ToggleScript")]
public class ToggleScript : Action
{
        public Object _scriptType;

        public override void Apply(GameObject go)
        {
        //Use get compenent to find a component with the type of input script 
        //Debug.Log(_scriptType.name);
        (go.GetComponent(_scriptType.name) as MonoBehaviour).enabled = !(go.GetComponent(_scriptType.name) as MonoBehaviour).enabled;
        }
    }
