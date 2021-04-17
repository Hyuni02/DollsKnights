using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kar98kController : SRController
{
    ChainShot cs;

    public override void Start() {
        base.Start();
        cs = GetComponent<ChainShot>();
    }

    public override void attack() {
        SetFaceDir(Target.transform.position.x);
        if (cs.skilling) {
            if (Target != null) {
                Target.GetComponent<CharacterBase>().GetAttacked((int)(cs.Selected_dmg_rate * fs.damage), fs.accuracy, fs.critrate, fs.armorpen);
                Target.GetComponent<CharacterBase>().GetAttacked((int)(cs.Selected_dmg_rate * fs.damage), fs.accuracy, fs.critrate, fs.armorpen);
                cs.skilling = false;
                cs.stack = 0;
                cs.stack_timer = 0;
            }
        }
        else {
            Target.GetComponent<CharacterBase>().GetAttacked(fs.damage, fs.accuracy, fs.critrate, fs.armorpen);
        }
    }
}
