using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Uses 3D perlin noise to deform the mesh in a smooth way
/// Calcs range and converts 0-1 values and range to 3D space of sphere
/// </summary>
[CreateAssetMenu(menuName = "Actions/NoiseMesh")]
public class NoiseMesh : Action {
    public int detail = 100;

    public float disPlaceAmt = 0.1f;
    public override void Apply(GameObject go)
    {
        MeshFilter filter = go.GetComponent<MeshFilter>();
        Mesh mesh = filter.mesh;
        MeshCollider collider = go.GetComponent<MeshCollider>();



        Vector3[] vertices = mesh.vertices;
        Vector2[] uvs = mesh.uv;
        Vector3[] normals = mesh.normals;

        float minX = vertices[0].x; float minY = vertices[0].y; float minZ = vertices[0].z; float maxX = vertices[0].x; float maxY = vertices[0].y; float maxZ = vertices[0].z;

        for (int i = 0; i < vertices.Length; i++)
        {
            if (vertices[i].x < minX) minX = vertices[i].x;
            if (vertices[i].y < minY) minY = vertices[i].y;
            if (vertices[i].z < minZ) minZ = vertices[i].z;
            if (vertices[i].x > maxX) maxX = vertices[i].x;
            if (vertices[i].y > maxY) maxY = vertices[i].y;
            if (vertices[i].z > maxZ) maxZ = vertices[i].z;
            
        }
        //Debug.Log(minX + "-" + maxX + "," + minY + "-" + maxY + "," + minZ + "-" + maxZ );

        float xRange = maxX - minX;
        float yRange = maxY - minY;
        float zRange = maxZ - minZ;
        float[][][] field = NoiseGenerator.Perlin3D(detail, detail, detail, new Vector3(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3)), 2);

        //foreach (float[][] a in field)
        //    foreach (float[] b in a)
        //        foreach (float c in b)
        //           // Debug.Log(c);

        for(int i = 0; i < vertices.Length; i++)
        {
            //Range(-0.5 to 0.5) -> Detail(0-99)
            int xFieldIndex = (int)(((vertices[i].x - minX )/ xRange) * (detail - 1));
            int yFieldIndex = (int)(((vertices[i].y - minY) / yRange) * (detail - 1));
            int zFieldIndex = (int)(((vertices[i].z - minZ) / zRange) * (detail - 1));
            //0 - 1;
            float amt = field[xFieldIndex][yFieldIndex][zFieldIndex];

          //  Debug.Log(amt);
            // 0-1 -> Displace Amount min-max (-0.1 - 0.1)
            amt = amt *(2f*disPlaceAmt) - disPlaceAmt;
            //Debug.Log(amt);

            vertices[i] += new Vector3( amt,amt, amt);

        }
        //Reset values on mesh 
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.normals = normals;


        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        //Update collider
        collider.sharedMesh = mesh;
        
        filter.mesh = mesh;      
    }
}
