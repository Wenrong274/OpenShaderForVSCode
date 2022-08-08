# OpenShaderForVSCode

真對 Unity Shader 檔案打開時，使用 VSCode 開啟而不是 Visual Studio，假如預設是 VSCode 則無需使用這功能。

會寫這功能是平常寫 C# 都是習慣使用 Visual Studio，而 Visual Studio 好像沒有針對 Unity Shaderlab 的關鍵字，而 VSCode 則有 [ShaderlabVSCode(Free)][shaderlab_vscode]，也因此這樣選擇使用 VSCode。

因為平常開發時都是使用 Visual Studio，想開啟 Shader 時可以直接使用 VSCode 編輯，因此才參考 [Sublime Text & Unity Shader][ref_1]，把 Sublime Text 改成使用 VSCode。

## 環境變數

需要注意`環境變數`裡的使用者變數的 `Path` 需要有 VSCode 的路徑

![env_1]

也可以使用 CMD 測試有無環境變數

![env_2]

## VSCode CLI Args

根據 Unity 開啟 VSCode Args，可以使用 `Process` 填寫對應路徑就可以了。

```text
"$(ProjectPath)" -g "$(File)":$(Line):$(Column)
```

``` C#
startInfo.Arguments = $"{projectPath} -g {fileName}";
```

## [GitHub][github]

_________________________________________________________________________

[shaderlab_vscode]:https://marketplace.visualstudio.com/items?itemName=amlovey.shaderlabvscodefree

[ref_1]:https://blog.csdn.net/weixin_44293055/article/details/120340635

[env_1]:https://imgur.com/MZN9Wgs.jpg
[env_2]:https://imgur.com/ME4qXZs.jpg
[github]:https://github.com/Wenrong274/OpenShaderForVSCode
