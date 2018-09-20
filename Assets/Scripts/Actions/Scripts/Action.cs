using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Interface used to tell if something has actions
/// </summary>
public interface IActions
{
    List<Action> GetActions();
    void Apply(Action act);
}
/// <summary>
/// Action information needed on every action
/// </summary>
public abstract class Action : ScriptableObject{
    public Sprite _sprite;
    public string _name;
    public string _description;
    public abstract void Apply(GameObject go);
}
