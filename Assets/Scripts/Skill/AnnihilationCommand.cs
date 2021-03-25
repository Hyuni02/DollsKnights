using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnihilationCommand : SkillBase
{
    GameObject Target;
    public int[] increase_rof = { 6, 6, 7, 7, 8, 8, 9, 9, 10, 10 };
    public int[] increase_crit = { 25, 26, 27, 28, 29, 31, 32, 33, 34, 35 };

    public override void SkillActive() {
        print("use skill : " + GetSkillName() + "    LV." + level);
        //배치된 모든 인형들에게 버프 부여
        skill_cool_timer = GetCoolDown();
        skill_duration_timer = GetDuration();
        for (int i = 0; i < InGameManager.instance.Spawned_Dolls.Count; i++) {
            //버프 소환
            GameObject buff_ppk = Instantiate(InGameManager.instance.buff);
            buff_ppk.GetComponent<Buff>().Initialized(GetSkillName(), GetDuration(), GetComponent<SkillBase>().skill_icon, gameObject, InGameManager.instance.Spawned_Dolls[i], 0, 0, 0, increase_rof[level - 1], 0, 0,
                increase_crit[level - 1], false, 0);
        }
    }
    public override void SkillDescribe() {
        base.SkillDescribe();

        skill_describe = skill_describe_form.Replace("_rof", increase_rof[level - 1].ToString());
        skill_describe = skill_describe.Replace("_crit", increase_crit[level - 1].ToString());
    }
}
