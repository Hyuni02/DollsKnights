using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIndexInfo : MonoBehaviour
{
    [SerializeField]
    private int index = 0;

    private void Awake() {
        GetComponent<Button>().onClick.AddListener(delegate { Set_SelectedLevel(index); });
    }

    void Set_SelectedLevel(int index) {
        GameManager.instance.Set_SelectedLevel(index);
        MainMenuSceneController.instance.Open_Panel_LevelInfo();
    }
}
