using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIContainer : MonoBehaviour
{
    public static InGameUIContainer instance;

    [Header("Canvas")]
    public Canvas Canvas_FormatedDolls;
    public Canvas Canvas_DollInfo;
    public Canvas Canvas_GameInfo;

    [Header("Prefab")]
    public GameObject Button_FormatedDoll;

    [Header("FormatedDolls-----Panel-----")]
    public GameObject Panel_FormatedDolls;
    public GameObject content;
    [Header("                          -----Button-----")]

    //[Header("DollInfo-----Panel-----")]
    //[Header("                   -----Button-----")]

    [Header("GameInfo-----Panel-----")]
    public GameObject Panel_Pause;
    [Header("                   -----Button-----")]
    public Button Button_Pause;
    public Button Button_Resume;
    public Button Button_Quit;

    void Awake() {
        instance = this;
        Time.timeScale = 1;
    }


    public void Open_Panel_Pause() {
        Time.timeScale = 0;
        Panel_Pause.SetActive(true);
    }
    public void Close_Panel_Pause() {
        Time.timeScale = 1;
        Panel_Pause.SetActive(false);
    }
    public void Open_Panel_FormatedDolls() {
        Panel_FormatedDolls.SetActive(true);
    }
    public void Close_Panel_FormatedDolls() {
        Panel_FormatedDolls.SetActive(false);
    }
    public void UpdateButtonState() {
        for(int i = 0; i < content.transform.childCount; i++) {
            content.transform.GetChild(i).GetComponent<Button_FormatedDollInfo>().UpdateState();
        }
    }
}
