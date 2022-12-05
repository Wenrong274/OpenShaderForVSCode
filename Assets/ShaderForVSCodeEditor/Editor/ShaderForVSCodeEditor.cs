using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ShaderForVSCodeEditor
{
    [UnityEditor.Callbacks.OnOpenAsset(0)]
    public static bool CallbackShader(int instanceID, int line)
    {
        string projectPath = Directory.GetParent(Application.dataPath).ToString();
        string strFilePath = AssetDatabase.GetAssetPath(EditorUtility.InstanceIDToObject(instanceID));
        string fileName = projectPath + "/" + strFilePath;
        if (fileName.EndsWith(".shader"))
        {
            var envUser = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.User);
            var envPaths = envUser["Path"].ToString().Split(";");
            string vscodePath = string.Empty;
            for (int i = 0; i < envPaths.Length; i++)
            {
                var path = Path.Combine(envPaths[i], "code");
                if (File.Exists(path))
                {
                    vscodePath = path;
                    break;
                }
            }
            if (!string.IsNullOrEmpty(vscodePath))
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = vscodePath;
                ///vscode args "$(ProjectPath)" -g "$(File)":$(Line):$(Column)
                startInfo.Arguments = $"{projectPath} -g {fileName}";
                process.StartInfo = startInfo;
                process.Start();
                return true;
            }
            else
            {
                UnityEngine.Debug.Log("Not Found Enviroment Variable 'VSCode_Path'.");
                return false;
            }
        }
        return false;
    }
}
