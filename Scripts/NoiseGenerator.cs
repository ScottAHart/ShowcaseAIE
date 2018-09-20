using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Generator used to create perlin noise in 2D or 3D space
/// Uses Mathf.PerlinNoise to generate a 2D array of floats
/// </summary>
public static class NoiseGenerator {
   
    public static float[][] Perlin2D(int width, int height, Vector2 offset, float scale)
    {
        float[][] map = new float[width][];

        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new float[height];
        }

        int y = 0;
        while (y < height)
        {
            int x = 0;
            while (x < width)
            {
                float xCoord = offset.x + (float)x / width * scale;
                float yCoord = offset.y + (float)y / height * scale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                map[x][y] = sample;
                x++;
            }
            y++;
        }
        
        return map;
    }
    /// <summary>
    /// 3D map uses Mathf on 3 axis to get smooth noise
    /// </summary>
    /// <param name="width">array size width</param>
    /// <param name="height">array size height</param>
    /// <param name="depth">array size depth</param>
    /// <param name="offset">Used to get different noise with same values</param>
    /// <param name="scale">Detail on noise</param>
    /// <returns></returns>
    public static float[][][] Perlin3D(int width, int height, int depth, Vector3 offset, float scale)
    {
        float[][][] map = new float[width][][];

        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new float[height][];
            for(int j = 0; j < map[i].Length; j++)
            {
                map[i][j] = new float[depth];
            }
        }
        int z = 0;
        while (z < depth)
        {
            int y = 0;
            while (y < height)
            {
                int x = 0;
                while (x < width)
                {
                    float xCoord = offset.x + (float)x / width * scale;
                    float yCoord = offset.y + (float)y / height * scale;
                    float zCoord = offset.z + (float)z / height * scale;

                    float sample = Perlin3DPoint(xCoord, yCoord, zCoord);
                    map[x][y][z] = sample;
                    x++;
                }
                y++;
            }
            z++;
        }

        return map;
    }
    //3D perlin point using 2D Math function. 
    private static float Perlin3DPoint(float x, float y, float z)
    {
        float AB = Mathf.PerlinNoise(x, y);
        float AC = Mathf.PerlinNoise(x, z);
        float BA = Mathf.PerlinNoise(y, y);
        float BC = Mathf.PerlinNoise(y, z);
        float CA = Mathf.PerlinNoise(z, x);
        float CB = Mathf.PerlinNoise(z, y);

        float ABC = AB + BC + AC + BA + CB + CA;
        return ABC / 6f;
    }
}
