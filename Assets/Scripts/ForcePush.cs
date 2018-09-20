using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Pushes all objects within its collider
/// Uses update so that it can be enable/disabled
/// </summary>
public class ForcePush : MonoBehaviour {
    private List<Rigidbody> _objs; //Objects to push
    public Vector3 _force = new Vector3(0,1,0); //Direction of push
    public ParticleSystem _particles; //Effect
    //Clear objs 
    private void Awake()
    {
        _objs = new List<Rigidbody>();
    }
    //Fixed update for physics
    private void FixedUpdate()
    {
        foreach (Rigidbody r in _objs)
        {
            r.AddForce(_force );
        }
    }
    //Add objects
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody oRig = other.GetComponent<Rigidbody>();
        _objs.Add(oRig);
    }
    //Remove object
    private void OnTriggerExit(Collider other)
    {
        Rigidbody oRig = other.GetComponent<Rigidbody>();
        _objs.Remove(oRig);
    }
    //Play particles
    private void OnEnable()
    {
        _particles.Play();
    }
    //Stop particles
    private void OnDisable()
    {
        _particles.Stop();
    }
    //Old Force applier
    /*
    private void OnTriggerStay(Collider other)
    {
        Rigidbody oRig = other.GetComponent<Rigidbody>();
        if (oRig != null)
        {

            oRig.AddForce(_force );
            //Particle effect
                //Create
                //Add to obj
        }

    }
    */
}
