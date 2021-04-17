using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGController : MGController
{
    //저지중인 모든 적 공격
    public override void SearchTarget() {

        if (!gameObject.activeSelf)
            return;

        attackable = false;

        if (Blocked_Enemies.Count > 0) {
            SetTarget(Blocked_Enemies[0]);
            attackable = true;
        }
        else {
            SetTarget();
            attackable = false;
        }
    }

    public override void attack() {
        SetFaceDir(Target.transform.position.x);
        for (int i = 0; i < Blocked_Enemies.Count; i++) {
            Blocked_Enemies[i].GetComponent<CharacterBase>().GetAttacked(fs.damage, fs.accuracy, fs.critrate, fs.armorpen);
        }
        fs.ammo--;
    }
}
