using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BlueyModFramework
{
    public class BMF_ControllerInput : MonoBehaviour
    {
        public GameObject backToDisable, backToEnable, toFocus;

        void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(toFocus);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button1)) {
                
                if(backToEnable!=null)
                    backToEnable.SetActive(true);

                backToDisable.SetActive(false);
            }
        }
    }
}
