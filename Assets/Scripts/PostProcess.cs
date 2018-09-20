using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Simple post process attached to camera to render post process shader
/// </summary>
public class PostProcess : MonoBehaviour {
    public Material _materail;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _materail);
        
    }
}
