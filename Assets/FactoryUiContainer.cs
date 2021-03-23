using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactoryUiContainer : MonoBehaviour
{
    public FactoryUiContainer instance;

    public Button Button_Start;
    public GameObject Panel_TierList;
    public Image Image_EventThumbnail;


    void Awake() {
        instance = this;
    }

}
