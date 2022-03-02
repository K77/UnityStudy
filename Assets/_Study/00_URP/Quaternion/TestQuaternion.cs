using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TestQuaternion : MonoBehaviour
{
    public GameObject _obj;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        _obj.transform.Rotate(45,45,0);
        Debug.Log(Quaternion.Normalize(_obj.transform.rotation));
        var q = _obj.transform.rotation;
        float tmp = q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w;
        Debug.Log(tmp);
    }

    // Update is called once per frame
    void Update()
    {
        // _obj.transform.LookAt(Camera.main.transform.position);
        // _obj.transform.Rotate();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,100,100),"reset"))
        {
            _obj.transform.rotation = Quaternion.identity;
        }
        if (GUI.Button(new Rect(0,100,100,100),"reset"))
        {
            Vector3 zhou = Vector3.right.normalized;
            float jiaodu = 45f*Mathf.PI/180f;
            Quaternion q = new Quaternion(Mathf.Sin(jiaodu / 2) * zhou.x, Mathf.Sin(jiaodu / 2) * zhou.y,
                Mathf.Sin(jiaodu / 2) * zhou.z, math.cos(jiaodu / 2));
            
            float tmp = q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w;
            Debug.Log(tmp);
            
            _obj.transform.rotation = q;
        }
    }
}
