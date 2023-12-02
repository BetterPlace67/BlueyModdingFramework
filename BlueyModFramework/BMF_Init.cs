using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MelonLoader;
using System.IO;
using Spine.Unity;
using System.Collections;
using UnityEngine.UI;
using BlueyModdingFramework;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using KinematicCharacterController;

namespace BlueyModFramework
{
    public class BMF_Init : MelonMod
    {
        public bool menuOpen;
        AssetBundle bundle;
        GameObject modMenu;

        public List<BMF_QuickMenu.ModAsset> loadedMods = new List<BMF_QuickMenu.ModAsset>();

        public static BMF_Init bmf_Callback;

        public string[] splashText =
        {
            "Now with flavor text!",
            "Modded!",
            "Custom Content!",
            "Now with mods!",
            "Custom levels!",
            "Custom items!",
            "Custom skins!",
            "Like you've never seen before!",
            "Moddable!",
            "BetterPlace was here",
            "Hackerman 9000",
            "Extendable!",
            "But cool",
            "Bluey Modding Framework",
            "(Don't tell Ludo)",
            "Come on squirts let's meet Rad in the backyard",
            "Moddable kids game",
            "Check NexusMods for new content!",
            "(Funny text)",
            "Press ~ or [back] (controller) for mods",
            "Customize your looks within 'ModAssets/CustomSkins/'",
            "Drag new levels to 'ModAssets/CustomLevels/'",
            "Add new items with 'ModAssets/CustomItems/'",
            "Extended shadow distance!",
            "Press - & + to change time scale!",
            "Hold TAB to enable mouse cursor",
            "Open stickerbook, anywhere!",
            "50x more prone to errors! :)",
            ":)",
            "Levels, items, and skins update in real time!",
            "To reload a freshly updated asset, unload it first!",
            "Enjoy!",
            "Have fun!",
            "Report bugs on NexusMods or Github!",
            "Open Source!",
            "Technical debt!",
            "Please try keep mods appropriate-ish",
            "Happy days!",
            "Project Biscuits!",
            "100% more Biscuits!",
            "I don't want a valuable lime lesson!",
            "Dad enters the room",
            "Here's dad!",
            "I slipped on mah beans!",
            "aaaand why should I care?",
            "COCONUTS HAVE WATER IN THEM?!?!?!?",
            "30% less bum worms",
            "Socks is the best Heeler, fight me",
            "Duck cake!",
            "I'm not taking advice from a cartoon dog",
            "This was trifficult",
            "It's just monkeys singing songs mate",
            "<color=red>R<color=orange>A<color=yellow>I<color=green>N<color=#03c6fc>B<color=blue>O<color=purple>W</color>",
            "Better than Let's Play!",
            "FOURTY BUCKS?????",
            "BeeeeeOOP!",
            ":3",
            "90% of this framework was developed past midnight",
            "It just works",
            "Modding sdk included!",
            "Keyboard & mouse support?",
            "Leave .blueymod files in the root 'ModAssets' folder",
            "Loads non-item/level assets too!",
            "Modders should read the documentation!",
            "Use UnityExplorer to debug your mods!",
            "Pineapple on pizza isn't bad, but needs other toppings to support it",
            "The 'ModAssets' folder is the source of all fun",
            "FMOD sucks"
        };


        //after startup
        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            base.OnSceneWasInitialized(buildIndex, sceneName);

            bmf_Callback = this;

            //find custom level manager (suboptimal name check)
            if (sceneName != "IntroVideoScene" && sceneName != "SetUp" && sceneName != "MainMenu" && sceneName != "GameplayLoader" && sceneName != "HeelerHouse" && sceneName != "Playgrounds" && sceneName != "TheCreek" && sceneName != "TheBeach") {
                Debug.Log("Custom Level!");
                ProxyLevelManager lvlManager = ProxyLevelManager.callback;
                if (lvlManager == null) { Debug.Log("Couldn't find level manager, using fallback spawnpos :,("); CustLevelTempSpawn(); return; }
                PlayerMovementKinematic[] players = GameObject.FindObjectsOfType<PlayerMovementKinematic>();
                if (players.Length == 0) { Debug.Log("Couldn't find players :,("); return; }
                foreach (PlayerMovementKinematic player in players) {
                    //set movement parameters.           TODO: revert to default vals after loading official level
                    player.m_jumpSpeed = lvlManager.playerJumpSpeed;
                    player.m_playerWalkingSpeed = lvlManager.playerWalkSpeed;
                    player.m_playerRunSpeed= lvlManager.playerRunSpeed;
                    player.m_maxAirMoveSpeed = lvlManager.playerAirSpeed;
                    player.Gravity = lvlManager.playerGravity;
                    //teleport to spawn pos

                    Vector3 spawnPos = new Vector3();

                    //per character spawn points 
                    if (lvlManager.m_listTpScenes[0].m_positionsToTpPlayers.Count > 1 ) {
                        switch (player.gameObject.name) {
                            case "Bluey": spawnPos = lvlManager.m_listTpScenes[0].m_positionsToTpPlayers[0].transform.position; break;
                            case "Bingo": spawnPos = lvlManager.m_listTpScenes[0].m_positionsToTpPlayers[1].transform.position; break;
                            case "Bandit": spawnPos = lvlManager.m_listTpScenes[0].m_positionsToTpPlayers[2].transform.position; break;
                            case "Chilli": spawnPos = lvlManager.m_listTpScenes[0].m_positionsToTpPlayers[3].transform.position; break;
                        }
                    } else { //fallback in case not enough spawnpoints exist for each character
                        spawnPos = lvlManager.m_listTpScenes[0].m_positionsToTpPlayers[0].transform.position;
                    }

                    //teleport
                    player.TeleportTo( spawnPos, true );
                }
            }

            //load modassets
            if (buildIndex != 0 && buildIndex != 3) {
                foreach (BMF_QuickMenu.ModAsset asset in loadedMods) {
                    foreach (string name in asset.bundle.GetAllAssetNames()) {
                        if (name.Contains(sceneName)) {
                            var item = asset.bundle.LoadAsset(name);
                            GameObject.Instantiate(item, null, true);
                        }
                    }
                }
            }

            //on startup
            if (buildIndex == 0) {
                Debug.Log("-   Framework Initialized   -");

                //lock cursor to game
                ToggleCursorVisibility(false);

                //initialize modules
                //FetchSkins();

                var info = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(Application.dataPath), "ModAssets"));

                FileInfo[] files = info.GetFiles();

                foreach (FileInfo file in files) {
                    if (file.Extension == ".blueymod") {
                        //LoadModAsset(file.Name);
                        BMF_QuickMenu.ModAsset asset = new BMF_QuickMenu.ModAsset();
                        asset.assetPath = file.FullName;
                        asset.loadTime = System.DateTime.Now.ToString();
                        asset.bundle = AssetBundle.LoadFromFile(file.FullName);//Application.dataPath + "/ModAssets/" + file.Name);
                        loadedMods.Add(asset);

                        if (file.Name.Contains("!")) {
                            asset.bundle.LoadAllAssets<MonoBehaviour>();
                            GameObject[] prefabs = asset.bundle.LoadAllAssets<GameObject>();

                            foreach (GameObject prefab in prefabs) {
                                GameObject.Instantiate(prefab, null, true);
                            }
                        } else {
                            foreach (string name in asset.bundle.GetAllAssetNames()) {
                                if (name.Contains("!")) {
                                    var item = asset.bundle.LoadAsset(name);
                                    GameObject.Instantiate(item, null, true);
                                }
                            }
                        }
                    }
                }


                string assetPath = Path.Combine(Path.GetDirectoryName(Application.dataPath), "ModAssets/modframework.bmf");
                bundle = AssetBundle.LoadFromFile(assetPath);

                bundle.LoadAllAssets<MonoBehaviour>();

                //increase shadow distance so custom maps don't look horrendus (default - 150)
                float newShadowDistance = 400; //new distance
                QualitySettings.shadowDistance = newShadowDistance;
                UniversalRenderPipelineAsset urp = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;
                urp.shadowDistance = newShadowDistance;

                BMF_QuickMenu.ModAsset modAsset = new BMF_QuickMenu.ModAsset();
                modAsset.assetPath = assetPath;
                modAsset.bundle = bundle;
                modAsset.loadTime = System.DateTime.Now.ToString();
                loadedMods.Add(modAsset);


            } else if (buildIndex == 3) { //in gameplay
                BMF_Startup();
                PrepSkinManager();

                foreach (BMF_QuickMenu.ModAsset asset in loadedMods) {
                    if (asset.bundle.name.Contains("~")) {
                        asset.bundle.LoadAllAssets<MonoBehaviour>();
                        GameObject[] prefabs = asset.bundle.LoadAllAssets<GameObject>();

                        foreach (GameObject prefab in prefabs) {
                            GameObject.Instantiate(prefab, null, true);
                        }
                    } else {
                        foreach (string name in asset.bundle.GetAllAssetNames()) {
                            if (name.Contains("~")) {
                                var item = asset.bundle.LoadAsset(name);
                                GameObject.Instantiate(item, null, true);
                            }
                        }
                    }
                }

            } else if (buildIndex == 2) { //in main menu
                var prefab = bundle.LoadAsset<GameObject>("MenuFlavorText.prefab");
                GameObject splashGo = GameObject.Instantiate(prefab, GameObject.Find("Logo").transform, false);
                BMF_SplashRefresh splash = splashGo.transform.GetChild(0).gameObject.AddComponent<BMF_SplashRefresh>();
                splash.bmfInit = this;
                splash.OnEnable();
                //splashGo.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = splashText[UnityEngine.Random.Range(0, splashText.Length)];
            }
        }

        //  temp for testing since finding proxylevelmanager is more difficult than imagined
        public void CustLevelTempSpawn()
        {
            PlayerMovementKinematic[] players = GameObject.FindObjectsOfType<PlayerMovementKinematic>();
            if (players.Length == 0) { Debug.Log("Couldn't find players :,("); return; }
            foreach (PlayerMovementKinematic player in players) {
                player.TeleportTo(new Vector3(0, 0.5f, 0), false);
            }
        }

        public static void LoadModAsset(string modName, string assetName, Transform parent = null, bool worldSpace = false)
        {
            foreach (BMF_QuickMenu.ModAsset asset in BMF_Init.bmf_Callback.loadedMods) {
                if (asset.bundle.name == modName) {
                    var file = asset.bundle.LoadAsset(assetName);
                    GameObject.Instantiate(file, parent, worldSpace);
                    break;
                }
            }
        }

        public void BMF_Startup()
        {
            //load framework assets
            Debug.Log("Loading framework assets...");

            //keyboard / mouse UI input
            var intPrefab = bundle.LoadAsset<GameObject>("FullscreenCanvas.prefab");
            GameObject.Instantiate(intPrefab, null, true);

            Debug.Log("Creating Quick Menu...");
            var prefab = bundle.LoadAsset<GameObject>("ModCanvas.prefab");

            //create mod menu
            modMenu = GameObject.Instantiate(prefab, Camera.main.transform, false);
            modMenu.gameObject.GetComponent<Canvas>();
            modMenu.transform.localPosition = new Vector3(0, 0, 1);
            modMenu.transform.localScale = new Vector3(0.002f, 0.002f, 0.002f);
            //setup mod menu
            Debug.Log("Setting parameters...");
            BMF_QuickMenu menu = modMenu.transform.GetChild(0).gameObject.AddComponent<BMF_QuickMenu>();
            GameObject button = bundle.LoadAsset<GameObject>("ModButton.prefab");
            Debug.Log("ButtonAsset: " + button);

            menu.button = button;
            menu.itemRoot = menu.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetChild(0); //item page root
            menu.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(() => menu.RefreshItems()); //refresh item list on open
            menu.levelRoot = menu.transform.GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetChild(0); //level page root
            menu.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(() => menu.RefreshLevels()); //refresh level list on open
            menu.settingsRoot = menu.transform.GetChild(0).GetChild(3); //settings page root
            menu.bundleManagerRoot = menu.transform.GetChild(0).GetChild(4); //bundle manager page root
            menu.bmfInit = this;

            //menu.RefreshItems();
            //menu.RefreshLevels();

            modMenu.transform.GetChild(0).gameObject.SetActive(false);
        }

        //Custom keybinds
        public override void OnUpdate()
        {
            base.OnUpdate();

            // + - keys adjust game speed
            if (Input.GetKeyDown(KeyCode.Equals)) {
                if (Time.timeScale == 4f){
                    Time.timeScale = 1;
                } else {
                    Time.timeScale = 4f;
                }
            } else if (Input.GetKeyDown(KeyCode.Minus)) {
                if (Time.timeScale == 0.3f) {
                    Time.timeScale = 1;
                } else {
                    Time.timeScale = 0.3f;
                }
            }

            // ` / ~ keys toggle cursor / quick menu
            if (Input.GetKeyDown(KeyCode.BackQuote) || Input.GetKeyDown(KeyCode.Joystick1Button6)) {
                switch (menuOpen) {
                    case false: {
                            ToggleCursorVisibility(true);
                            modMenu.transform.GetChild(0).gameObject.SetActive(true);
                            break;
                        }
                    case true: {
                            ToggleCursorVisibility(false);
                            modMenu.transform.GetChild(0).gameObject.SetActive(false);
                            break;
                        }
                }
                menuOpen = !menuOpen;
            } else if (Input.GetKey(KeyCode.Tab)) {
                ToggleCursorVisibility(true);
            } else if (menuOpen) {
                ToggleCursorVisibility(true);
            } else {
                ToggleCursorVisibility(false);
            }

            if ((Input.GetKeyDown(KeyCode.Insert))) { //restart bmf
                BMF_Startup();
            }
        }

        //toggle mouse cursor visibility
        public void ToggleCursorVisibility(bool visible)
        {
            switch (visible)
            {
                case true: {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                }
                case false: {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Confined;
                    break;
                }
            }
        }

        //CUSTOM SKINS
        public void FetchSkins()
        {
            Debug.Log("Fetching custom skins...");
            Debug.Log(Path.Combine(Path.GetDirectoryName(Application.dataPath), "ModAssets/CustomSkins/"));

            SkeletonMecanim[] skeletons = GameObject.FindObjectsOfType<SkeletonMecanim>();

            foreach (SkeletonMecanim skel in skeletons)
            {
                Material mat = skel.gameObject.GetComponent<MeshRenderer>().material;
                if (mat != null)
                {
                    switch (skel.gameObject.name)
                    {
                        case "Bluey": MelonCoroutines.Start(loadImage(skel.gameObject.name, new Vector2(2048, 2048), mat, skel)); break;
                        case "Bingo": MelonCoroutines.Start(loadImage(skel.gameObject.name, new Vector2(2048, 2048), mat, skel)); break;
                        case "Chilli": MelonCoroutines.Start(loadImage(skel.gameObject.name, new Vector2(2048, 1024), mat, skel)); break;
                        case "Bandit": MelonCoroutines.Start(loadImage(skel.gameObject.name, new Vector2(2048, 1024), mat, skel)); break;
                    }
                }
            }
        }

        private IEnumerator loadImage(string path, Vector2 dimensions, Material mat, SkeletonMecanim skel)
        {
            Debug.Log("Fetching: " + path);
            WWW www = new WWW(Path.Combine(Path.GetDirectoryName(Application.dataPath), "ModAssets/CustomSkins/" + path + ".png"));
            yield return www;
            Debug.Log("Found texture... " + Path.Combine(Path.GetDirectoryName(Application.dataPath), "ModAssets/CustomSkins/" + path + ".png"));
            Texture2D texTmp = new Texture2D(Mathf.RoundToInt(dimensions.x), Mathf.RoundToInt(dimensions.y), TextureFormat.DXT1, false);
            www.LoadImageIntoTexture(texTmp);

            Material material = new Material(mat);
            material.mainTexture = texTmp;


            mat.SetTexture("_MainTex", texTmp);// = texTmp;
            //skel.skeletonDataAsset.atlasAssets[0] = atlas;//= material;

            MeshRenderer renderer = skel.gameObject.GetComponent<MeshRenderer>();

            Material ogMat = renderer.material;

            skel.CustomMaterialOverride.Add(ogMat, material);

            GameObject testGo = GameObject.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube));
            testGo.transform.position = skel.transform.position;
            testGo.GetComponent<MeshRenderer>().material = material;

            material.mainTexture = texTmp;

            renderer.material = material;
            renderer.sharedMaterial = material;

            GameplayManager manager = GameObject.FindAnyObjectByType<GameplayManager>();

            switch (skel.gameObject.name)
            {
                case "Bluey": manager.m_playerMaterials[0] = material; break;
                case "Bingo": manager.m_playerMaterials[1] = material; break;
                case "Bandit": manager.m_playerMaterials[2] = material; break;
                case "Chilli": manager.m_playerMaterials[3] = material; break;
            }

            //skel.gameObject.AddComponent<MixAndMatchSkinsExample>().customTexture = texTmp;

            Debug.Log("SET!");

            yield return null;
        }


        public void PrepSkinManager()
        {
            var prefab = bundle.LoadAsset<GameObject>("BMF_SkinManager.prefab");
            GameObject skinManager = GameObject.Instantiate(prefab, Camera.main.transform, false);

            Debug.Log("Manager initialized, fetching custom skins:");
            skinManager.GetComponent<BMF_SkinManager>().RefreshSkins();
        }
    }
}
