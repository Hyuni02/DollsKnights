using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimatum : SkillBase
{
    public int[] possibility = { 5, 7, 8, 10, 12, 13, 15, 17, 18, 20 };

    public override void Start() {
        base.Start();
        GetComponent<PKPController>().possibility = possibility[skilllevel];
    }

    public override void SkillActive() {
        return;
    }


    public override void SkillDescribe() {
        base.SkillDescribe();
        skill_describe = skill_describe.Replace("_posi", possibility[skilllevel].ToString());
    }
}
