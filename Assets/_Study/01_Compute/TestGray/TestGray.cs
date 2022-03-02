using System;
using UnityEngine;
using UnityEngine.UI;

public class TestGray : MonoBehaviour 
{
    public Texture2D inputTexture;
    public RawImage outputImage;
    public ComputeShader shader;

    void Start ()
    {
        RenderTexture t = new RenderTexture (inputTexture.width, inputTexture.height, 24);
        t.enableRandomWrite = true;
        t.Create ();
        outputImage.texture = t;

        int k = shader.FindKernel ("CSMain");
        shader.SetTexture (k, "inputTexture", inputTexture);
        shader.SetTexture (k, "outputTexture", t);
        shader.Dispatch (k, inputTexture.width, inputTexture.height, 1);
    }

    void test()
    {
        Texture2D texture2D =
            new Texture2D(128, 128, TextureFormat.ARGB32, false);
        for (int x = 0; x < 128; x++)
        {
            for (int y = 0; y < 128; y++)
            {
                texture2D.SetPixel(x, y, Color.red);
            }
        }
        texture2D.Apply();
        outputImage.texture = texture2D;
    }
    void test1 ()
    {
        Texture2D texture2D =
            new Texture2D(inputTexture.width, inputTexture.height, TextureFormat.ARGB32, false);

        int k = shader.FindKernel ("CSMain");
        shader.SetTexture (k, "inputTexture", inputTexture);
        shader.SetTexture (k, "outputTexture", texture2D);
        shader.Dispatch (k, inputTexture.width, inputTexture.height, 1);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,100,100),""))
        {
            test1();
        }
    }
}
