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
            Button_heal.gameObject.SetActive(true);
        }
        else {
            Slider_HP.gameObject.SetActive(false);
            Button_heal.gameObject.SetActive(false);
        }

        if(dollState.cost <= InGameManager.instance.cost && !placed) {
            button.interactable = true;
        }
        else {
            button.interactable = false;
        }
    }

    public void UpdateState() {
        if (placed || destroyed)
            button.interactable = false;
        else
            button.interactable = true;
    }

    void PlaceDoll() {
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
