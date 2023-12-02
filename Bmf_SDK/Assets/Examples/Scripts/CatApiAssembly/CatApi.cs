using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

//Hello!
//This script goes along with the example level "ex_CatAPI"
//It calls an online api for cat images and loads them in game as a texture to view
//To use in game, press the "Compile Scripts" button at the top, under 'Bluey Modding', and drag the .dll into the 'Plugins/' folder of your game directory


//In game, hold tab to enable cursor and click the button
namespace CatApi
{
    public class CatApi : MonoBehaviour
    {
        //  image component that will be used
        [SerializeField] RawImage image;

        //  called by the UI button
        public void GetNextCat()
        {
            Debug.Log("Fetching you a fresh feline!");
            StartCoroutine(setImage("https://api.thecatapi.com/v1/images/search"));
        }

        //await url response & set texture
        IEnumerator setImage(string url)
        {
            //create texture reference
            Texture2D texture = null;
            //contact CatApi
            WWW www = new WWW(url);
            //await response
            yield return www;
            //split response data by "
            string[] contents = www.text.Split('"');
            //content[7] is web url for current cat image, submit new request for that url
            WWW www2 = new WWW(contents[7]);
            //await response
            yield return www2;
            //set texture to cat image
            texture = www2.texture;
            //set ui image to texture
            image.texture = texture;
            image.SetNativeSize();
            //cleanup
            www.Dispose();
            www = null;
            www2.Dispose();
            www2 = null;
        }
    }
}

//uses deprecated WWW > UnityWebRequest for simplicity