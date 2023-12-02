//using BlueyModFramework;
using BlueyModdingFramework;
using BlueyModFramework;
using Gandalf;
using MelonLoader.TinyJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BMF_QuickMenu : MonoBehaviour
{
    [SerializeField] public Transform itemRoot, levelRoot, settingsRoot, bundleManagerRoot;
    [SerializeField] public GameObject button;

    public BMF_Init bmfInit;

    FileInfo[] cachedLevels, cachedItems;

    int fullscreen;

    public string customSceneName = "";
    SceneLoaderManager loader;

    public MeshRenderer[] skinPreviews = new MeshRenderer[8];

    List<ModAsset> loadedItems = new List<ModAsset>();
    List<ModAsset> loadedLevels = new List<ModAsset>();

    /*private void OnEnable()
    {
        Debug.Log("Quick Menu Opened!");
        RefreshItems();
        RefreshLevels();
    }*/

    public struct ModAsset
    {
        public string assetPath, loadTime;
        public AssetBundle bundle;
    }

    void OnDisable()
    {
        bmfInit.menuOpen = false;
        bmfInit.ToggleCursorVisibility(false);
    }


    void Start()
    {
        //create custom scene loader
        GameManager loaderRoot = GameObject.FindObjectOfType<GameManager>();
        GameObject load1 = loaderRoot.transform.GetChild(0).GetChild(1).gameObject;
        GameObject load2 = loaderRoot.transform.GetChild(0).GetChild(1).gameObject;
        GameObject load3 = loaderRoot.transform.GetChild(0).GetChild(1).gameObject;

        loader = new SceneLoaderManager(load1, load2, load3);
        loader.Initialize();

        //setup buttons
        settingsRoot.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => Setting_OpenStickerBook()); //sticker book
        settingsRoot.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(() => Setting_EndEpisode()); //end episode
        settingsRoot.GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(() => Setting_ToggleFullScreen()); //toggle fullscreen mode
        settingsRoot.GetChild(5).gameObject.GetComponent<Button>().onClick.AddListener(() => RefreshSkins()); //refresh skins
        settingsRoot.GetChild(6).gameObject.GetComponent<Button>().onClick.AddListener(() => RefreshItems()); //refresh items
        settingsRoot.GetChild(7).gameObject.GetComponent<Button>().onClick.AddListener(() => RefreshLevels()); //refresh levels
        settingsRoot.GetChild(9).gameObject.GetComponent<Button>().onClick.AddListener(() => Setting_GetLoadedContent()); //refresh loaded content on page enter
        bundleManagerRoot.GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(() => Setting_UnloadAllLevels()); //unload levels
        bundleManagerRoot.GetChild(5).gameObject.GetComponent<Button>().onClick.AddListener(() => Setting_UnloadAllItems()); //unload items
        bundleManagerRoot.GetChild(6).gameObject.GetComponent<Button>().onClick.AddListener(() => Setting_GetLoadedContent()); //refresh loaded content

        skinPreviews[0] = settingsRoot.GetChild(8).GetChild(0).gameObject.GetComponent<MeshRenderer>(); //bluey
        skinPreviews[1] = settingsRoot.GetChild(8).GetChild(1).gameObject.GetComponent<MeshRenderer>(); //bingo
        skinPreviews[2] = settingsRoot.GetChild(8).GetChild(2).gameObject.GetComponent<MeshRenderer>(); //bandit
        skinPreviews[3] = settingsRoot.GetChild(8).GetChild(3).gameObject.GetComponent<MeshRenderer>(); //chilli
        skinPreviews[4] = settingsRoot.GetChild(8).GetChild(4).gameObject.GetComponent<MeshRenderer>(); //rad
        skinPreviews[5] = settingsRoot.GetChild(8).GetChild(5).gameObject.GetComponent<MeshRenderer>(); //stripe
        skinPreviews[6] = settingsRoot.GetChild(8).GetChild(6).gameObject.GetComponent<MeshRenderer>(); //muffin
        skinPreviews[7] = settingsRoot.GetChild(8).GetChild(7).gameObject.GetComponent<MeshRenderer>(); //mort

        BMF_ControllerInput control_main = transform.GetChild(0).GetChild(0).gameObject.AddComponent<BMF_ControllerInput>();
        control_main.backToDisable = gameObject;
        control_main.toFocus = control_main.transform.GetChild(1).gameObject;

        BMF_ControllerInput control_item = transform.GetChild(0).GetChild(1).gameObject.AddComponent<BMF_ControllerInput>();
        control_item.backToDisable = control_item.gameObject;
        control_item.backToEnable = control_main.gameObject;
        control_item.toFocus = control_item.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;

        BMF_ControllerInput control_level = transform.GetChild(0).GetChild(2).gameObject.AddComponent<BMF_ControllerInput>();
        control_level.backToDisable = control_level.gameObject;
        control_level.backToEnable = control_main.gameObject;
        control_level.toFocus = control_level.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;

        BMF_ControllerInput control_setting = transform.GetChild(0).GetChild(3).gameObject.AddComponent<BMF_ControllerInput>();
        control_setting.backToDisable = control_setting.gameObject;
        control_setting.backToEnable = control_main.gameObject;
        control_setting.toFocus = control_setting.transform.GetChild(2).gameObject;

        BMF_ControllerInput control_manager = transform.GetChild(0).GetChild(4).gameObject.AddComponent<BMF_ControllerInput>();
        control_manager.backToDisable = control_manager.gameObject;
        control_manager.backToEnable = control_main.gameObject;
        control_manager.toFocus = control_manager.transform.GetChild(6).gameObject;

        RefreshSkins(); //so initial previews are accurate
    }

    public void RefreshSkins()
    {
        Debug.Log("Refreshing skin textures...");
        //CustomSkins.FetchSkins();
        BMF_SkinManager.callback.RefreshSkins();

        StartCoroutine(DelayedSkinPreview());
    }

    public void RefreshLevels()
    {
        Debug.Log("Fetching level list...");
        StartCoroutine(disableDebugObj());

        //clear list
        DestroyChildren(levelRoot);
        //get all assets
        var info = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(Application.dataPath), "ModAssets/CustomLevels"));
        var fileInfo = info.GetFiles();

        Debug.Log("Found " + fileInfo.Length + " canidate(s)...");

        //create buttons
        for (int i = 0; i < fileInfo.Length; i++)
        {
            if (fileInfo[i].Extension == ".blueylevel")
            {
                Debug.Log(fileInfo[i].Name + " accepted. Index: " + i);
                BMF_UiButton levelButton = Instantiate(button, levelRoot, false).AddComponent<BMF_UiButton>();
                levelButton.text = levelButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
                levelButton.text.text = fileInfo[i].Name.Substring(0, fileInfo[i].Name.Length - fileInfo[i].Extension.Length);
                levelButton.index = i;
                levelButton.menu = this;
                levelButton.isLevel = true;
                if (i == 0)
                    EventSystem.current.SetSelectedGameObject(levelButton.gameObject);
                    levelButton.transform.parent.parent.parent.parent.gameObject.GetComponent<BMF_ControllerInput>().toFocus = levelButton.gameObject;

                levelButton.GetComponent<Button>().onClick.AddListener(() => levelButton.GetComponent<BMF_UiButton>().UseButton()); //() => levelButton.buttonEvent.Invoke()
            } else {
                Debug.Log(fileInfo[i].Name + " rejected, unwanted file extension: " + fileInfo[i].Extension);
            }
        }
        //cache results
        cachedLevels = fileInfo;
    }

    IEnumerator disableDebugObj()
    {
        yield return new WaitForSeconds(1);
        yield return null;
    }

    public void RefreshItems()
    {
        Debug.Log("Fetching item list...");

        //clear list
        DestroyChildren(itemRoot);
        //get all assets
        var info = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(Application.dataPath), "ModAssets/CustomItems"));
        var fileInfo = info.GetFiles();

        Debug.Log("Found " + fileInfo.Length + " canidate(s)...");

        //create buttons
        for (int i = 0; i < fileInfo.Length; i++)
        {
            if (fileInfo[i].Extension == ".blueyitem")
            {
                Debug.Log(fileInfo[i].Name + " accepted. Index: " + i);
                BMF_UiButton itemButton = Instantiate(button, itemRoot, false).AddComponent<BMF_UiButton>();
                itemButton.text = itemButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
                itemButton.text.text = fileInfo[i].Name.Substring(0, fileInfo[i].Name.Length - fileInfo[i].Extension.Length);
                itemButton.index = i;
                itemButton.menu = this;
                itemButton.isLevel = false;
                if (i == 0)
                    EventSystem.current.SetSelectedGameObject(itemButton.gameObject);
                    itemButton.transform.parent.parent.parent.parent.gameObject.GetComponent<BMF_ControllerInput>().toFocus = itemButton.gameObject;

                itemButton.GetComponent<Button>().onClick.AddListener(() => itemButton.GetComponent<BMF_UiButton>().UseButton()); //() => GiveItem(i)
            } else {
                Debug.Log(fileInfo[i].Name + " rejected, unwanted file extension: " + fileInfo[i].Extension);
            }
        }
        //cache results
        cachedItems = fileInfo;
    }

    public void LoadLevel(int level)
    {

        Debug.Log("Requested lvl " + level + " / " + cachedLevels.Length);

        string assetPath = Path.Combine(Path.GetDirectoryName(Application.dataPath), "ModAssets/CustomLevels/") + cachedLevels[level].Name;

        Debug.Log("Loading Level: \n" + assetPath);


        AssetBundle bundle = null;
        //allows multiple of same asset to be loaded
        foreach (ModAsset asset in loadedItems)
        {
            if (asset.assetPath == assetPath)
            {
                Debug.Log("ModAsset already loaded... Reusing content");
                bundle = asset.bundle;
            }
        }
        if (bundle == null)
        {
            Debug.Log("ModAsset loaded for the first time...");
            bundle = AssetBundle.LoadFromFile(assetPath);

            ModAsset modAsset = new ModAsset();
            modAsset.assetPath = assetPath;
            modAsset.bundle = bundle;
            modAsset.loadTime = System.DateTime.Now.ToString();
            loadedLevels.Add(modAsset);
        }
        //bundle.isStreamedSceneAssetBundle

        //bundle.LoadAllAssets();

        Debug.Log(bundle);

        string[] scenePath = bundle.GetAllScenePaths();

        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath[0]);

        Debug.Log("SceneAsset: " + sceneName);

        GameObject loadScreen = GameObject.Find("SavingAnimation").transform.parent.GetChild(1).gameObject;

        loader.LoadScene(sceneName, global::EnumClass.ScreenLoader.LoadingScreen, true, LoadSceneMode.Additive, true, EnumClass.DayTime.Morning);

        //StartCoroutine(LoadScene(sceneName, loadScreen));
        StartCoroutine(AwaitScene(scenePath[0]));

        if (customSceneName != null && customSceneName != "" && customSceneName != System.IO.Path.GetFileNameWithoutExtension(scenePath[0])) {
            loader.UnLoadScene(customSceneName);
            loadedLevels[0].bundle.Unload(true);
            loadedLevels.RemoveAt(0);
            //SceneManager.UnloadSceneAsync(customSceneName);
        }

        customSceneName = sceneName;
        SceneManager.UnloadSceneAsync(4);
        SceneManager.UnloadSceneAsync(5);
        SceneManager.UnloadSceneAsync(6);
        SceneManager.UnloadSceneAsync(7);

        //SceneManager.LoadScene(scenePath[0]);
    }

    //suboptimal
    private IEnumerator DelayedSkinPreview()
    {
        yield return new WaitForSeconds(2);

        for (int i = 0; i <= 7; i++) {
            skinPreviews[i].material.mainTexture = BMF_SkinManager.callback.AtlasMats[i].mainTexture;
        }
/*
        skinPreviews[0].material.mainTexture = BMF_SkinManager.callback.AtlasMats[0].mainTexture;
        skinPreviews[1].material.mainTexture = BMF_SkinManager.callback.AtlasMats[1].mainTexture;
        skinPreviews[2].material.mainTexture = BMF_SkinManager.callback.AtlasMats[2].mainTexture;
        skinPreviews[3].material.mainTexture = BMF_SkinManager.callback.AtlasMats[3].mainTexture;
        skinPreviews[4].material.mainTexture = BMF_SkinManager.callback.AtlasMats[4].mainTexture;
        skinPreviews[5].material.mainTexture = BMF_SkinManager.callback.AtlasMats[5].mainTexture;
        skinPreviews[6].material.mainTexture = BMF_SkinManager.callback.AtlasMats[6].mainTexture;
        skinPreviews[7].material.mainTexture = BMF_SkinManager.callback.AtlasMats[7].mainTexture;*/

        yield return null;
    }

    private IEnumerator AwaitScene(string sceneName)
    {
        Debug.Log("Waiting...");
        yield return !loader.IsLoadingScene();

        /*while (loader.IsLoadingScene()) {
            yield return null;
        }*/

        GameplayManager manager = GameObject.FindAnyObjectByType<GameplayManager>();

        Debug.Log("GameplayManager: " + manager);

        manager.RemoveLevelManager();

        yield return new WaitForSecondsRealtime(3);

        /*foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {
            {
                Debug.Log(obj.name);
            }
        }*/

        var sceneGos = FindObjectsOfType<ProxyLevelManager>();

        ProxyLevelManager proxy = null;

        Debug.Log("Searching for proxy manager");;

        foreach (var go in sceneGos) {
            //Debug.Log(go.name);

            ProxyLevelManager lvl = null;
            lvl = go.GetComponent<ProxyLevelManager>();
            if (lvl != null)
                proxy = lvl; break;
        }

        proxy = ProxyLevelManager.callback;

        Debug.Log("Proxy: " + proxy);
        if (proxy != null) {
            proxy.gameObject.AddComponent<BMF_PlayerTp>();
            LevelManager proper = proxy.gameObject.AddComponent<LevelManager>();

            Debug.Log("Proper: " + proper);

            proper.m_listTpScenes.Clear();

            TpPlayersPositions tpPos = new TpPlayersPositions();
            tpPos.m_scene = EnumClass.Scenes.HeelerHouse;
            foreach (GameObject position in proxy.m_listTpScenes[0].m_positionsToTpPlayers) {
                tpPos.m_positionsToTpPlayers.Add(position);
            }

            proper.m_listTpScenes.Add(tpPos);
            Debug.Log("Custom Level Loaded!");


            Debug.Log("Level Manager: " + proper);
            manager.AddLevelManager(proper);
            manager.TeleportPlayersToSpawnPoints();
        }
    }

    private IEnumerator LoadScene(string sceneName, GameObject loadScreen)
    {
        loadScreen.SetActive(true);
        var asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!asyncLoadLevel.isDone)
        {
            loadScreen.SetActive(true);
            //Debug.Log("Loading the Scene");
            yield return null;
        }

        loadScreen.SetActive(false);
        GameplayManager manager = GameObject.FindAnyObjectByType<GameplayManager>();
        //manager.sce
        manager.TeleportPlayersToScene(EnumClass.Scenes.HeelerHouse);
        manager.RemoveLevelManager();
        manager.AddLevelManager(GameObject.FindAnyObjectByType<LevelManager>());
        manager.TeleportPlayersToSpawnPoints();
        //LoadScene?.Invoke(newSceneName);
    }

    public void GiveItem(int item)
    {
        string assetPath = Path.Combine(Path.GetDirectoryName(Application.dataPath), "ModAssets/CustomItems/") + cachedItems[item].Name;
        Debug.Log("Loading Item: \n" + assetPath);
        
        AssetBundle bundle = null;
        //allows multiple of same asset to be loaded
        foreach (ModAsset asset in loadedItems) {
            if (asset.assetPath == assetPath) {
                Debug.Log("ModAsset already loaded... Reusing content");
                bundle = asset.bundle;
            }
        }
        if (bundle == null) {
            Debug.Log("ModAsset loaded for the first time...");
            bundle = AssetBundle.LoadFromFile(assetPath);

            ModAsset modAsset = new ModAsset();
            modAsset.assetPath = assetPath;
            modAsset.bundle = bundle;
            modAsset.loadTime = System.DateTime.Now.ToString();
            loadedItems.Add(modAsset);
        }

        string[] assets = bundle.GetAllAssetNames();
        foreach (string asset in assets) {
            var itemProxy = bundle.LoadAsset<BlueyItemProxy>(asset);

            GameObject itemObj = GameObject.Instantiate(itemProxy.itemPrefab, null, true);
            itemObj.transform.position = Camera.main.transform.position + (Camera.main.transform.TransformDirection(Vector3.forward) * 2.5f);

            itemObj.layer = 19; //RoomSpawnable

            itemObj.AddComponent<Rigidbody>();
            BoxCollider collider = itemObj.AddComponent<BoxCollider>();
            collider.size = itemProxy.colliderBounds;
            collider.center = itemProxy.colliderOffset;

            SphereCollider trigger = itemObj.AddComponent<SphereCollider>();
            trigger.isTrigger = true;
            trigger.radius = itemProxy.pickupRadius;

            PickableItem ITEM = itemObj.AddComponent<PickableItem>();
            //ITEM.m_action = itemProxy.m_action;
            
        }
    }


    void DestroyChildren(Transform root)
    {
        foreach (Transform child in root)
        {
            Destroy(child.gameObject);
        }
    }

    public void Setting_GetLoadedContent()
    {
        string combinedString = "<color=white>-------------- Mods --------------</color>";
        combinedString += getAssetsFromList(bmfInit.loadedMods);
        combinedString += "\n<color=white>-------------- Items --------------</color>";
        combinedString += getAssetsFromList(loadedItems);
        combinedString += "\n<color=white>-------------- Levels --------------</color>";
        combinedString += getAssetsFromList(loadedLevels);

        bundleManagerRoot.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = combinedString;
    }

    private string getAssetsFromList(List<ModAsset> assets) {

        string combinedString = "";
        foreach (ModAsset loadedMod in assets) {
            combinedString += "\n<color=green>" + loadedMod.bundle.name + " @ " + loadedMod.loadTime + "<color=yellow>";
            foreach (string assetName in loadedMod.bundle.GetAllAssetNames()) {
                combinedString += "\n  ├─<color=yellow> " + assetName + "</color>";
            }
        }

        return combinedString;
    }

    public void Setting_ToggleFullScreen()
    {
        switch (fullscreen)
        {
            case 0: Screen.fullScreenMode = FullScreenMode.Windowed; break;
            case 1: Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; break;
            case 2: Screen.fullScreenMode = FullScreenMode.FullScreenWindow; fullscreen = -1; break;
        }
        fullscreen++;
    }

    public void Setting_UnloadAllItems()
    {
        foreach (ModAsset asset in loadedItems) {
            asset.bundle.Unload(true);
        }
        loadedItems.Clear();

        Setting_GetLoadedContent();
    }

    public void Setting_UnloadAllLevels()
    {
        foreach (ModAsset asset in loadedLevels)
        {
            asset.bundle.Unload(true);
        }
        loadedLevels.Clear();

        Setting_GetLoadedContent();
    }

    public void Setting_UnloadAllAssets()
    {
        Setting_UnloadAllItems();
        Setting_UnloadAllLevels();
    }

    public void Setting_EndEpisode()
    {
        GameObject.FindAnyObjectByType<GameplayManager>().m_episodeManager.EpisodeEnd();
    }

    public void Setting_OpenStickerBook()
    {
        GameplayManager manager = GameObject.FindAnyObjectByType<GameplayManager>();

        manager.m_playerControllers[0].m_cancelUIStack.Add(CloseStickerBook, new GameAction());
        manager.m_stickerManager.OpenMenu(0);

        Cursor.lockState= CursorLockMode.Locked;
        Cursor.visible = false;

        transform.gameObject.SetActive(false);
    }

    public void CloseStickerBook(PlayerController player)
    {
        GameObject.FindAnyObjectByType<GameplayManager>().m_stickerManager.CloseMenu();
    }
}
