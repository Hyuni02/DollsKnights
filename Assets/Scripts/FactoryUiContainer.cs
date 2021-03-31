using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactoryUiContainer : MonoBehaviour
{
    public static FactoryUiContainer instance;

    public Button Button_Start;
    public GameObject Panel_TierList;
    public GameObject Panel_ThumbNail;


    void Awake() {
        instance = this;
    }

}
