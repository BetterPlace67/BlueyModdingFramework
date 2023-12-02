using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BMF_UiButton : MonoBehaviour
{
    [HideInInspector] public bool isLevel;
    [HideInInspector] public int index;
    public TextMeshProUGUI text;
    [HideInInspector] public BMF_QuickMenu menu;

    void Awake()
    {
        text = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void UseButton()
    {
        if (isLevel)
        {
            menu.LoadLevel(index);
        }
        else
        {
            menu.GiveItem(index);
        }
    }
}
