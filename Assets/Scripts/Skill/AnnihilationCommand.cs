using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnihilationCommand : SkillBase
{
    GameObject Target;
    public int[] increase_rof = { 6, 6, 7, 7, 8, 8, 9, 9, 10, 10 };
    public int[] increase_crit = { 25, 26, 27, 28, 29, 31, 32, 33, 34, 35 };

    public override void SkillActive() {
        //배치된 모든 인형들에게 버프 부여
    }
    public override void SkillDescribe() {
        base.SkillDescribe();

        skill_describe = skill_describe_form.Replace("_rof", increase_rof[level - 1].ToString());
        skill_describe = skill_describe.Replace("_crit", increase_crit[level - 1].ToString());
    }
}
