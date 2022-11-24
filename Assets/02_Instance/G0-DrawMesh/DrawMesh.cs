using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMesh : MonoBehaviour
{
    public Camera camera;
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
    }

    // Update is called once per frame
    void Update()
    {
        Matrix4x4 m1 = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0,-45,  0), Vector3.one);
        Matrix4x4 m2 = Matrix4x4.TRS(new Vector3(0,0,2), Quaternion.Euler(-45, 0, 0), Vector3.one);
        Graphics.DrawMesh(mesh, m1, material, 0, Camera.current);
        Graphics.DrawMesh(mesh, m2, material, 0, Camera.main);
    }
}
