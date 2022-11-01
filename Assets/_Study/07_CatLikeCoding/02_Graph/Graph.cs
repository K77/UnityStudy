using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private Transform pointPrefab;
    [SerializeField]
    int resolution = 55; 
    [SerializeField]
    int indFunc = 0;

    private Transform[] points;// = new List<Transform>();
    private Vector3[] pos;
    // Start is called before the first frame update
    void Start()
    {
        float step = 2f / resolution;
        var position = Vector3.zero;
        var scale = Vector3.one * step *0.7f;
        points = new Transform[resolution * resolution];
        pos = new Vector3[resolution * resolution];
        
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++) {
            if (x == resolution) {
                x = 0;
                z += 1;
            }
            Transform point = points[i] = Instantiate(pointPrefab);
            point.localScale = scale;
            position.x = (x + 0.5f) * step - 1f;
            position.z = (z + 0.5f) * step - 1f;
            pos[i] = position;
            point.position = position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;
        Vector3 position;
        for (int i = 0; i < points.Length; i++)
        {
            var func = FunctionLibrary.GetFunction(indFunc);
            Transform point = points[i];
            position = pos[i];
            point.localPosition = func(position.x, position.z,time);
        }
    }
}
