using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://forum.unity.com/threads/performance-issue-with-drawmeshinstancedprocedural.1173008/

public class Procedural : MonoBehaviour
{
    public GameObject obj;

    private Mesh mesh;

    private Material material;

    private MaterialPropertyBlock block; 

    // private Matrix4x4 matrix = new Matrix4x4(1023);
    // Start is called before the first frame update
    void Start()
    {
        mesh = obj.GetComponent<MeshFilter>().mesh;
        material = obj.GetComponent<MeshRenderer>().material;
        block = new MaterialPropertyBlock();
        obj.SetActive(false);
        var positionBuffer = new ComputeBuffer(2, 16);
        Vector4[] positions = new Vector4[2];
        positions[0] = Vector4.one*2;
        positions[1] = Vector4.one;

        positionBuffer.SetData(positions);
        material.SetBuffer("positionBuffer", positionBuffer);
        // positionBuffer.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        // Matrix4x4 m1 = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0,-45,  0), Vector3.one);
        // Matrix4x4 m2 = Matrix4x4.TRS(new Vector3(0,0,2), Quaternion.Euler(-45, 0, 0), Vector3.one);
        // // Graphics.DrawMesh(mesh, m1, material, 0, Camera.current);
        // // Graphics.DrawMesh(mesh, m2, material, 0, Camera.current);
        // var arr = new Matrix4x4[2];
        // arr[0] = m1;
        // arr[1] = m2;
        // List<Vector4> _colors = new List<Vector4>();
        // _colors.Add(Color.red);
        // _colors.Add(Color.blue);
        //
        // List<Vector4> _pos = new List<Vector4>();
        // _pos.Add(Vector4.zero);
        // _pos.Add(Vector4.one*2);
        //
        // MaterialPropertyBlock mb = new MaterialPropertyBlock();
        // mb.SetVectorArray("_Color",_colors);
        // mb.SetVectorArray("_WorldPos",_pos);
        // //Graphics.DrawMeshInstanced(mesh, 0, material, arr,2,mb);

        Graphics.DrawMeshInstancedProcedural(mesh,0,material,mesh.bounds,2);//,mb);
    }
}