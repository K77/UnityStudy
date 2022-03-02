using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Unity Editor 下右键创建文本类文件
/// </summary>
public class CreateFileEditor : Editor
{
    [MenuItem("Assets/Create/Lua File")]
    static void CreateLuaFile()
    {
        CreateFile("lua");
    }

    [MenuItem("Assets/Create/Text File")]
    static void CreateTextFile()
    {
        CreateFile("txt");
    }
    [MenuItem("Assets/Create/Shader/UrpShader %#u",false,0)]
    static void CreateUrpFile()
    {
        CreateFile("shader");
    }
    /// <summary>
    /// 创建文件类的文件
    /// </summary>
    /// <param name="fileEx"></param>
    static void CreateFile(string fileEx)
    {
        //获取当前所选择的目录（相对于Assets的路径）
        var selectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        var path = Application.dataPath.Replace("Assets", "") + "/";
        var newFileName = "new_" + fileEx + "." + fileEx;
        var newFilePath = selectPath + "/" + newFileName;
        var fullPath = path + newFilePath;

        //简单的重名处理
        if (File.Exists(fullPath))
        {
            var newName = "new_" + fileEx + "-" + UnityEngine.Random.Range(0, 100) + "." + fileEx;
            newFilePath = selectPath + "/" + newName;
            fullPath = fullPath.Replace(newFileName, newName);
        }

        if (fileEx == "shader")
        {
            TextAsset text = 	EditorGUIUtility.Load("UrpLit.txt")as TextAsset;
            File.WriteAllText(fullPath,text.text,Encoding.UTF8);
        }
        else
        {
            File.WriteAllText(fullPath, "-- test", Encoding.UTF8);
        }
        //如果是空白文件，编码并没有设成UTF-8
        

        AssetDatabase.Refresh();

        //选中新创建的文件
        var asset = AssetDatabase.LoadAssetAtPath(newFilePath, typeof(Object));
        Selection.activeObject = asset;
    }
}