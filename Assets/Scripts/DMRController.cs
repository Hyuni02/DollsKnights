using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuffContainer))]
public class DMRController : DollController {

    public List<GameObject> List_temp_target = new List<GameObject>();
    public GameObject temp_target;

    //체력이 적은 적 우선 공격
    public override void SearchTarget() {

        if (!gameObject.activeSelf)
            return;

        attackable = false;
        GetInRangeTarget();

        if (List_temp_target.Count > 0) {
            temp_target = List_temp_target[0];
            for (int i = 0; i < List_temp_target.Count; i++) {
                if (temp_target.GetComponent<FinalState>().hp > List_temp_target[i].GetComponent<FinalState>().hp) {
                    temp_target = List_temp_target[i];
                }
            }
            SetTarget(temp_target);
            attackable = true;
        }
        else {
            SetTarget();
            attackable = false;
        }
    }

    public virtual void GetInRangeTarget() {
        List_temp_target.Clear();
        for (int i = 0; i < InGameManager.instance.Spawned_Enemies.Count; i++) {
            if (GetDistance(InGameManager.instance.Spawned_Enemies[i]) <= fs.range
                && InGameManager.instance.Spawned_Enemies[i].activeSelf) {
                List_temp_target.Add(InGameManager.instance.Spawned_Enemies[i]);
            }
        }
    }
}
