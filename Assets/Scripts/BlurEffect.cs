using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Uses blur post processing effect to blur over time.
/// </summary>
public class BlurEffect : MonoBehaviour {
    public Material _materail;
    public float _blurStrength = 1;
    private float _blurCurrent = 0;
    public float _blurWidth = 0.2f;
    public float _blurRecovery = 3.0f;
    private float _blurRecTimer = 0;


	// Update is called once per frame
	void Update () {
        //Lerps to target value
        _blurCurrent = Mathf.Lerp(_blurStrength, 0, _blurRecTimer / _blurRecovery);
        if(_blurRecTimer < _blurRecovery)
        {
            _blurRecTimer += Time.deltaTime;
        }
    }
    //Applies shader
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //If opengl this will not be 0-1, will be 0-width(big)
        float ImageWidth = 1;
        float ImageHeight = 1;

        _materail.SetFloat("_BlurStrength", _blurCurrent);
        _materail.SetFloat("_BlurWidth", _blurWidth);

        _materail.SetFloat("_imgHeight", ImageWidth);
        _materail.SetFloat("_imgWidth", ImageHeight);

        _materail.SetFloat("_clearSize", 10);

        Graphics.Blit(source, destination, _materail);


    }
    /// <summary>
    ///
    ///Public var to set blur 
    /// </summary>
    /// <param name="amt">Set blur to amt</param>
    public void SetBlur(float amt)
    {
        _blurStrength = amt;
        _blurCurrent = _blurStrength;
        _blurRecTimer = 0;
    }
    //Premade hit blur 8 blur amount
    public void HitBlur()
    {
        SetBlur(8);
    }
}
