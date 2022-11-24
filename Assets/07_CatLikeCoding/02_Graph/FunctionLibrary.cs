using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;


public static class FunctionLibrary 
{
    public delegate Vector3 Function (float x, float z, float t);
    public enum FunctionName { Wave, MultiWave, Ripple ,Sphere,Torus}

    static Function[] functions = { Wave, MultiWave, Ripple, Sphere, Torus};

    public static Function GetFunction (int index)
    {
        if (index < 0) index = 0;
        if (index >= functions.Length) index = functions.Length - 1;
        return functions[index];
    }
    
    public static Vector3 Wave (float u,float v, float t) {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + v + t));
        p.z = v;
        return p;    
    }
    
    public static Vector3 MultiWave (float u, float v, float t) {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + 0.5f * t));
        p.y += 0.5f * Sin(2f * PI * (v + t));
        p.y += Sin(PI * (u + v + 0.25f * t));
        p.y *= 1f / 2.5f;
        p.z = v;
        return p;
    }

    public static Vector3 Ripple (float u, float v, float t) {
        float d = Sqrt(u * u + v * v);
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (4f * d - t));
        p.y /= 1f + 10f * d;
        p.z = v;
        return p;
    }
    public static Vector3 Sphere (float u, float v, float t) {
        float r = 0.5f + 0.5f * Sin(PI * t);
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(0.5f * PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }
    
    public static Vector3 Torus (float u, float v, float t) {
        float r = 1f;
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(0.5f * PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }


}
