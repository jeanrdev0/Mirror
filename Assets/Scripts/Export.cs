using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Export : MonoBehaviour
{
    [MenuItem("Export/Full")]
    static void ExportFull()
    {
        string packagePath = EditorUtility.SaveFilePanel("Save Package", "", "Mirror VERSION.unitypackage", "unitypackage");

        if (string.IsNullOrEmpty(packagePath))
        {
            Debug.Log("Package export canceled.");
            return;
        }

        string[] assetPaths = new string[]
        {
            "Assets/Mirror",
            "Assets/ScriptTemplates"
        };

        AssetDatabase.ExportPackage(assetPaths, packagePath, ExportPackageOptions.Recurse);
        Debug.Log("Package successfully exported: " + packagePath);
    }

    [MenuItem("Export/Minimal")]
    static void ExportMinimal()
    {
        string packagePath = EditorUtility.SaveFilePanel("Save Package", "", "Mirror VERSION.unitypackage", "unitypackage");

        if (string.IsNullOrEmpty(packagePath))
        {
            Debug.Log("Package export canceled.");
            return;
        }

        List<string> assetPaths = new List<string>
        {
            "Assets/Mirror"
        };

        List<string> exclusions = new List<string>
        {
            "Assets/Mirror\\Examples",
            "Assets/Mirror\\Hosting",
            "Assets/Mirror\\Tests",
            "Assets/Mirror\\Transports\\Edgegap"
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

        Debug.Log("Package successfully exported: " + packagePath);
    }

}
