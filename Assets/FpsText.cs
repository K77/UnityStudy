/// <summary>
/// Set FPS
/// this script use to set FPS if platform is mobile
/// </summary>

using System;
using UnityEngine;

public class FpsText : MonoBehaviour
{
    public float updateInterval = 0.5f;
    private float lastInterval;
    private float lastFrameTime;
    private int frames = 0;
    private float fps;
    private float maxTimeFrameCost;

    private int displayFrameCost;

    private string uuid;

    void Start()
    {
        Application.targetFrameRate = 60;
        lastInterval = lastFrameTime = Time.realtimeSinceStartup;
        frames = 0;
    }

    // Update is called once per frame  
    void Update()
    {
        ++frames;
        float timeNow = Time.realtimeSinceStartup;

        // Max time cost of frame drawing
        float timeCostThisFrame = timeNow - lastFrameTime;
        lastFrameTime = timeNow;
        if (timeCostThisFrame > maxTimeFrameCost)
        {
            maxTimeFrameCost = timeCostThisFrame;
        }

        // Fps
        if (timeNow >= lastInterval + updateInterval)
        {
            displayFrameCost = (int) (maxTimeFrameCost * 1000);
            maxTimeFrameCost = 0;

            fps = frames / (timeNow - lastInterval);
            frames = 0;
            lastInterval = timeNow;
        }
    }

    void OnGUI()
    {
//        Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.fontSize = 32;
        labelStyle.normal.textColor = Color.black;
        
        GUI.Label(new Rect(240, 50, 1000, 120), 
            $"FPS: {fps} {Environment.NewLine}" +
            $"MaxCost: {(displayFrameCost)} ms {Environment.NewLine}"
            , labelStyle);
    }
}