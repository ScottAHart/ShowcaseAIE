using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Deforms the mesh from a list of hit points 
/// Made for project, modified to work more meshes
/// </summary>
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class MeshDeform : MonoBehaviour {
    public List<HitInfo> _hitPoints = new List<HitInfo>();
    public Vector3 _pos;
    public float _pointSize = 1f;
    public float _deformAmount = 0.1f;
    
    // Checks for input to see if the user clicked the sphere.
    //If call hit at position
    void Update ()
    {
        //Input check for clicks on object
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    _pos = hit.point;
                    Hit(_pos,  -hit.normal);
                }
            }
        }

	}
    //Adds hit to list and recalc shape
    //Could use this for impact collision (on collision enter)
    private void Hit(Vector3 pos, Vector3 dir)
    {
        _hitPoints.Add(new HitInfo(pos,dir));
        CalcShape();
    }
    //Deforms mesh depending on hit locations.
    private void CalcShape()
    {
        //Grab current mesh and collider
        MeshFilter filter = GetComponent<MeshFilter>();
        Mesh mesh = filter.mesh;
        MeshCollider collider = GetComponent<MeshCollider>();


        //Easier use of variables
        Vector3[] vertices = mesh.vertices;
        Vector2[] uvs = mesh.uv;
        Vector3[] normals = mesh.normals;


        //For each hit point check all vertices on shape to see if it was close enough
        for (int j = 0; j < _hitPoints.Count; j++) {
            for (int i = 0; i < vertices.Length; i++)
            {
                //Mesh to world pos
                Vector3 vert = transform.localRotation * new Vector3(vertices[i].x * transform.localScale.x, vertices[i].y * transform.localScale.y, vertices[i].z * transform.localScale.z) + transform.position;
                //
                if (Vector3.Magnitude(vert - _hitPoints[j].pos) < _pointSize)
                {
                    //Displacement not dist
                    Vector3 dis = _hitPoints[j].dir.normalized * _deformAmount;                    
                    dis = Quaternion.Inverse(transform.rotation) * dis; //transform rotation
                    vertices[i] += dis;
                }

            }
        }
        //Update mesh
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.normals = normals;
        
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        //Replace colider
        collider.sharedMesh = mesh;

        filter.mesh = mesh;
        _hitPoints.Clear();
    }
    //Struct for hit info, where and what direction to move mesh
    public struct HitInfo
    {
        public Vector3 pos;
        public Vector3 dir;

        public HitInfo(Vector3 p, Vector3 d)
        {
            pos = p;
            dir = d;
        }
    }
}
