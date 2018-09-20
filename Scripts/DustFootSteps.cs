using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Used with animation events to give the player particle effects on foot steps
/// </summary>
public class DustFootSteps : MonoBehaviour {
    public Transform _lftFoot;
    public Transform _rhtFoot;
    public GameObject _particlePre;
	
    //Spawns particle on left foot
    void StepL()
    {
        GameObject obj = Instantiate<GameObject>(_particlePre, _lftFoot.position, _lftFoot.rotation);
        Destroy(obj, 1);
    }
    //Spawns partickl on right foot
    void StepR()
    {
        GameObject obj = Instantiate<GameObject>(_particlePre, _rhtFoot.position, _rhtFoot.rotation);
        Destroy(obj, 1);
    }
}
