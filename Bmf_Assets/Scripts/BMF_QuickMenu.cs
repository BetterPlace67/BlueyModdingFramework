//using BlueyModFramework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BMF_QuickMenu : MonoBehaviour
{
    [SerializeField] Transform itemRoot, levelRoot;
    [SerializeField] GameObject button;

    FileInfo[] cachedLevels, cachedItems;

    private void OnEnable()
    {
        Debug.Log("Quick Menu Opened!");
        RefreshItems();
        RefreshLevels();
    }

    public void RefreshSkins()
    {
        Debug.Log("Refreshing skin textures...");
        //CustomSkins.FetchSkins();
    }

    public void RefreshLevels()
    {
        Debug.Log("Fetching level list...");

        //clear list
        DestroyChildren(levelRoot);
        //get all assets
        var info = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(Application.dataPath), "ModAssets/CustomLevels"));
        var fileInfo = info.GetFiles();

        Debug.Log("Found " + fileInfo.Length + " canidates...");

        //create buttons
        for (int i = 0; i < fileInfo.Length; i++) {
            if (fileInfo[i].Extension == "blueylevel") {
                BMF_UiButton levelButton = Instantiate(button, levelRoot, false).GetComponent<BMF_UiButton>();
                levelButton.text.text = fileInfo[i].Name;
                levelButton.index = i;
                levelButton.menu = this;
                levelButton.isLevel = true;
            }
        }
        //cache results
        cachedLevels = fileInfo;
    }

    public void RefreshItems()
    {
        //clear list
        DestroyChildren(itemRoot);
        //get all assets
        var info = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(Application.dataPath), "ModAssets/CustomItems"));
        var fileInfo = info.GetFiles();
        //create buttons
        for (int i = 0; i < fileInfo.Length; i++)
        {
            if (fileInfo[i].Extension == "blueyitem") {
                BMF_UiButton itemButton = Instantiate(button, itemRoot, false).GetComponent<BMF_UiButton>();
                itemButton.text.text = fileInfo[i].Name;
                itemButton.index = i;
                itemButton.menu = this;
                itemButton.isLevel = false;
            }
        }
        //cache results
        cachedItems = fileInfo;
    }

    public void LoadLevel(int level)
    {
        AssetBundle bundle = AssetBundle.LoadFromFile(cachedLevels[level].Directory.FullName);

        bundle.LoadAllAssets();

        string[] scenePath = bundle.GetAllScenePaths();

        GameObject loadScreen = GameObject.Find("SavingAnimation").transform.parent.GetChild(1).gameObject;

        StartCoroutine(LoadScene(scenePath[0], loadScreen));

        SceneManager.UnloadSceneAsync(4);
        SceneManager.UnloadSceneAsync(5);
        SceneManager.UnloadSceneAsync(6);
        SceneManager.UnloadSceneAsync(7);
        
        //SceneManager.LoadScene(scenePath[0]);
    }

    private IEnumerator LoadScene(string sceneName, GameObject loadScreen)
    {
        var asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone)
        {
            loadScreen.SetActive(true);
            //Debug.Log("Loading the Scene");
            yield return null;
        }

        loadScreen.SetActive(false);
        //LoadScene?.Invoke(newSceneName);
    }

    public void GiveItem(int item)
    {

    }


    void DestroyChildren(Transform root)
    {
        foreach (Transform child in root)
        {
            Destroy(child.gameObject);
        }
    }
}
