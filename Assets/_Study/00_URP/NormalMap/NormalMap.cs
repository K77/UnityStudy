using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class NormalMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float3x3 a = new float3x3(float3.zero, new float3(1,1,1), new float3(2,2,2));
        Debug.Log(a[1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
