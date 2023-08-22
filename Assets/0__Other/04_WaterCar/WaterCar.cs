using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCar : MonoBehaviour
{
    [SerializeField]
    private Camera main;
    [SerializeField]
    private Camera mirror;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mirror.transform.position = new Vector3(main.transform.position.x, -main.transform.hierarchyCapacity,
            main.transform.position.z);
        mirror.transform.eulerAngles = new Vector3(-main.transform.eulerAngles.x, main.transform.eulerAngles.y,
            -main.transform.eulerAngles.z);
    }
    
    // private void UpdateReflectionCamera(Camera curCamera) {
    //     if (targetPlane == null) {
    //         Debug.LogError("target plane is null!");
    //     }
    //
    //     UpdateCamera(curCamera, _reflectionCamera);  // 同步当前相机数据
    //
    //     // 将相机移转换到平面空间 plane space，再通过平面对称创建反射相机
    //     Vector3 camPosPS = targetPlane.transform.worldToLocalMatrix.MultiplyPoint(curCamera.transform.position);
    //     Vector3 reflectCamPosPS = Vector3.Scale(camPosPS, new Vector3(1, -1, 1)) + new Vector3(0, m_planeOffset, 0);  // 反射相机平面空间
    //     Vector3 reflectCamPosWS = targetPlane.transform.localToWorldMatrix.MultiplyPoint(reflectCamPosPS);  // 将反射相机转换到世界空间
    //     _reflectionCamera.transform.position = reflectCamPosWS;
    //
    //     // 设置反射相机方向
    //     Vector3 camForwardPS = targetPlane.transform.worldToLocalMatrix.MultiplyVector(curCamera.transform.forward);
    //     Vector3 reflectCamForwardPS = Vector3.Scale(camForwardPS, new Vector3(1, -1, 1));
    //     Vector3 reflectCamForwardWS = targetPlane.transform.localToWorldMatrix.MultiplyVector(reflectCamForwardPS); 
    //         
    //     Vector3 camUpPS = targetPlane.transform.worldToLocalMatrix.MultiplyVector(curCamera.transform.up);
    //     Vector3 reflectCamUpPS = Vector3.Scale(camUpPS, new Vector3(-1, 1, -1));
    //     Vector3 reflectCamUpWS = targetPlane.transform.localToWorldMatrix.MultiplyVector(reflectCamUpPS); 
    //     _reflectionCamera.transform.rotation = Quaternion.LookRotation(reflectCamForwardWS, reflectCamUpWS);
    //
    //     // 斜截视锥体
    //     Vector3 planeNormal = targetPlane.transform.up;
    //     Vector3 planePos = targetPlane.transform.position + planeNormal * m_planeOffset;
    //     var clipPlane = CameraSpacePlane(_reflectionCamera, planePos - Vector3.up * 0.1f, planeNormal, 1.0f);
    //     _reflectionCamera.projectionMatrix = projection;
    //     _reflectionCamera.cullingMask = m_settings.m_ReflectLayers; // never render water layer
    // }
}
