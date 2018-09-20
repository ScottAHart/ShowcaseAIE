using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Spanwer that spawns an object at a position at set rate
/// particle system will be turned on or off when this spawner is active 
/// </summary>
public class Spawner : MonoBehaviour {
    public GameObject _spawnObj;
    public Transform _targPos;
    public float _spawnRate;
    public GameObject _particles;

    private float _spawnTime = 0;
	// Update is called once per frame
	void Update () {
        //Check time 
		if(Time.time -_spawnTime > _spawnRate)
        {
            //spawn
            GameObject ob = Instantiate<GameObject>(_spawnObj, _targPos.position, _targPos.rotation);
            _spawnTime = Time.time;

            // for zombies
            if (ob.GetComponent<ZombieController>() != null)
                ob.GetComponent<ZombieController>()._target = FindObjectOfType<PlayerControl>().transform;

            //
        }
	}
    //Toggle particles
    private void OnEnable()
    {
        _particles.SetActive(true);
    }
    private void OnDisable()
    {
        _particles.SetActive(false);
    }
}
