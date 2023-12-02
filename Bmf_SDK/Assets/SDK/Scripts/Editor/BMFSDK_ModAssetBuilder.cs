using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;

public class BMFSDK_ModAssetBuilder : EditorWindow
{
    //build assetbundles
    [MenuItem("Bluey Modding/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        //  output path
        string assetBundleDirectory = "Assets/Compiled/ModAssets";
        Directory.CreateDirectory(assetBundleDirectory);

        //  build
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
        AssetDatabase.Refresh();

        //  console notification
        Debug.Log("<color=lime>Assets built! </color>Located under: 'Compiled/ModAssets/'");
    }
}
