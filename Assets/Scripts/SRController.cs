using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuffContainer))]
public class SRController : DMRController
{
    public override void GetInRangeTarget() {
        List_temp_target.Clear();
        for (int i = 0; i < InGameManager.instance.Spawned_Enemies.Count; i++) {
            if (InGameManager.instance.Spawned_Enemies[i].activeSelf) {
                List_temp_target.Add(InGameManager.instance.Spawned_Enemies[i]);
            }
        }
    }
}
