using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace BlueyModFramework
{
    //purely exists to refresh menu flavor text when exiting settings panel, because I'm sure others would like to read them more
    public class BMF_SplashRefresh : MonoBehaviour
    {
        public BMF_Init bmfInit;

        public void OnEnable()
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = bmfInit.splashText[UnityEngine.Random.Range(0, bmfInit.splashText.Length)];
        }
    }
}
