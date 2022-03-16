using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;

public class CtrGyroscope : MonoBehaviour
{
    // Ball ball;
    bool isHasGyro = false;
    // GUItext infoMsg;
    float x;
    float y;
    float z;
    Vector3 Force;

    void Start(){
        if (SystemInfo.supportsGyroscope){  // 检查手机是否有提供陀螺仪功能
            Input.gyro.enabled = true;  // 启用陀螺仪
            isHasGyro = true;
            Debug.Log("Using Gyroscope");
        }
        else{
            Debug.Log("Not using Gyroscope");
        }
    }

    private float tmp = 0;
    void Update()
    {
        tmp += Time.deltaTime;
        
        if (tmp<1) return;
        tmp -= 1;
        if (isHasGyro){
            x = Input.gyro.attitude.x;
            y = Input.gyro.attitude.y;
            z = Input.gyro.attitude.z;  // 读取陀螺仪的值
            // Debug.Log("Using Gyroscope : "+ x +" , "+ y +" , "+ z);
            Debug.Log("Using Gyroscope : "+ Input.gyro.attitude.eulerAngles);
            // Debug.Log("gyro.updateInterval: "+ Input.gyro.updateInterval);
            
            // Input.gyro.gravity.ToString();
            // Input.gyro.

            // force = new Vector3(-x * 10.0F , 0.0F , -z * 10.0F);
        }
        // ball.forward(force);
    }
}
