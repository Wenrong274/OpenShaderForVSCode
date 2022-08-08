using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class OpenShaderForVSCodeEditor
{
    [UnityEditor.Callbacks.OnOpenAsset(0)]
    public static bool CallbackShader(int instanceID, int line)
    {
        string projectPath = Directory.GetParent(Application.dataPath).ToString();
        string strFilePath = AssetDatabase.GetAssetPath(EditorUtility.InstanceIDToObject(instanceID));
        string strFileName = projectPath + "/" + strFilePath;
        if (strFileName.EndsWith(".shader"))
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
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = vscodePath;
                ///vscode args "$(ProjectPath)" -g "$(File)":$(Line):$(Column)
                startInfo.Arguments = $"{projectPath} -g {strFileName}";
                process.StartInfo = startInfo;
                process.Start();
                return true;
            }
            else
            {
                Debug.Log("Not Found Enviroment Variable 'VSCode_Path'.");
                return false;
            }
        }
        return false;
    }
}
