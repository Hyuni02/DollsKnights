using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIContainer_LevelSelect : MonoBehaviour
{
    public static UIContainer_LevelSelect instance;

    [Header("Level List")]
    public GameObject Levellist_content;
    public List<GameObject> Buttons_Level;
    public GameObject Image_WorldMap;
    [Header("Level Info")]
    public GameObject Panel_LevelInfo;
    public Text Text_title;
    public Text Text_MaxEchlon;
    public GameObject Image_MapPreview;
    public GameObject Panel_Reward;

    private void Awake() {
        instance = this;
        for (int i = 0; i < Levellist_content.transform.childCount; i++) {
            Buttons_Level.Add(Levellist_content.transform.GetChild(i).gameObject);
        }
    }
}
