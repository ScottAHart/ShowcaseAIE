using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Simple spawn object script for a button
/// </summary>
public class SpawnItem : MonoBehaviour {
    public GameObject _item;
    public Transform _location;
    public void Spawn()
    {
        Instantiate<GameObject>(_item, _location.position, _location.rotation);
    }
}
