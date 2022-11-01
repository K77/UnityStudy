using UnityEngine;

public class GPUGraph : MonoBehaviour {

    //[SerializeField]
    //Transform pointPrefab;

    [SerializeField, Range(10, 200)]
    int resolution = 10;

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
        
    }

    void PickNextFunction()
    {
        
    }

    //void UpdateFunction () { … }

    //void UpdateFunctionTransition () { … }
}