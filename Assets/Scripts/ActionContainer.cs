using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// An object that can have actions applied to it. 
/// Holds what actions and supplies actions functions
/// </summary>
public class ActionContainer : MonoBehaviour, IActions{

    public List<Action> _actions;

    public void Apply(Action act)
    {
        act.Apply(gameObject);
    }

    public List<Action> GetActions()
    {
        return _actions;
    }
}
