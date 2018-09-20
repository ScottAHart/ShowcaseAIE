using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Noise visualiser to show 3D noise.
/// Scales objects depending on float value 
/// </summary>
public class NoiseVis : MonoBehaviour {
    public GameObject _prefab;
	// Use this for initialization
	void Start () {
        float[][][] noise = NoiseGenerator.Perlin3D(3, 3, 3, new Vector3(0, 0, 0), 10);

        for(int i =0; i < noise.Length;  i++)
        {
            for (int j = 0; j < noise[i].Length; j++)
            {
                for (int k = 0; k < noise[i][j].Length; k++)
                {
                    GameObject go = Instantiate<GameObject>(_prefab, transform.position + new Vector3(i * 1, j * 1, k * 1), transform.rotation);
                    go.transform.localScale = new Vector3(noise[i][j][k], noise[i][j][k], noise[i][j][k]);
                }
            }
        }
	}
}
