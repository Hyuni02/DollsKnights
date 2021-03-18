using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIContainer : MonoBehaviour
{
    public static InGameUIContainer instance;

    public GameObject Indicator_Container;
    public GameObject Indicator;
    [Header("Canvas")]
    public Canvas Canvas_FormatedDolls;
    public Canvas Canvas_DollInfo;
    public Canvas Canvas_GameInfo;

    [Header("Prefab")]
    public GameObject Button_FormatedDoll;

    [Header("FormatedDolls")]
    public GameObject Panel_FormatedDolls;
    public GameObject content;

    [Header("DollInfo")]
    public GameObject Panel_DollInfo;
    public GameObject Panel_StateInfo;
    public GameObject Panel_SkillInfo;
    public GameObject Panel_Buffs;
    public Button Button_DeSpawn;
    public Image Image_Doll;
    public Text Text_level;
    public Text Text_dmg;
    public Text Text_armor;
    public Text Text_block;
    public Text Text_acc;
    public Text Text_hp;
    public Text Text_eva;
    public Slider Slider_hp;
    public Button Button_Skill;
    public Image Image_skill;
    public Text Text_skillname;

    [Header("GameInfo")]
    public GameObject Panel_Pause;
    public Button Button_Pause;
    public Button Button_Resume;
    public Button Button_Quit;

    void Awake() {
        instance = this;
        Time.timeScale = 1;
    }
    void Start() {
        Close_Panel_DollInfo();
        Close_Panel_FormatedDolls();
        Close_Panel_Pause();
    }

    //Pause
    public void Open_Panel_Pause() {
        Time.timeScale = 0;
        Panel_Pause.SetActive(true);
    }
    public void Close_Panel_Pause() {
        Time.timeScale = 1;
        Panel_Pause.SetActive(false);
    }
    //FormatedDolls
    public void Open_Panel_FormatedDolls() {
        Panel_FormatedDolls.SetActive(true);
    }
    public void Close_Panel_FormatedDolls() {
        Panel_FormatedDolls.SetActive(false);
    }
    //DollInfo
    public void Open_Panel_DollInfo(GameObject doll) {
        DollController dc = doll.GetComponent<DollController>();
        Image_Doll.sprite = dc.Sprite_Doll;
        Text_level.text = "LV." + doll.GetComponent<OriginalState>().level.ToString();
        Text_acc.text = "ACC." + dc.fs.accuracy.ToString();
        Text_armor.text = "Amr." + dc.fs.armor.ToString();
        Text_block.text = "BLK." + dc.fs.block.ToString();
        Text_dmg.text = "DMG." + dc.fs.damage.ToString();
        Text_eva.text = "EVA." + dc.fs.evasion.ToString();
        //체력바, 스킬 쿨은 InGameManager Update에서
        Panel_DollInfo.SetActive(true);
    }
    public void Close_Panel_DollInfo() {
        InGameManager.instance.SelectedDoll = null;
        Panel_DollInfo.SetActive(false);
    }

    public void UpdateButtonState() {
        for(int i = 0; i < content.transform.childCount; i++) {
            content.transform.GetChild(i).GetComponent<Button_FormatedDollInfo>().UpdateState();
        }
    }
    public void RetreatDoll() {
        InGameManager.instance.SelectedDoll.GetComponent<DollController>().Retreat();
        UpdateButtonState();
        Close_Panel_DollInfo();
    }
}
