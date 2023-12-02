using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BlueyModdingFramework
{
    public class BMF_SkinManager : MonoBehaviour
    {
        public static BMF_SkinManager callback;
        //  0     1     2       3    4    5      6     7
        //bluey bingo bandit chilli rad stripe muffin mort
        public Material[] AtlasMats;

        void Start()
        {
            callback = this;
        }

        public void RefreshSkins()
        {
            Debug.Log("Fetching custom skins...");
            Debug.Log(Path.Combine(Path.GetDirectoryName(Application.dataPath), "ModAssets/CustomSkins/"));

            SkeletonUtility[] skeletons = GameObject.FindObjectsOfType<SkeletonUtility>();

            foreach (SkeletonUtility skel in skeletons)
            {
                Material mat = skel.gameObject.GetComponent<MeshRenderer>().material;
                if (mat != null)
                {

                    switch (skel.gameObject.name)
                    {
                        case "Bluey": StartCoroutine(loadImage(skel.gameObject.name, new Vector2(2048, 2048), mat, skel)); break;
                        case "Bingo": StartCoroutine(loadImage(skel.gameObject.name, new Vector2(2048, 2048), mat, skel)); break;
                        case "Chilli": StartCoroutine(loadImage(skel.gameObject.name, new Vector2(2048, 1024), mat, skel)); break;
                        case "Bandit": StartCoroutine(loadImage(skel.gameObject.name, new Vector2(2048, 1024), mat, skel)); break;
                    }
                }
            }
        }

        private IEnumerator loadImage(string path, Vector2 dimensions, Material mat, SkeletonUtility skel)
        {
            Debug.Log("Fetching: " + path);
            WWW www = new WWW(Path.Combine(Path.GetDirectoryName(Application.dataPath), "ModAssets/CustomSkins/" + path + ".png"));
            yield return www;
            Debug.Log("Found texture... " + Path.Combine(Path.GetDirectoryName(Application.dataPath), "ModAssets/CustomSkins/" + path + ".png"));
            Texture2D texTmp = new Texture2D(Mathf.RoundToInt(dimensions.x), Mathf.RoundToInt(dimensions.y), TextureFormat.DXT1, false);
            www.LoadImageIntoTexture(texTmp);

            int index = 0;
            string texName = "";
            switch(path) {
                case "Bluey": index = 0; texName = "skeleton"; break;
                case "Bingo": index = 1; texName = "skeleton"; break;
                case "Bandit": index = 2; texName = "Skeleton"; break;
                case "Chilli": index = 3; texName = "Chilli_Animation2D"; break;
                case "Rad": index = 4; texName = "Radley_Animation2D"; break;
                case "Stripe": index = 5; texName = "skeleton"; break;
                case "Muffin": index = 6; texName = "skeleton"; break;
                case "Mort": index = 7; texName = "Mort_2D_Animation2D"; break;
            }

            texTmp.name = texName;

            AtlasMats[index].mainTexture = texTmp;
            //Atlases[index].Clear

            skel.gameObject.GetComponent<MeshRenderer>().material = AtlasMats[index];

            Debug.Log("SET!");

            yield return null;
        }
    }
}

