using UnityEngine;

public class GPUGraph : MonoBehaviour {

    static readonly int
        positionsId = Shader.PropertyToID("_Positions"),
        resolutionId = Shader.PropertyToID("_Resolution"),
        stepId = Shader.PropertyToID("_Step"),
        timeId = Shader.PropertyToID("_Time");

    [SerializeField, Range(10, 200)]
    int resolution = 10;
    [SerializeField]
    ComputeShader computeShader;
    
    [SerializeField]
    Material material;

    [SerializeField]
    Mesh mesh;

    [SerializeField]
    FunctionLibrary.FunctionName function;

    public enum TransitionMode { Cycle, Random }

    [SerializeField]
    TransitionMode transitionMode = TransitionMode.Cycle;

    [SerializeField, Min(0f)]
    float functionDuration = 1f, transitionDuration = 1f;
    
    ComputeBuffer positionsBuffer;

    //Transform[] points;

    float duration;

    bool transitioning;

    FunctionLibrary.FunctionName transitionFunction;

    //void Awake () { … }
    void OnEnable () {
        positionsBuffer = new ComputeBuffer(resolution * resolution, 3 * 4);
    }
    void OnDisable () {
        positionsBuffer.Release();
        positionsBuffer = null;
    }
    void Update()
    {
        UpdateFunctionOnGPU();
    }

    void PickNextFunction()
    {
        
    }
    
    void UpdateFunctionOnGPU () {
        float step = 2f / resolution;
        computeShader.SetInt(resolutionId, resolution);
        computeShader.SetFloat(stepId, step);
        computeShader.SetFloat(timeId, Time.time);
        computeShader.SetBuffer(0, positionsId, positionsBuffer);
        int groups = Mathf.CeilToInt(resolution / 8f);
        computeShader.Dispatch(0, groups, groups, 1);
        var bounds = new Bounds(Vector3.zero, Vector3.one * 2f);
        Graphics.DrawMeshInstancedProcedural(mesh, 0, material, bounds, positionsBuffer.count);
    }

    //void UpdateFunction () { … }

    //void UpdateFunctionTransition () { … }
}