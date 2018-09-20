using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Camera that follows target by a set amount and allows for mouse control
/// Scroll wheel for zoom distance. 
/// 
/// </summary>
public class FollowCam : MonoBehaviour {

    public Transform _target;
    public Vector3 _offset; //Displacement from target transform
    public float _speed = 5; //Speed of camera movement 
    public float _zoomSpeed = 100;
    private float _distFrom;
    private float _lastMouseX, _lastMouseY;
    public float _distanceMin = 0.5f;
    public float _distanceMax = 20.0f; //
    public float _sens = 10f; //Modifier for speed
    // Use this lastMouseYfor initialization
    void Start () {
		if(_target != null)
        {
            _distFrom = (_target.position - transform.position).magnitude;
        }
	}
	
	// Update is called once per frame
	void Update () {

        //Dist
        _distFrom -= Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed * Time.deltaTime;
        _distFrom = Mathf.Clamp(_distFrom, _distanceMin, _distanceMax);
        //Mouse input 
        if (Input.GetMouseButton(1))
        {
            if (_lastMouseX > 0 || _lastMouseY > 0)
            {
                //
                float deltaX = Input.mousePosition.x - _lastMouseX;
                float deltaY =  _lastMouseY - Input.mousePosition.y;

                // set the camera's rotation
                Vector3 angles = transform.eulerAngles + (Vector3.right * deltaY + Vector3.up * deltaX) * Time.deltaTime * _speed * _sens;
                if (angles.x > 180)
                    angles.x -= 360;
                angles.x = Mathf.Clamp(angles.x, 0, 70);
                transform.eulerAngles = angles;
            }

            _lastMouseX = Input.mousePosition.x;
            _lastMouseY = Input.mousePosition.y;
        }
        if (Input.GetMouseButtonUp(1))
        {
            _lastMouseX = -1;
            _lastMouseY = -1;
        }
        // position the camera looking at the target, with a height offset so we don't focus on their feet
        transform.position = _target.position - transform.forward * _distFrom + _offset;
    }
    //Sesitivity change slider. 
    public void SensChange(Slider slider)
    {
        _sens = slider.value;
    }
}
