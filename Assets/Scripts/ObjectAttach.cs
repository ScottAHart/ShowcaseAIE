using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// Script that checks mouse input and connects a laser from object to selected object
/// Lerps to laser to the target at a speed
/// </summary>
public class ObjectAttach : MonoBehaviour {
    public LineRenderer _line;
    private GameObject _obj;
    private Vector3 _point; // on object
    public EventSystem _eventSys;
    [Tooltip("Dist Per Meter")]
    public float _connectSpeed = 20; 
    public float _currentTime = 0;
    public Transform _connect;
    public float _connectTime;
    public float _dist;
    
    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0) && _eventSys.IsPointerOverGameObject() == false)
        {
            GetComponent<PlayerControl>().SetAttached(null);
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                //Hit something that has actions
                if (hit.collider.gameObject.GetComponent<IActions>() != null)
                {
                    _obj = hit.collider.gameObject;
                    _point = hit.point - _obj.transform.position;
                    _currentTime = 0;
                    _line.gameObject.SetActive(true);
                    _dist = Vector3.Magnitude(_obj.transform.position + _point - _connect.position); //Dist in meters
                    _connectTime = _dist / _connectSpeed;
                }
            }
        }
        //If attached, set line up
        if (_obj)
        {

            _currentTime = _currentTime > _connectTime + 5 ? _connectTime : _currentTime + Time.deltaTime;
            _line.SetPosition(0, _connect.position);
           
            _line.SetPosition(1, Vector3.Lerp(_connect.position, _obj.transform.position + _point, _currentTime / _connectTime));
        }
        //If connected
        if (_currentTime >= _connectTime)
        {
            GetComponent<PlayerControl>().SetAttached(_obj);
        }
        //Disable line if nothing selected or selected destroyed
        if (_obj == null)
        {
            _line.gameObject.SetActive(false);
        }



    }
}
