using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_FormatedDollInfo : MonoBehaviour
{
    public Sprite Icon;
    public GameObject model;
    public bool placed = false;

    void Start()
    {
        GetComponent<Image>().sprite = Icon;
    }
}
