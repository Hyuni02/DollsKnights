﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InGameManager : MonoBehaviour {
    public static InGameManager instance;

    public List<DollData> Formated_Dolls;
    public List<GameObject> Spawned_Dolls;
    public List<GameObject> Spawned_Enemies;

    public GameObject StartNode;
    public GameObject EndNode;
    public List<GameObject> DragedNodes = new List<GameObject>();
    public GameObject SelectedNode;

    public GameObject SelectedDoll;

    void Awake() {
        instance = this;
    }

    void Start() {
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

        //스포닝 풀 생성
    }


    void Update() {
        if (IsPointerOverUIObject())
            return;

        GetMouseInput();
        CheckInputType();
    }

    void GetMouseInput() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit = UseRay();
            if (hit.transform.tag.Equals("node")) {
                //print("Click Start");
                DragedNodes.Clear();
                StartNode = hit.transform.gameObject;
                DragedNodes.Add(hit.transform.gameObject);
            }
        }
        if (Input.GetMouseButton(0)) {
            RaycastHit hit = UseRay();
            if (hit.transform.tag.Equals("node")) {
                //print("Click ing");
                if (DragedNodes[DragedNodes.Count - 1] != hit.transform.gameObject) {
                    DragedNodes.Add(hit.transform.gameObject);
                }
            }
        }
        if (Input.GetMouseButtonUp(0)) {
            RaycastHit hit = UseRay();
            if (hit.transform.tag.Equals("node")) {
                //print("Click End");
                if (StartNode != null) {
                    EndNode = hit.transform.gameObject;
                    DragedNodes.Add(hit.transform.gameObject);
                }
                else {
                    _Reset();
                }
            }
        }
    }
    RaycastHit UseRay() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);

        return hit;
    }
    private void _Reset() {
        StartNode = null;
        EndNode = null;
        DragedNodes.Clear();
    }

    void CheckInputType() {
        if (StartNode == null || EndNode == null)
            return;

        //클릭
        if (StartNode == EndNode) {
            print("Input Type : Click");
            //노드 위에 서있는 인형 탐색
            SelectedNode = StartNode;
            SelectedDoll = FindDoll(SelectedNode);

            //인형이 서있으면 인형 정보 표시
            if (SelectedDoll != null)
                ViewDollInfo(SelectedDoll);
            else {
                //노드가 헬리포트이면 편성표 표시
                if (StartNode.GetComponent<NodeInfo>().heilport) {
                    InGameUIContainer.instance.Open_Panel_FormatedDolls();
                }
            }
        }

        //드래그
        else {
            print("Input Type : Drag");

            //유효한 루트인지 확인
        }

        _Reset();
    }
    public void ViewDollInfo(GameObject doll) {
        print("View Doll Info");

    }
    GameObject FindDoll(GameObject node) {
        for (int i = 0; i < Spawned_Dolls.Count; i++) {
            if (Spawned_Dolls[i].GetComponent<DollController>().placed
                && Spawned_Dolls[i].GetComponent<DollController>().Node_StandOn == node)
                return Spawned_Dolls[i];
        }
        return null;
    }

    private bool IsPointerOverUIObject() {//터치가 UI를 뚫고 지나가 뒤에 있는 오브젝트에 닿는 것을 막는 함수
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
