using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves an object and moves it back after some time 
/// </summary>
[CreateAssetMenu(menuName = "Actions/MoveOffset")]
public class MoveOffset : Action
{
    public Vector3 _move;
    public float _time;
    public float _returnDelay;
    //Coroutines dont work in scriptable object apparently.. 
    //Uses manager 
    public override void Apply(GameObject go)
    {
        
        CoroutineManager.Instance.StartCoroutine(CoroutineManager.Instance.Move( go, _move, _time));
        CoroutineManager.Instance.StartCoroutine(CoroutineManager.Instance.MoveAfter(go, -_move, _time, _returnDelay));

    }

}
