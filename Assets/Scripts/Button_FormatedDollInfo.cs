using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_FormatedDollInfo : MonoBehaviour
{
    public Sprite Icon;
    public GameObject model;
    public bool placed = false;

    Button button;

    void Start()
    {
        GetComponent<Image>().sprite = Icon;
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { PlaceDoll(); });
    }

    public void UpdateState() {
        if (placed)
            button.interactable = false;
        else
            button.interactable = true;
    }

    void PlaceDoll() {
        Transform pos = InGameManager.instance.SelectedNode.transform;
        switch (pos.GetComponent<NodeInfo>().type) {
            case NodeInfo.Type.low:
                model.transform.position = new Vector3(pos.position.x, 0f, pos.position.z);
                break;
            case NodeInfo.Type.high:
                model.transform.position = new Vector3(pos.position.x, 0.4f, pos.position.z);
                break;
        }

        model.SetActive(true);
        placed = true;
        model.GetComponent<DollController>().placed = true;
        InGameUIContainer.instance.UpdateButtonState();
        InGameUIContainer.instance.Close_Panel_FormatedDolls();
    }
}
