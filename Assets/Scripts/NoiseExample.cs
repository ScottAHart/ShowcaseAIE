using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Used to test noise
/// prints the perlin noise onto screen using ongui
/// </summary>
public class NoiseExample : MonoBehaviour {
    public int pixWidth;
    public int pixHeight;
    public float xOrg;
    public float yOrg;
    public float scale = 1.0F;
    private Texture2D noiseTex;
    private Color[] pix;
    private Renderer rend;
    //Set up
    void Start()
    {
        rend = GetComponent<Renderer>();
        noiseTex = new Texture2D(pixWidth, pixHeight);
        pix = new Color[noiseTex.width * noiseTex.height];
        rend.material.mainTexture = noiseTex;
    }
    //Get noise for target size
    void CalcNoise()
    {
        float y = 0.0F;
        while (y < noiseTex.height)
        {
            float x = 0.0F;
            while (x < noiseTex.width)
            {
                float xCoord = xOrg + x / noiseTex.width * scale;
                float yCoord = yOrg + y / noiseTex.height * scale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                pix[(int)(y * noiseTex.width + x)] = new Color(sample, sample, sample);
                x++;
            }
            y++;
        }

        noiseTex.SetPixels(pix);
        noiseTex.Apply();
    }
    
    void Update()
    {
        CalcNoise();
    }
    //Draw texture
    private void OnGUI()
    {
        Texture2D test = new Texture2D(50, 50);
        Color[] pixel = new Color[50 * 50];
        float[][] noise = NoiseGenerator.Perlin2D(200, 200, new Vector2(0,0), 10);
        for (int i = 0; i < 50; i++)
            for (int j = 0; j < 50; j++)
                pixel[i * 50 + j] = new Color(noise[i][j], noise[i][j], noise[i][j]);
        test.SetPixels(pixel);
        test.Apply();
        GUI.DrawTexture(new Rect(100, 100, 50, 50), test);
    }
}

