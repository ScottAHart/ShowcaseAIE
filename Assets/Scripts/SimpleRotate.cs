using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Simple script to rotate an object 
/// </summary>
public class SimpleRotate : MonoBehaviour {
    public Vector3 _dir;
    public float _speed = 5;

	void Update () {
        transform.Rotate(_dir * _speed * Time.deltaTime);
	}
}
