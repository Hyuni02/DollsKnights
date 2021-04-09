using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_FormatedDollInfo : MonoBehaviour
{
    public Sprite Icon;
    public Text Text_Cost;
    public GameObject model;
    public Button Button_heal;
    public bool placed = false;
    public bool healing = false;
    public bool destroyed = false;
    public DollState dollState;
    public Slider Slider_HP;
    int maxhp;

    Button button;

    void Start()
    {
        GetComponent<Image>().sprite = Icon;
        Text_Cost.text = dollState.cost.ToString();
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { PlaceDoll(); });
        maxhp = dollState.hp;
        Slider_HP.maxValue = maxhp;

        Slider_HP.gameObject.SetActive(false);
        Button_heal.gameObject.SetActive(false);

        InvokeRepeating("Refresh", 0.1f, 0.1f);
    }

    void Refresh() {
        Slider_HP.value = model.GetComponent<FinalState>().hp;
        placed = model.GetComponent<DollController>().placed;
        if (model.GetComponent<FinalState>().hp != maxhp) {
            Slider_HP.gameObject.SetActive(true);
            if (!placed)
                Button_heal.gameObject.SetActive(true);
        }
        else {
            Slider_HP.gameObject.SetActive(false);
            Button_heal.gameObject.SetActive(false);
        }

        if(dollState.cost <= InGameManager.instance.cost && !placed && !healing) {
            button.interactable = true;
        }
        else {
            button.interactable = false;
        }

        if (healing)
            Button_heal.gameObject.SetActive(false);
    }

    public void UpdateState() {
        if (placed || destroyed)
            button.interactable = false;
        else
            button.interactable = true;
    }

    [SerializeField]
    int cost_heal, part_heal, time_heal = 0;
    int type_c, type_p = 0;
    float hp_rate;
    public void HealDoll() {
        //클래스 별 수복 계수
        switch (model.GetComponent<OriginalState>().dollstate._class) {
            case "HG": type_c = 20; type_p = 7; break;
            case "SMG": type_c = 45; type_p = 12; break;
            case "AR": type_c = 40; type_p = 14; break;
            case "DMR": type_c = 35; type_p = 16; break;
            case "SR": type_c = 35; type_p = 16; break;
            case "SG": type_c = 65; type_p = 32; break;
            case "MG": type_c = 75; type_p = 30; break;
        }
        //남은 체력 비율
        hp_rate = (float)model.GetComponent<FinalState>().hp / (float)maxhp;
        //수복 인력 계산
        cost_heal = Mathf.CeilToInt(type_c * hp_rate);
        //수복 부품 계산
        part_heal = Mathf.CeilToInt(type_p * hp_rate);

        if (InGameManager.instance.cost < cost_heal || InGameManager.instance.parts < part_heal) {
            //코스트 창 강조
            return;
        }

        //수복 시간 계산
        //temp
        time_heal = 4;
        InGameManager.instance.cost -= cost_heal;
        InGameManager.instance.parts -= part_heal;
        Invoke("HealDone", time_heal);
        healing = true;
    }
    void HealDone() {
        model.GetComponent<FinalState>().hp = maxhp;
        healing = false;
    }

    void PlaceDoll() {
        if (InGameManager.instance.SelectedNode == null)
            return;

        Transform pos = InGameManager.instance.SelectedNode.transform;
        switch (pos.GetComponent<NodeInfo>().type) {
            case NodeInfo.Type.low:
                model.transform.position = new Vector3(pos.position.x, 0.05f, pos.position.z);
                break;
            case NodeInfo.Type.high:
                model.transform.position = new Vector3(pos.position.x, 0.5f, pos.position.z);
                break;
        }

        model.GetComponent<DollController>().Node_StandOn = InGameManager.instance.SelectedNode;
        model.SetActive(true);
        placed = true;
        model.GetComponent<DollController>().placed = true;
        InGameManager.instance.cost -= dollState.cost;

        InGameUIContainer.instance.UpdateButtonState();
        InGameUIContainer.instance.Close_Panel_FormatedDolls();
    }
}
