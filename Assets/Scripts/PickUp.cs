using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Pick up class for item to add score to player
/// </summary>
public class PickUp : MonoBehaviour {
    public int _scoreIncrease = 1;
    public GameObject _particles;
    /// <summary>
    /// Checks to see if other object contains a score holder if so add score to it and delete self.
    /// If particles attached they are also played. Hard coded to destroy self after 2 seconds. 
    /// </summary>
    /// <param name="other">Rigid Body this collided with.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ScoreHolder>() != null)
        {
            other.GetComponent<ScoreHolder>().AddScore(_scoreIncrease);
            if (_particles != null)
            {
                GameObject part = Instantiate<GameObject>(_particles, transform.position, transform.rotation);
                Destroy(part, 2);
            }
            Destroy(gameObject);
        }
    }


}
