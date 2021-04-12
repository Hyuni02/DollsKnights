using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionFocus : SkillBase
{
    public int[] increase_armor = { 40, 43, 47, 50, 53, 57, 60, 63, 67, 70 };

    public override void SkillActive() {
        print("use skill : " + GetSkillName() + "    LV." + skilllevel);
        //배치된 모든 인형들에게 버프 부여
        skill_cool_timer = GetCoolDown();
        skill_duration_timer = GetDuration();

        //버프 소환
        GameObject buff = Instantiate(InGameManager.instance.buff);
        buff.GetComponent<Buff>().Initialized(GetSkillName(), GetDuration(), GetComponent<SkillBase>().skill_icon, gameObject, gameObject, 0, 0, increase_armor[skilllevel], 0, 0, 0, 0, false, 0);
    }

    public override void SkillDescribe() {
        base.SkillDescribe();
        skill_describe = skill_describe.Replace("_armor", increase_armor[skilllevel].ToString());
    }
}
