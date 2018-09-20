using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Simple move player to positoin and play an effect
/// </summary>
public class ReturnPlayer : MonoBehaviour {
    public Transform _position;
    public GameObject _particle;
    //Called to return player to set postion
    public void Return()
    {
        PlayerControl.Instance.transform.position = _position.position;

        GameObject part = Instantiate<GameObject>(_particle, PlayerControl.Instance.transform.position, PlayerControl.Instance.transform.rotation);
        Destroy(part, 2);
    }
}
