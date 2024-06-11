using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MirrorExporter : MonoBehaviour
{
    private static string packagePath = EditorUtility.SaveFilePanel("Save Package", "", "Mirror.unitypackage", "unitypackage");

    [MenuItem("Mirror Exporter/Complete")]
    static void ExportFull()
    {
        if (string.IsNullOrEmpty(packagePath)) return;

        List<string> assetPaths = new List<string>();
        assetPaths.AddRange(GetAllAssetPaths("Assets/Mirror"));
        assetPaths.AddRange(GetAllAssetPaths("Assets/ScriptTemplates"));

        AssetDatabase.ExportPackage(assetPaths.ToArray(), packagePath, ExportPackageOptions.Recurse);
        Debug.Log("Successfully Exported: " + packagePath);
    }

    [MenuItem("Mirror Exporter/Minimal")]
    static void ExportMinimal()
    {
        if (string.IsNullOrEmpty(packagePath)) return;

        List<string> assetPaths = GetAllAssetPaths("Assets/Mirror");

        List<string> exclusions = new List<string>
        {
            "Assets/Mirror/Examples",
            "Assets/Mirror/Hosting",
            "Assets/Mirror/Tests",
            "Assets/Mirror/Transports/Edgegap",
            "Assets/Mirror/Editor/Welcome.cs"
        };

        List<string> filteredAssetPaths = new List<string>();

        foreach (string path in assetPaths)
        {
            bool exclude = false;

            foreach (string exclusion in exclusions)
            {
                if (path.StartsWith(exclusion))
                {
                    exclude = true;
                    break;
                }
            }

            if (!exclude)
            {
                filteredAssetPaths.Add(path);
            }
        }

        AssetDatabase.ExportPackage(filteredAssetPaths.ToArray(), packagePath, ExportPackageOptions.Recurse);
        Debug.Log("Successfully Exported: " + packagePath);
    }

    private static List<string> GetAllAssetPaths(string basePath)
    {
        List<string> assetPaths = new List<string>();
        foreach (string file in Directory.GetFiles(basePath, "*", SearchOption.AllDirectories))
        {
            if (file.EndsWith(".meta")) continue;
            assetPaths.Add(file.Replace('\\', '/'));
        }
        return assetPaths;
    }
}


