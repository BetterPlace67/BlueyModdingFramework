using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditorInternal;
using UnityEngine;


//compiles scripts into dlls and moves them into project for easy access
public class BMFSDK_ScriptCompiler : EditorWindow
{
    //start
    [MenuItem("Bluey Modding/Compile Scripts")]
    public static void ShowWindow()
    {
        //datapath outside of project file (in %AppData%)
        string compilePath = Application.persistentDataPath + "/CompiledMods";
        
        //compile scripts as if building project
        UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation();
        BuildPipeline.BuildPlayer(new string[0], compilePath + "/scripts.exe", BuildTarget.StandaloneWindows, BuildOptions.BuildScriptsOnly);
        //open window (to call OnEnable)
        EditorWindow.GetWindow(typeof(BMFSDK_ScriptCompiler));
    }

    //starting coroutines
    public void OnEnable()
    {
        //datapath outside of project file (in %AppData%)
        string compilePath = Application.persistentDataPath + "/CompiledMods";
        //await script compilation
        EditorCoroutineUtility.StartCoroutine(AwaitBuild(compilePath), this);
    }

    //Move dlls into project
    public IEnumerator AwaitBuild(string compilePath)
    {
        //await
        yield return !BuildPipeline.isBuildingPlayer;

        //datapath within project file
        DirectoryInfo dir = new DirectoryInfo(compilePath + "/scripts_data/Managed/");
        //get all files within external %AppData% dll build path
        FileInfo[] info = dir.GetFiles("*.*");
        foreach (FileInfo f in info) { 
            //ignore unity player & common dependancy dlls
            if (f.Name == "Assembly-CSharp.dll" || f.Name == "BMF_Assembly.dll" || f.Name == "FbxBuildTestAssets.dll" || f.Name == "FMODUnityResonance.dll" || f.Name == "Assembly-CSharp-firstpass.dll" || f.Name == "BehaviorDesigner.Runtime.dll" || f.Name == "Coffee.UIParticle.dll" || f.Name == "Coffee.UnmaskForUGUI.dll" || f.Name == "com.rlabrecque.steamworks.net.dll" || f.Name == "NavMeshComponents.dll" || f.Name == "Sirenix.OdinInspector.Attributes.dll" || f.Name == "YamlDotNet.dll" || f.Name == "mscorlib.dll" || f.Name == "netstandard.dll" || f.Name == "Cinemachine.dll" || f.Name == "Autodesk.Fbx.dll" || f.Name.Contains("Mono.") || f.Name.Contains("System.") || f.Name.Contains("spine-") || f.Name.Contains("Unity.") || f.Name.Contains("UnityEngine.")) {
            } else {
                //check folder path exists, if not, create
                if (!Directory.Exists(Application.dataPath + "/Compiled/Plugins"))
                    Directory.CreateDirectory(Application.dataPath + "/Compiled/Plugins");
                f.MoveTo(Application.dataPath + "/Compiled/Plugins/" + f.Name); //move
            }
        }

        AssetDatabase.Refresh();

        //close editorGuiWindow before it becomes visible (only needed for coroutine)
        this.Close();
        //output to console
        Debug.Log("<color=lime>Scripts compiled! </color>Located under: 'Compiled/Plugins/'");

        yield return null;
    }

    //ideally unseen by user, but if the editor somehow repaints during compilation, this exists
    private void OnGUI()
    {
        GUILayout.Label("Compiling scripts... please wait");
    }
}
