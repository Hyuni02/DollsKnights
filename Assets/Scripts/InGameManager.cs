using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public static InGameManager instance;

    public List<DollData> Formated_Dolls;
    public List<GameObject> Spawned_Dolls;
    public List<GameObject> Spawned_Enemies;

    void Awake() {
        instance = this;
    }

    void Start()
    {
        GameObject Map = Instantiate(LevelContainer.instance.Levels[GameManager.instance.Index_SelectedLevel]);

        //선택한 제대 생성
        for (int target_echlon = 0; target_echlon < GameManager.instance.Index_SelectedEchlons.Count; target_echlon++) {
            for (int i = 0; i < GetData.instance.List_DollData.Count; i++) {
                if (GetData.instance.List_DollData[i].echlon 
                    == GameManager.instance.Index_SelectedEchlons[target_echlon].GetComponent<Button_EchlonInfo>().index_Echlon) {
                    Formated_Dolls.Add(GetData.instance.List_DollData[i]);
                    GameObject button = Instantiate(InGameUIContainer.instance.Button_FormatedDoll, InGameUIContainer.instance.content.transform);
                    GameObject doll = Instantiate(DollContainer.instance.Dolls[i]);
                    Spawned_Dolls.Add(doll);
                    button.GetComponent<Button_FormatedDollInfo>().model = doll;
                    button.GetComponent<Button_FormatedDollInfo>().Icon = doll.GetComponent<DollController>().Sprite_Doll_face;

                    //능력치 적용
                    doll.GetComponent<OriginalState>().level = GetData.instance.List_DollData[i].level;
                    doll.GetComponent<OriginalState>().dollstate = GetData.instance.List_DollState[i];
                    doll.GetComponent<OriginalState>().SetState_Doll();

                    doll.SetActive(false);
                }
            }
        }

        //제대표에 이미지 추가

        //스포닝 풀 생성
    }

}
