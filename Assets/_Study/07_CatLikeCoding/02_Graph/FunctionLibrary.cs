using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;


public static class FunctionLibrary 
{
    public delegate float Function (float x, float z, float t);
    public enum FunctionName { Wave, MultiWave, Ripple }

    static Function[] functions = { Wave, MultiWave, Ripple };

    public static Function GetFunction (int index)
    {
        if (index < 0) index = 0;
        if (index >= functions.Length) index = functions.Length - 1;
        return functions[index];
    }
    
    public static float Wave (float x,float z, float t) {
        return Sin(PI * (x + z + t));
    }
    
    public static float MultiWave (float x, float z,float t) {
        float y = Sin(PI * (x + t));
        y += Sin(2f * PI * (x + t)) / 2f;
        return y;
    }
    public static float Ripple (float x, float z, float t) {
        float d = Abs(x);
        float y = Sin(PI * (4f * d - t));
        return y / (1f + 10f * d);
    }
}
