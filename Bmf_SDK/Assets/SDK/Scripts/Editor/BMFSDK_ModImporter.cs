using System;
using System.Collections;
using System.Collections.Generic;
using Unity.EditorCoroutines.Editor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

//Assigns icons & descriptions to built assetbundles which vary depending on modasset type (mod, level, item, bmf)

[ScriptedImporter(1, "blueymod")]
public class BMFSDK_ModImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        TextAsset desc = new TextAsset("-   Bluey Mod Asset File   -\n\nDrag into 'ModAssets/' folder and launch game with Bluey Mod Framework to load\n\nAsset Generated on: " + DateTime.Now);
        Texture2D icon = Resources.Load<Texture2D>("ModIcon");

        ctx.AddObjectToAsset(ctx.assetPath, desc, icon);
    }
}

[ScriptedImporter(1, "blueylevel")]
public class BMFSDK_LevelImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        TextAsset desc = new TextAsset("-   Bluey Custom Level   -\n\nDrag into 'ModAssets/CustomLevels/' folder and launch game with Bluey Mod Framework to load\n\nAsset Generated on: " + DateTime.Now);
        Texture2D icon = Resources.Load<Texture2D>("LevelIcon");

        ctx.AddObjectToAsset(ctx.assetPath, desc, icon);

        //EditorCoroutineUtility.StartCoroutine(BMFSDK_MoveAsset.moveAsset(ctx.assetPath, Application.dataPath + "/Compiled/ModAssets/CustomLevels/"), this);
    }
}

[ScriptedImporter(1, "blueyitem")]
public class BMFSDK_ItemImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        TextAsset desc = new TextAsset("-   Bluey Custom Item   -\n\nDrag into 'ModAssets/CustomItems/' folder and launch game with Bluey Mod Framework to load\n\nAsset Generated on: " + DateTime.Now);
        Texture2D icon = Resources.Load<Texture2D>("ItemIcon");

        ctx.AddObjectToAsset(ctx.assetPath, desc, icon);

        //EditorCoroutineUtility.StartCoroutine(BMFSDK_MoveAsset.moveAsset(ctx.assetPath, Application.dataPath + "/Compiled/ModAssets/CustomItems/"), this);
    }
}

//  for building Bmf_Assets to "modframework.bmf"
[ScriptedImporter(1, "bmf")]
public class BMFSDK_BmfImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        TextAsset desc = new TextAsset("-   Bluey Modding Framework Asset   -\n\nPrimarily for internal use, filtered out of other ModAssets, and initialized first\n\nDrag into 'ModAssets/' folder and launch game with Bluey Mod Framework to load\n\nAsset Generated on: " + DateTime.Now);
        Texture2D icon = Resources.Load<Texture2D>("BmfIcon");

        ctx.AddObjectToAsset(ctx.assetPath, desc, icon);
    }
}


//UNUSED
//move built assetbundles into subfolders depending on type
public class BMFSDK_MoveAsset : Editor
{
    public static IEnumerator moveAsset(string assetPath, string newPath)
    {
        yield return !AssetDatabase.IsAssetImportWorkerProcess();

        Debug.Log("Moved!");

        AssetDatabase.MoveAsset(assetPath, newPath);
        //FileUtil.MoveFileOrDirectory(assetPath, newPath);
        AssetDatabase.Refresh();
        yield return null;
    }
}